// Let -- Parse tree node strategy for printing the special form let

using System;

namespace Tree
{
    public class Let : Special
    {
	public Let() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printLet(t, n, p);
        }
        
        public override Node eval(Node exp, Node env)
        {
            Console.Error.WriteLine("Error: Eval not implemented for Let:Special");
            return Nil.getInstance();
        }
    }
}


