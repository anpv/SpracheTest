namespace ConsoleApplication1
{
    using System;
    using Scripts;

    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var str = "(1 + 1) * 2 = 4 or null + 1 is null and 1 + 1 in (1, 2, 3)";
                var expected = "((((1 + 1) * 2) = 4) or (((null + 1) is null) and ((1 + 1) in (1, 2, 3))))";
                var actual = ScriptParser.ParseExpression(str).ToString();
                Console.WriteLine(actual);
                if (expected != actual)
                {
                    throw new Exception(
                        string.Format(
                            "Expected not equal actual:{1}{0}{1}!={1}{2}",
                            expected,
                            Environment.NewLine,
                            actual));
                }
                
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.ToString());
            }
        }
    }
}
