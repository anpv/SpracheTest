namespace ConsoleApplication1.Scripts
{
    using System.ComponentModel;

    public enum Operator
    {
        [Description("+")]
        Plus,

        [Description("-")]
        Minus,

        [Description("*")]
        Multiply,

        [Description("/")]
        Divide,

        [Description("=")]
        Equal,

        [Description("and")]
        And,

        [Description("or")]
        Or,

        [Description("is")]
        Is,

        [Description("in")]
        In
    }
}
