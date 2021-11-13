using System;

namespace delegates
{
    // declaration
    delegate void PrintMessage(string text);
    delegate T Print<T>(T param1);

    class Program
    {
        // method to point to
        public static void WriteText(string text) => Console.WriteLine($"Text: {text}");
        public static void ReverseWriteText(string text) => Console.WriteLine($"Text in reverse: {Reverse(text)}");
        public static string ReverseText(string text) => Reverse(text);

        static void Main(string[] args)
        {
            // instantiate
            var delegate1 = new PrintMessage(WriteText);
            var delegate2 = new PrintMessage(ReverseWriteText);
            var delegate3 = new Print<string>(ReverseText);

            Action<string> executeReverseWrite = ReverseWriteText;
            Func<string, string> executeReverse = ReverseText;

            // multicast
            var multicastDelegate1 = delegate1 + delegate2;
            var multicastDelegate2 = delegate1;
            multicastDelegate2 += delegate2;

            // invoke
            multicastDelegate1.Invoke("Go ahead, make my day.");
            multicastDelegate1("You're gonna need a bigger boat.");
            multicastDelegate2.Invoke("Go ahead, make my day.");
            multicastDelegate2("You're gonna need a bigger boat.");

            Console.WriteLine(delegate3("I'll be back."));

            executeReverseWrite("Are you not entertained?");
            Console.WriteLine(executeReverse("Are you not entertained?")); 
        }

        private static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
