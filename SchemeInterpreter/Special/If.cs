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
            Node cond1, cond2, cond3;
            cond1 = exp.getCdr().getCar();                      // If Condition
            cond2 = exp.getCdr().getCdr().getCar();             // Then Clause
            cond3 = exp.getCdr().getCdr().getCdr().getCar();    // Else Clause
            
            bool elseExists = (cond3 != null && cond3 != Nil.getInstance());
            
            // cond3 not guarunteed to exist
            
            
            if(((BoolLit)cond1.eval(cond1, env)).getBoolVal()) // Condition of the If exp is true
            {
                return cond2.eval(cond2, env);
            } 
            else if (elseExists) // An else-clause exists:
            {
                return cond3.eval(cond3, env);
            }
            else // Condition is false and no else-clause
            {
                return Nil.getInstance();
            }
        }
    }
}

