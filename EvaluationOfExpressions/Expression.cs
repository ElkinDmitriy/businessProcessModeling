using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluationOfExpressions
{
    /*
     * Abstract class with abstract Calc method
     */
    public abstract class Expression
    {
        string InputExpression;                                          // input expression
        List<string> Leksemu;
        List<Operator> Operators;
        List<string> LeksemuRevers;

        public string Source {                                           // property for InputExpression Filed
            get {
                return this.InputExpression;
            }
            set {
                this.InputExpression = value;
            }
        }




        public Expression()
        {
            this.Operators = new List<Operator> {   new Operator { Value = "+", Priority = 2 },
                                                    new Operator { Value = "-", Priority = 2 },
                                                    new Operator { Value = "*", Priority = 3 },
                                                    new Operator { Value = "/", Priority = 3 },
                                                    new Operator { Value = "^", Priority = 4 },
                                                    new Operator { Value = "=", Priority = 0 },
                                                    new Operator { Value = "(", Priority = 1 },
                                                    new Operator { Value = ")", Priority = 1 },

            };
            
        }

        public abstract void Calc();

        protected void FillLeksemu()
        {
            if(this.Leksemu != null) { return;  } // is created


            this.Leksemu = new List<String>();
            this.InputExpression = this.InputExpression.Replace(" ", ""); // deleted all space
            StringBuilder tmpStr = new StringBuilder();
            foreach(char c in InputExpression)
            {
                try
                {
                    Operator tmpOperator = this.Operators.First(o => o.Value == c.ToString()); // perhaps Exception!!!
                    if (tmpStr.Length != 0) { this.Leksemu.Add(tmpStr.ToString()); } //put in Leksemu
                    this.Leksemu.Add(c.ToString());
                    tmpStr = new StringBuilder();
                }
                catch(Exception ex)
                {
                    tmpStr.Append(c.ToString());
                }
            }

            if(tmpStr.Length != 0) {  // in case EndExpression
                this.Leksemu.Add(tmpStr.ToString()); //put in Leksemu
            }

            //==================================================== past "0" where value is negative START SECTION
            List<int> PositionMinus = new List<int>();  
            for(int i=0; i<this.Leksemu.Count; i++)
            {
                if(this.Leksemu[i] == "-") 
                {
                    if(i == 0)
                    {
                        PositionMinus.Add(i);
                    }
                    else if(this.Operators.Find(o => o.Value == this.Leksemu[i - 1]) != null)
                    {
                        PositionMinus.Add(i);
                    }
                }
            }

            int offset = 0;
            foreach(int item in PositionMinus)
            {
                this.Leksemu.Insert(item + offset, "0.0");
                offset = offset + 1;
            }

            //===================================================== past "0" where value is negative END SECTION

        }

        protected void FillLeksemuRevers()
        {
            if(this.LeksemuRevers != null) { return; } // is created

            FillLeksemu();

            //need ADD EXCEPTION if Leksemu == null or is empty;!!!!

            Stack<Operator> WorkStack = new Stack<Operator>();

            this.LeksemuRevers = new List<string>();
            foreach(string leks in this.Leksemu)
            {
                Operator tmpOperator = this.Operators.Find(o => o.Value == leks); // check is operator
                if(tmpOperator == null) // leks is not operator
                {
                    this.LeksemuRevers.Add(leks);
                }
                else // leks is operator
                {
                    if(tmpOperator.Value == "(")
                    {
                        WorkStack.Push(tmpOperator);
                    }
                    else
                    {
                        if(tmpOperator.Value == ")")
                        {
                            while(WorkStack.Peek().Value != "(")
                            {
                                this.LeksemuRevers.Add(WorkStack.Pop().Value);
                            }
                            WorkStack.Pop(); // Pop operator "("
                        }
                        else 
                        {
                            if (WorkStack.Count != 0)
                            {
                                while (WorkStack.Peek().Priority > tmpOperator.Priority)
                                {
                                    this.LeksemuRevers.Add(WorkStack.Pop().Value);
                                }
                            }
                            WorkStack.Push(tmpOperator);
                        }
                    }
                }
            }

            while(WorkStack.Count != 0)
            {
                this.LeksemuRevers.Add(WorkStack.Pop().Value);
            }
            
        }

        
    }
}
