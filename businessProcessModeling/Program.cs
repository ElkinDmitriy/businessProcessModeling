using EvaluationOfExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace businessProcessModeling
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "c = a * cdb - g";
            ArithmeticExpression ArithExpression1 = new ArithmeticExpression();
            ArithExpression1.Source = str;
            ArithExpression1.WrapFillLeksemu();
        }
    }
}
