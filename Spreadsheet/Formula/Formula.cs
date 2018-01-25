// Skeleton written by Joe Zachary for CS 3500, January 2017
// Completed by Nathan Herrmann for CS 3500, January 2017
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Formulas
{
    /// <summary>
    /// Represents formulas written in standard infix notation using standard precedence
    /// rules.  Provides a means to evaluate Formulas.  Formulas can be composed of
    /// non-negative floating-point numbers, variables, left and right parentheses, and
    /// the four binary operator symbols +, -, *, and /.  (The unary operators + and -
    /// are not allowed.)
    /// </summary>
    public class Formula
    {

        //Object Variables
        private IEnumerable<String> formulaStrings;

        //Patterns used for recognition of tokens.
        private String lpPattern = @"^\($";
        private String rpPattern = @"\)$";
        private String opPattern = @"[\+\-*/]$";
        private String varPattern = @"^[a-zA-Z][0-9a-zA-Z]*$";
        private String doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: e[\+-]?\d+)?";
        //Syntax check variables
        private Boolean hasToken = false;
        private Boolean firstToken = false;
        private Boolean lastToken = false;
        private Boolean followOpeningOperator = false;
        private Boolean followNumberVariableClosing = false;
        private int lpCount = 0;
        private int rpCount = 0;

        /// <summary>
        /// Creates a Formula from a string that consists of a standard infix expression composed
        /// from non-negative floating-point numbers (using C#-like syntax for double/int literals), 
        /// variable symbols (a letter followed by zero or more letters and/or digits), left and right
        /// parentheses, and the four binary operator symbols +, -, *, and /.  White space is
        /// permitted between tokens, but is not required.
        /// 
        /// Examples of a valid parameter to this constructor are:
        ///     "2.5e9 + x5 / 17"
        ///     "(5 * 2) + 8"
        ///     "x*y-2+35/9"
        ///     
        /// Examples of invalid parameters are:
        ///     "_"
        ///     "-5.3"
        ///     "2 5 + 3"
        /// 
        /// If the formula is syntacticaly invalid, throws a FormulaFormatException with an 
        /// explanatory Message.
        /// </summary>
        public Formula(String formula)
        {
            this.formulaStrings  = GetTokens(formula);

            //Iterator that loops through each token.
            foreach (String token in formulaStrings)
            {
                //If the token is recognized as valid. (Valid tokens are described in the Formula class. and code is provided to detect them.)
                if (Regex.IsMatch(token, doublePattern, RegexOptions.IgnorePatternWhitespace) ||
                    Regex.IsMatch(token, varPattern) || Regex.IsMatch(token, opPattern) ||
                    Regex.IsMatch(token, rpPattern) || Regex.IsMatch(token, lpPattern))
                {
                    //Change boolean to reflect presence of at least one token.
                    this.hasToken = true;

                    //The first token of a formula must be a number, a variable, or an opening parenthesis.
                    //Check for the first token.  If it is not a number, variable or an opening parenthesis, throw exception.
                    if (this.firstToken == false)
                    {
                        if (Regex.IsMatch(token, doublePattern, RegexOptions.IgnorePatternWhitespace) ||
                           Regex.IsMatch(token, varPattern) || Regex.IsMatch(token, lpPattern))
                        {
                            this.firstToken = true;
                        }
                        else
                        {
                            throw new FormulaFormatException("First token is not a number, variable or opening parenthesis");
                        }
                    }

                    //The last token of a formula must be a number, a variable, or a closing parenthesis.
                    //If the last viewed token is a right parentheses, variable or a number, then it could be a valid last token.
                    if (Regex.IsMatch(token, rpPattern) || Regex.IsMatch(token, varPattern) ||
                        Regex.IsMatch(token, doublePattern, RegexOptions.IgnorePatternWhitespace))
                    {
                        this.lastToken = true;
                    }
                    else
                    {
                        //If the last token seen is not a valid last token, then the iteration will end with an error.
                        this.lastToken = false;
                    }
                    
                    //Any token that immediately follows an opening parenthesis or an operator must be either a number, a variable, or an opening parenthesis.
                    if (this.followOpeningOperator == true)
                    {
                        if(Regex.IsMatch(token, doublePattern, RegexOptions.IgnorePatternWhitespace) ||
                           Regex.IsMatch(token, varPattern) || Regex.IsMatch(token, lpPattern))
                        {
                            this.followOpeningOperator = false;
                        }
                        else
                        {
                            throw new FormulaFormatException("The token following an opening parenthesis or an oporator is not valid");
                        }
                    }
                    
                    //Set check for following token.
                    if(Regex.IsMatch(token, lpPattern) || Regex.IsMatch(token,opPattern))
                    {
                        this.followOpeningOperator = true;
                    }
                    else
                    {
                        this.followOpeningOperator = false;
                    }

                    //Any token that immediately follows a number, a variable, or a closing parenthesis must be either an operator or a closing parenthesis.
                    if (this.followNumberVariableClosing == true)
                    {
                        if(Regex.IsMatch(token, opPattern) || Regex.IsMatch(token, rpPattern))
                        {
                            this.followNumberVariableClosing = false;
                        }
                        else
                        {
                            throw new FormulaFormatException("The token following a number, variable or closing parenthesis is not valid");
                        }
                    }

                    //Set check for following token.
                    if(Regex.IsMatch(token, doublePattern, RegexOptions.IgnorePatternWhitespace) ||
                        Regex.IsMatch(token, varPattern) || Regex.IsMatch(token, rpPattern))
                    {
                        this.followNumberVariableClosing = true;
                    }
                    else
                    {
                        this.followNumberVariableClosing = false;
                    }

                    //When reading tokens from left to right, at no point should the number of closing parentheses seen so far be greater than the number of opening parentheses seen so far.
                    if (Regex.IsMatch(token, lpPattern))
                    {
                        this.lpCount++;
                    }

                    if (Regex.IsMatch(token, rpPattern))
                    {
                        this.rpCount++;
                    }
                    
                    if(this.rpCount > this.lpCount)
                    {
                        throw new FormulaFormatException("Closing parentheses appear before openining parentheses.");
                    }
                }
                else
                {
                    //Throw a format exception.
                    throw new FormulaFormatException(token + " is an invalid input");
                }
            }
            //If no tokens are present
            if (hasToken == false)
            {
                throw new FormulaFormatException("There must be at least one token");
            }

            //If the last token is not a valid last token, throw an exception.
            if(this.lastToken == false)
            {
                throw new FormulaFormatException("The last token is not a valid last input");
            }

            //The total number of opening parentheses must equal the total number of closing parentheses.
            if (this.rpCount != this.lpCount)
            {
                throw new FormulaFormatException("The number of opening and closing parentheses is not equal");
            }
        }
        /// <summary>
        /// Evaluates this Formula, using the Lookup delegate to determine the values of variables.  (The
        /// delegate takes a variable name as a parameter and returns its value (if it has one) or throws
        /// an UndefinedVariableException (otherwise).  Uses the standard precedence rules when doing the evaluation.
        /// 
        /// If no undefined variables or divisions by zero are encountered when evaluating 
        /// this Formula, its value is returned.  Otherwise, throws a FormulaEvaluationException  
        /// with an explanatory Message.
        /// </summary>
        public double Evaluate(Lookup lookup)
        {
            Stack<string> valueStack = new Stack<string>();
            Stack<string> operatorStack = new Stack<string>();
        
            foreach(String token in this.formulaStrings)
            {
                if(Regex.IsMatch(token, doublePattern, RegexOptions.IgnorePatternWhitespace))
                {

                    //If the string at the top of the oporator stack is * or /
                    if (!(operatorStack.Count == 0) && Regex.IsMatch(operatorStack.Peek(), @"[\*\/]$"))
                    {
                        PopOnceApplyOperator(operatorStack, valueStack, Convert.ToDouble(token));
                    }
                    else
                    {
                        //Otherwise push the token onto the value stack.
                        valueStack.Push(token);
                    }
                }
                
                if (Regex.IsMatch(token, varPattern))
                {
                    //Try to find the value of the variable.
                    try
                    {
                        double tokenLookupValue = lookup(token);
                    }
                    catch (Exception)
                    {
                        //If no variable is found throw an exception.
                        throw new FormulaEvaluationException("Could not find the value of the variable");
                    }

                    //If the string at the top of the oporator stack is * or /
                    if (!(operatorStack.Count == 0) && Regex.IsMatch(operatorStack.Peek(), @"[\*\/]$"))
                    {
                        PopOnceApplyOperator(operatorStack, valueStack, lookup(token));
                    }
                    else
                    {
                        //Otherwise push the token onto the value stack.
                        valueStack.Push(Convert.ToString(lookup(token)));
                    }
                }

                //If the token is
                if(Regex.IsMatch(token, @"[\+\-]$"))
                {
                    //If teh operator stack is addition or subtraction, apply the operator to the next two values.
                    if (!(operatorStack.Count == 0) && Regex.IsMatch(operatorStack.Peek(), @"[\+\-]$"))
                    {
                        PopTwiceApplyOperator(operatorStack, valueStack);
                    }

                    //Token must be pushed onto the opearator stack.
                    operatorStack.Push(token);
                }

                //If the token is multiplication or division.
                if(Regex.IsMatch(token, @"[\*\/]$"))
                {
                    operatorStack.Push(token);
                }

                //If the token is left parentheses.
                if(Regex.IsMatch(token, lpPattern))
                {
                    operatorStack.Push(token);
                }

                //If the token is right parentheses.
                if(Regex.IsMatch(token, rpPattern))
                {
                    //If the sign is addition or subtraction, apply the operator to the next two values.
                    if (Regex.IsMatch(operatorStack.Peek(), @"[\+\-]$"))
                    {
                        PopTwiceApplyOperator(operatorStack, valueStack);
                    }

                    //Pop the operatior stack to clear '('
                    operatorStack.Pop();

                    //If the next operation is multiply or divide.
                    if(!(operatorStack.Count == 0) && Regex.IsMatch(operatorStack.Peek(), @"[\*\/]$"))
                    {
                        PopTwiceApplyOperator(operatorStack, valueStack);
                    }
                }
            }

            //Once the iteration has finished, two cases remain to check for the final value.
            //If the operator stackis empty, return the value on the value stack as the solution.
            if(operatorStack.Count == 0)
            {
                return Convert.ToDouble(valueStack.Pop());
            }
            //Otherwise either + or - reamain.  Pop the 
            else
            {
                if (Regex.IsMatch(operatorStack.Peek(), @"[\+\-]$"))
                {
                    //apply the operator to the remaining values in the value stack.
                    PopTwiceApplyOperator(operatorStack, valueStack);
                }
            }
            //Return the value as the solution.
            return Convert.ToDouble(valueStack.Pop());
        }


        /* Helper method that will pop the operator stack once and the value stack twice.  The operator that is popped is applied
         * to the two values popped from the value stack.
         * 
         * Two stacks are passed to the method, the operator stack and the value stack.  There is no value returned, the change
         * is made within the stacks that are passed to the method.
         * 
         */
        private void PopTwiceApplyOperator(Stack<string> operatorStack, Stack<string> valueStack)
        {
            //If the operator is addition.
            if (operatorStack.Peek().Equals("+"))
            {
                operatorStack.Pop();
               valueStack.Push(Convert.ToString(Convert.ToDouble(valueStack.Pop()) + (Convert.ToDouble(valueStack.Pop()))));
                return;
            }
            //If operator is subtraction.
            if(operatorStack.Peek().Equals("-"))
            {
                operatorStack.Pop();
                valueStack.Push(Convert.ToString(Convert.ToDouble(valueStack.Pop()) - (Convert.ToDouble(valueStack.Pop()))));
                return;
            }

            //If the operator is multiplication.
            if (operatorStack.Peek().Equals("*"))
            {
                operatorStack.Pop();
                valueStack.Push(Convert.ToString(Convert.ToDouble(valueStack.Pop()) * (Convert.ToDouble(valueStack.Pop()))));
                return;
            }
            //If operator is division.
            if(operatorStack.Peek().Equals("/"))
            {
                operatorStack.Pop();
                valueStack.Push(Convert.ToString(Convert.ToDouble(valueStack.Pop()) / (Convert.ToDouble(valueStack.Pop()))));
                return;
            }
        }
        /* Helper method that will pop the operator stack once and the value stack once.  A double is also passed to the caller.
         * the method will apply the operator to the value that it is passed and the value that is popped from the value stack.
         * 
         * The method retuqires an operator stack and a value stack and a double.  There is no value returned, the change is 
         * made within the stacks that are passed to the method.
         */

        private void PopOnceApplyOperator(Stack<string> operatorStack, Stack<string> valueStack, double token)
        {
            //If the operator is multiplication.
            if (operatorStack.Peek().Equals("*"))
            {
                operatorStack.Pop();
                valueStack.Push(Convert.ToString(Convert.ToDouble(valueStack.Pop()) * (token)));
                return;
            }
            //If operator is division.
            else
            {
                operatorStack.Pop();
                valueStack.Push(Convert.ToString(Convert.ToDouble(valueStack.Pop()) / (token)));
                return;
            }
        }

        /// <summary>
        /// Given a formula, enumerates the tokens that compose it.  Tokens are left paren,
        /// right paren, one of the four operator symbols, a string consisting of a letter followed by
        /// zero or more digits and/or letters, a double literal, and anything that doesn't
        /// match one of those patterns.  There are no empty tokens, and no token contains white space.
        /// </summary>
        private static IEnumerable<string> GetTokens(String formula)
        {
            // Patterns for individual tokens.
            // NOTE:  These patterns are designed to be used to create a pattern to split a string into tokens.
            // For example, the opPattern will match any string that contains an operator symbol, such as
            // "abc+def".  If you want to use one of these patterns to match an entire string (e.g., make it so
            // the opPattern will match "+" but not "abc+def", you need to add ^ to the beginning of the pattern
            // and $ to the end (e.g., opPattern would need to be @"^[\+\-*/]$".)
            String lpPattern = @"\(";
            String rpPattern = @"\)";
            String opPattern = @"[\+\-*/]";
            String varPattern = @"[a-zA-Z][0-9a-zA-Z]*";

            // PLEASE NOTE:  I have added white space to this regex to make it more readable.
            // When the regex is used, it is necessary to include a parameter that says
            // embedded white space should be ignored.  See below for an example of this.
            String doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: e[\+-]?\d+)?";
            String spacePattern = @"\s+";

            // Overall pattern.  It contains embedded white space that must be ignored when
            // it is used.  See below for an example of this.  This pattern is useful for 
            // splitting a string into tokens.
            String splittingPattern = String.Format("({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})",
                                            lpPattern, rpPattern, opPattern, varPattern, doublePattern, spacePattern);

            // Enumerate matching tokens that don't consist solely of white space.
            // PLEASE NOTE:  Notice the second parameter to Split, which says to ignore embedded white space
            /// in the pattern.
            foreach (String s in Regex.Split(formula, splittingPattern, RegexOptions.IgnorePatternWhitespace))
            {
                if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline))
                {
                    yield return s;
                }
            }
        }
    }

    /// <summary>
    /// A Lookup method is one that maps some strings to double values.  Given a string,
    /// such a function can either return a double (meaning that the string maps to the
    /// double) or throw an UndefinedVariableException (meaning that the string is unmapped 
    /// to a value. Exactly how a Lookup method decides which strings map to doubles and which
    /// don't is up to the implementation of the method.
    /// </summary>
    public delegate double Lookup(string var);

    /// <summary>
    /// Used to report that a Lookup delegate is unable to determine the value
    /// of a variable.
    /// </summary>
    [Serializable]
    public class UndefinedVariableException : Exception
    {
        /// <summary>
        /// Constructs an UndefinedVariableException containing whose message is the
        /// undefined variable.
        /// </summary>
        /// <param name="variable"></param>
        public UndefinedVariableException(String variable)
            : base(variable)
        {
        }
    }

    /// <summary>
    /// Used to report syntactic errors in the parameter to the Formula constructor.
    /// </summary>
    [Serializable]
    public class FormulaFormatException : Exception
    {
        /// <summary>
        /// Constructs a FormulaFormatException containing the explanatory message.
        /// </summary>
        public FormulaFormatException(String message) : base(message)
        {
        }
    }

    /// <summary>
    /// Used to report errors that occur when evaluating a Formula.
    /// </summary>
    [Serializable]
    public class FormulaEvaluationException : Exception
    {
        /// <summary>
        /// Constructs a FormulaEvaluationException containing the explanatory message.
        /// </summary>
        public FormulaEvaluationException(String message) : base(message)
        {
        }
    }
}
