// If -- Parse tree node strategy for printing the special form if

using System;

namespace Tree
{
    public class If : Special
    {
	public If() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printIf(t, n, p);
        }
        
        public override Node eval(Node exp, Environment env)
        {
            Console.Error.WriteLine("Error: Eval not implemented for If:Special");
            return Nil.getInstance();
        }
    }
}

