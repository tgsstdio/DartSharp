﻿namespace DartSharp.Expressions
{
    using System.Collections.Generic;

    public class ArrayExpression : IExpression
    {
        private IEnumerable<IExpression> expressions;

        public ArrayExpression(IEnumerable<IExpression> expressions)
        {
            this.expressions = expressions;
        }

        public IEnumerable<IExpression> Expressions { get { return this.expressions; } }

        public object Evaluate(Context context)
        {
            IList<object> values = new List<object>();

            if (this.expressions != null)
                foreach (var argument in this.expressions)
                    values.Add(argument.Evaluate(context));

            return values;
        }
    }
}

