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
            Node cond1, cond2, cond3; //cond then else
            cond1 = exp.getCdr().getCar();
            if((BoolLit)cond1.eval(env)getBoolVal())
            {
                cond2 = exp.getCdr().getCdr().getCar();
                return cond2.eval(env);
            } 
            else
            {
                cond3 = exp.getCdr().getCdr().getCdr().getCar();
                return cond3.eval(env);
            }
            Console.Error.WriteLine("Error: Eval not implemented for If:Special");
            return Nil.getInstance();
        }
    }
}

