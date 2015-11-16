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
        
        public override Node eval(Node exp, Environment env)
        {
            // Define new local Environment for the Let expression
                Environment letEnv = new Environment(env);
            
            // Target relevant Nodes
                Node rootVarList = exp.getCdr().getCar();
                Node rootProcedure = exp.getCdr().getCdr().getCar();
                
            // Loop through Variables for definition
                while (rootVarList != Nil.getInstance())
                {
                    // Construct a new Define:Special Node, and eval it :D
                        Node currentDefine = new Cons(new Ident("define"), rootVarList.getCar());
                        currentDefine.eval(currentDefine, letEnv);
                    
                    // Move rootVarList to the next variable
                        rootVarList = rootVarList.getCdr();
                }
            
            // Eval and return the Let's procedure
                return rootProcedure.eval(rootProcedure, letEnv);
        }
    }
}


