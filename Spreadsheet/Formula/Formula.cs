// Skeleton written by Joe Zachary for CS 3500, January 2017
// Completed by Nathan  Herrmann for grading in CS 3500, January 2017 final commit
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
    public struct Formula
    {
        //Object Variables
        private IEnumerable<String> formulaStrings;

        //Patterns used for recognition of tokens.
        private const String lpPattern = @"^\($";
        private const String rpPattern = @"\)$";
        private const String opPattern = @"[\+\-*/]$";
        private const String varPattern = @"^[a-zA-Z][0-9a-zA-Z]*$";
        private const String doublePattern = @"^(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: e[\+-]?\d+)?$";
        
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

            if(formula == null)
            {
                throw new FormulaFormatException("Formula cannot be null");
            }
            this.formulaStrings  = GetTokens(formula);

            //Syntax check variables
            Boolean hasToken = false;
            Boolean firstToken = false;
            Boolean lastToken = false;
            Boolean followOpeningOperator = false;
            Boolean followNumberVariableClosing = false;
            int lpCount = 0;
            int rpCount = 0;

            //Iterator that loops through each token.
            foreach (String token in formulaStrings)
            {
                //If the token is recognized as valid. (Valid tokens are described in the Formula class. and code is provided to detect them.)
                if (Regex.IsMatch(token, doublePattern, RegexOptions.IgnorePatternWhitespace) ||
                    Regex.IsMatch(token, varPattern) || Regex.IsMatch(token, opPattern) ||
                    Regex.IsMatch(token, rpPattern) || Regex.IsMatch(token, lpPattern))
                {
                    //Change boolean to reflect presence of at least one token.
                    hasToken = true;

                    //The first token of a formula must be a number, a variable, or an opening parenthesis.
                    //Check for the first token.  If it is not a number, variable or an opening parenthesis, throw exception.
                    if (firstToken == false)
                    {
                        if (Regex.IsMatch(token, doublePattern, RegexOptions.IgnorePatternWhitespace) ||
                           Regex.IsMatch(token, varPattern) || Regex.IsMatch(token, lpPattern))
                        {
                            firstToken = true;
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
                        lastToken = true;
                    }
                    else
                    {
                        //If the last token seen is not a valid last token, then the iteration will end with an error.
                        lastToken = false;
                    }
                    
                    //Any token that immediately follows an opening parenthesis or an operator must be either a number, a variable, or an opening parenthesis.
                    if (followOpeningOperator == true)
                    {
                        if(Regex.IsMatch(token, doublePattern, RegexOptions.IgnorePatternWhitespace) ||
                           Regex.IsMatch(token, varPattern) || Regex.IsMatch(token, lpPattern))
                        {
                            followOpeningOperator = false;
                        }
                        else
                        {
                            throw new FormulaFormatException("The token following an opening parenthesis or an oporator is not valid");
                        }
                    }
                    
                    //Set check for next token following a left parentheses or an operation.
                    if(Regex.IsMatch(token, lpPattern) || Regex.IsMatch(token,opPattern))
                    {
                        followOpeningOperator = true;
                    }
                    else
                    {
                        followOpeningOperator = false;
                    }

                    //Any token that immediately follows a number, a variable, or a closing parenthesis must be either an operator or a closing parenthesis.
                    if (followNumberVariableClosing == true)
                    {
                        if(Regex.IsMatch(token, opPattern) || Regex.IsMatch(token, rpPattern))
                        {
                            followNumberVariableClosing = false;
                        }
                        else
                        {
                            throw new FormulaFormatException("The token following a number, variable or closing parenthesis is not valid");
                        }
                    }

                    //Set check for the next token following a double, variable or right parentheses.
                    if(Regex.IsMatch(token, doublePattern, RegexOptions.IgnorePatternWhitespace) ||
                        Regex.IsMatch(token, varPattern) || Regex.IsMatch(token, rpPattern))
                    {
                        followNumberVariableClosing = true;
                    }
                    else
                    {
                        followNumberVariableClosing = false;
                    }

                    //When reading tokens from left to right, at no point should the number of closing parentheses seen so far be greater than the number of opening parentheses seen so far.
                    if (Regex.IsMatch(token, lpPattern))
                    {
                        lpCount++;
                    }
                    //If the token is a right parentheses, incrememnt the right parentheses count
                    if (Regex.IsMatch(token, rpPattern))
                    {
                        rpCount++;
                    }
                    
                    //If there are more right parentheses than left, throw an exception.
                    if(rpCount > lpCount)
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
            if(lastToken == false)
            {
                throw new FormulaFormatException("The last token is not a valid last input");
            }

            //The total number of opening parentheses must equal the total number of closing parentheses.
            if (rpCount != lpCount)
            {
                throw new FormulaFormatException("The number of opening and closing parentheses is not equal");
            }
        }

        /// <summary>
        /// A second constructor that takes a Normalizer and a Valudator for a argument.
        /// The normalizer is used to convert variables into a canonical form.  The Validator is used to
        /// impose extra restrictions past what is already required of the first constructor.
        /// 
        /// Constructor chaining is used to ensure that the requirements of the first constructor are met.
        /// If any argument is null then an ArgumentNullException is thrown.
        /// </summary>
        /// <param name="formula"></param>
        /// <param name="N"></param>
        /// <param name="V"></param>
        public Formula(String formula, Normalizer N, Validator V): this(formula)
        {
            //If ano
            if(formula == null || N == null || V == null)
            {
                throw new FormulaFormatException("Parameter cannot be null");
            }
            //Gets each token from the formula.
            this.formulaStrings = GetTokens(formula);
            string normalizedString = "";
            
            //Each token is normalized and placed into a new string.
            foreach(string token in this.formulaStrings)
            {
                if (Regex.IsMatch(token, varPattern))
                {
                    normalizedString = normalizedString + N(token);
                }
                else
                {
                    normalizedString = normalizedString + token;
                }
            }

            //The string is checked against the first constructor for errors afer normalization.
            new Formula(normalizedString);

            //Tokens are extracted from the normalized string.
            this.formulaStrings = GetTokens(normalizedString);

            //Tokens are checked against the validator.
            foreach (string token in this.formulaStrings)
            {
                //If validator returns false then throws a FormulaFormatException.
                if(V(token) == false)
                {
                    throw new FormulaFormatException("Validated string does not ");
                }

            }
        }

        /// <summary>
        /// Returns a set of all tokens in the normalized formula string.
        /// </summary>
        /// <returns></returns>
        public ISet<string> GetVariables()
        {
            HashSet<string> toReturn = new HashSet<string>();

            if(this.formulaStrings == null)
            {
                return toReturn;
            }

            foreach(string token in this.formulaStrings)
            {
                if (Regex.IsMatch(token, varPattern))
                {
                    toReturn.Add(token);
                }
            }

            return toReturn;
        }


        /// <summary>
        /// Overrides the ToString method and returns a string version of the tokens.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string toReturn = "";

            if(formulaStrings == null)
            {
                return "0";
            }

            foreach(string token in this.formulaStrings)
            {
                toReturn = toReturn + token;
            }

            return toReturn;
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
            if(lookup == null)
            {
                throw new FormulaFormatException("Parameter cannot be nul");
            }

            if(this.formulaStrings == null)
            {
                return 0;
            }

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
                    //If the top of the operator stack is addition or subtraction, apply the operator to the next two values.
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

         ///<summary>
         ///Helper method that will pop the operator stack once and the value stack twice.  The operator that is popped is applied
         ///to the two values popped from the value stack.
         ///
         ///Two stacks are passed to the method, the operator stack and the value stack.  There is no value returned, the change
         ///is made within the stacks that are passed to the method.
         ///
         ///
         ///<summary>
        private void PopTwiceApplyOperator(Stack<string> operatorStack, Stack<string> valueStack)
        {
            String varOperator = operatorStack.Pop();
            double varB = Convert.ToDouble(valueStack.Pop());
            double varA = Convert.ToDouble(valueStack.Pop());

            //If the operator is addition.
            if (varOperator.Equals("+"))
            {
               
               valueStack.Push(Convert.ToString(varA + varB));
                return;
            }
            //If operator is subtraction.
            if(varOperator.Equals("-"))
            {
                
                valueStack.Push(Convert.ToString(varA - varB));
                return;
            }

            //If the operator is multiplication.
            if (varOperator.Equals("*"))
            {
                
                valueStack.Push(Convert.ToString(varA * varB));
                return;
            }
            //If operator is division.
            if(varOperator.Equals("/"))
            {

                if(varB == 0)
                {
                    throw new FormulaEvaluationException("Can't divide by 0");
                }
                valueStack.Push(Convert.ToString(varA / varB));
                return;
            }
        }
        ///<summary>
        ///Helper method that will pop the operator stack once and the value stack once.  A double is also passed to the caller.
        /// the method will apply the operator to the value that it is passed and the value that is popped from the value stack.
        /// 
        ///The method retuqires an operator stack and a value stack and a double.  There is no value returned, the change is 
        ///made within the stacks that are passed to the method.
        ///
        ///<summary>
        private void PopOnceApplyOperator(Stack<string> operatorStack, Stack<string> valueStack, double token)
        {
            string varOperator = operatorStack.Pop();
            double varB = token;
            double varA = Convert.ToDouble(valueStack.Pop());

            //If the operator is multiplication, pop the stacks and multiply the values.
            if (varOperator.Equals("*"))
            {   
                valueStack.Push(Convert.ToString(varA * varB));
                return;
            }
            //If the operator is division, pop the stacks and divide the values.
            else
            {
                if (varB == 0)
                {
                    throw new FormulaEvaluationException("Can't divide by 0");
                }
                valueStack.Push(Convert.ToString(varA / varB));
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
    /// The purpose of a Normalizer is to convert variables into a canonical form
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public delegate string Normalizer(string s);

    /// <summary>
    /// The purpose of a Validator is to impose extra restrictions on the validity of a 
    /// variable, beyond the ones already built into the Formula definition.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public delegate bool Validator(string s);

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
