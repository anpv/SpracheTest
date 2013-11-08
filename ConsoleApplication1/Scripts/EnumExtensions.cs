namespace ConsoleApplication1.Scripts
{
    using System;
    using System.ComponentModel;
    using System.Reflection;

    internal static class EnumExtensions
    {
        public static string GetDescription(this Enum source)
        {
            return source
                .GetType()
                .GetField(source.ToString())
                .GetCustomAttribute<DescriptionAttribute>()
                .Description;
        }
    }
}