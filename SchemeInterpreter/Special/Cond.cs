// Cond -- Parse tree node strategy for printing the special form cond

using System;

namespace Tree
{
    public class Cond : Special
    {
	public Cond() { }

        public override void print(Node t, int n, bool p)
        { 
            Printer.printCond(t, n, p);
        }
        
        public override Node eval(Node exp, Environment env)
        {
            Node node = exp.getCdr();
            while((!(node.getCar()).getCar().eval(env).getBoolVal()) && (!node.isNull()))
            {
                node = node.getCdr();
            }
            if(node.isNull())
            {
                return new Nil();
            }
            else
            {
                return (node.getCar().getCdr().getCar().eval(env));
            }

            //Console.Error.WriteLine("Error: Eval not implemented for Cond:Special");
            //return Nil.getInstance();
        }
    }
}


