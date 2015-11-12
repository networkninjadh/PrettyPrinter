// Regular -- Parse tree node strategy for printing regular lists

using System;

namespace Tree
{
    public class Regular : Special
    {
        public Regular() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printRegular(t, n, p);
        }
        
        public override Node eval(Node env)
        {
            Console.Error.WriteLine("Error: Eval has not been implemented for Regular:Special");
            return Nil.getInstance();
        }
    }
}


