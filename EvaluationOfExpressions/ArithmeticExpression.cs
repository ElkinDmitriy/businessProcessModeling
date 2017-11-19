using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluationOfExpressions
{
    public class ArithmeticExpression: Expression
    {
        public override void Calc()
        {
            throw new NotImplementedException();
        }

        public void WrapFillLeksemu() { this.FillLeksemuRevers(); }
    }
}
