namespace ConsoleApplication1.Scripts.Ast
{
    using System;

    public class BinaryExpression : AstNode
    {
        public BinaryExpression(Operator op, AstNode left, AstNode right)
        {
            if (op == Operator.Is && right.GetType() != typeof(NullConstant))
            {
                throw new ArgumentException(
                    string.Format(
                        "Incorrect syntax near '{0}'. Missing null.",
                        right.ToString()));
            }

            if (op == Operator.In && right.GetType() != typeof(ExpressionsList))
            {
                throw new ArgumentException(
                    string.Format(
                        "Incorrect syntax near '{0}'. Missing (expr, expr, ...).",
                        right.ToString()));
            }

            this.Operator = op;
            this.Left = left;
            this.Right = right;
        }

        public Operator Operator { get; set; }

        public AstNode Left { get; set; }

        public AstNode Right { get; set; }

        public override string ToString()
        {
            return string.Format(
                "({0} {1} {2})",
                this.Left,
                this.Operator.GetDescription(),
                this.Right);
        }
    }
}
