// Set -- Parse tree node strategy for printing the special form set!

using System;

namespace Tree
{
    public class Set : Special
    {
	public Set() { }
	
        public override void print(Node t, int n, bool p)
        {
            Printer.printSet(t, n, p);
        }
        
        public override Node eval(Node exp, Environment env)
        {
            Node identity = exp.getCdr().getCar();
            Node valueIdent = exp.getCdr().getCdr().getCar();
            Node actualValue = valueIdent.eval(valueIdent, env);
            
            env.assign(identity, actualValue);
            
            return Nil.getInstance();
        }
    }
}

