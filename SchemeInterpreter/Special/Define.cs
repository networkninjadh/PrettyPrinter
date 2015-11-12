// Define -- Parse tree node strategy for printing the special form define

using System;

namespace Tree
{
    public class Define : Special
    {
	public Define() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printDefine(t, n, p);
        }
        
        public override Node eval(Node exp, Environment env)
        {
            Console.Error.WriteLine("Error: Eval not implemented for Define:Special");
            return Nil.getInstance();
        }
    }
}


