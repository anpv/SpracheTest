namespace ConsoleApplication1.Scripts
{
    using Ast;
    using Sprache;

    public static class ScriptParser
    {
        private static readonly Parser<char> Comma =
            Parse
                .Char(',')
                .Token();

        private static readonly Parser<char> OpenParenthesis =
            Parse
                .Char('(')
                .Token();

        private static readonly Parser<char> CloseParenthesis =
            Parse
                .Char(')')
                .Token();

        private static readonly Parser<AstNode> Integer =
            Parse
                .Number
                .Select(n => new IntegerConstant(int.Parse(n)))
                .Token();

        private static readonly Parser<AstNode> Null =
            Parse
                .IgnoreCase("null")
                .Return(new NullConstant())
                .Token();

        private static readonly Parser<Operator> Plus =
            MakeOperator(Operator.Plus);

        private static readonly Parser<Operator> Minus =
            MakeOperator(Operator.Minus);

        private static readonly Parser<Operator> Multiply =
            MakeOperator(Operator.Multiply);

        private static readonly Parser<Operator> Divide =
            MakeOperator(Operator.Divide);

        private static readonly Parser<Operator> Eq =
            MakeOperator(Operator.Equal);

        private static readonly Parser<Operator> And =
            MakeOperator(Operator.And);

        private static readonly Parser<Operator> Or =
            MakeOperator(Operator.Or);

        private static readonly Parser<Operator> Is =
            MakeOperator(Operator.Is);

        private static readonly Parser<Operator> In =
            MakeOperator(Operator.In);

        private static readonly Parser<AstNode> InExpressions =
            Parse
                .Ref(() => Expression)
                .DelimitedBy(Comma)
                .Contained(OpenParenthesis, CloseParenthesis)
                .Select(values => new ExpressionsList(values));

        private static readonly Parser<AstNode> ExpressionInParenthesis =
            Parse
                .Ref(() => Expression)
                .Contained(OpenParenthesis, CloseParenthesis);

        private static readonly Parser<AstNode> Primary =
            Integer
                .Or(Null)
                .Or(ExpressionInParenthesis)
                .Or(InExpressions)
                .Token();

        private static readonly Parser<AstNode> Multiplicative =
            Parse
                .ChainOperator(Multiply.Or(Divide), Primary, MakeBinary);

        private static readonly Parser<AstNode> Additive =
            Parse
                .ChainOperator(Plus.Or(Minus), Multiplicative, MakeBinary);

        private static readonly Parser<AstNode> Equality =
            Parse
                .ChainOperator(Eq.Or(Is).Or(In), Additive, MakeBinary);

        private static readonly Parser<AstNode> ConditionalAnd =
            Parse
                .ChainOperator(And, Equality, MakeBinary);

        private static readonly Parser<AstNode> ConditionalOr =
            Parse
                .ChainOperator(Or, ConditionalAnd, MakeBinary);

        private static readonly Parser<AstNode> Expression =
            ConditionalOr;

        public static AstNode ParseExpression(string str)
        {
            return Expression.End().Parse(str);
        }

        private static Parser<Operator> MakeOperator(Operator op)
        {
            return MakeOperator(op.GetDescription(), op);
        }

        private static Parser<Operator> MakeOperator(string str, Operator op)
        {
            return Parse.String(str).Return(op).Token();
        }

        private static AstNode MakeBinary(Operator op, AstNode left, AstNode right)
        {
            return new BinaryExpression(op, left, right);
        }
    }
}
