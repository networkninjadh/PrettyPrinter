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
            // Root of the First Clause
                Node clauseRoot = exp.getCdr();
                
            // And the "if" clause itself
                Node clauseIf = clauseRoot.getCar().getCar();
            
            // While: target clause is not true AND not the end of the expression
                while((!(((BoolLit)clauseIf.eval(clauseIf, env)).getBoolVal())) && (clauseRoot != Nil.getInstance()))
                {
                    clauseRoot = clauseRoot.getCdr();
                    clauseIf = clauseRoot.getCar().getCar();
                }
                
            // clauseRoot is either a Nil Node or the true clause
                if(clauseRoot.isNull())
                {
                    return Nil.getInstance();
                }
                else
                {
                    Node expression = clauseRoot.getCar().getCdr().getCar();
                    return expression.eval(expression, env); 
                }
        }
    }
}


