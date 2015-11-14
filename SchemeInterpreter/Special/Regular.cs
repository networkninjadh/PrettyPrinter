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
        
        public override Node eval(Node exp, Node env)
        {
            Console.WriteLine("Reached Regular Eval!");
            return exp.getCar().eval(env);
            
            // Evaluate all elements of the list
            // Then call apply (using those as args)
        }
    }
}


