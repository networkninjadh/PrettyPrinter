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
        
        public override Node eval(Node exp, Environment env)
        {            
            Node identifier = exp.getCar();
            Node func       = identifier.eval(identifier, env);
            
            // Unintended Case (?Error?)
                if(func.isProcedure() == false)
                {
                    Console.Error.WriteLine("ERROR: Regular eval called with a non-function Car");
                }
            
            // Evaluate the provided params
                Node paramPivot = exp.getCdr();
                Node evalArgs;
                Node evalPivot;
                
            //  If Function has no parameters, argument list is Nil
                if(paramPivot == Nil.getInstance())
                {
                    return func.apply(Nil.getInstance(), env);
                }
                
            // Else begin eval and assignment
            //  Initial Cons:Node is (Arg1 Value, Nil)
                evalArgs = new Cons((paramPivot.getCar().eval(paramPivot.getCar(), env)), Nil.getInstance());
                evalPivot = evalArgs;
                paramPivot = paramPivot.getCdr();
            
            // Loop for additional args
                while(paramPivot != Nil.getInstance())
                {
                    // Create new Cons Node (ArgN Value, Nil)
                    // Unnecessary storage, but prevents confusion
                        Node temp = new Cons((paramPivot.getCar().eval(paramPivot.getCar(), env)), Nil.getInstance());
                    
                    // Set right-side as newly created Node                        
                        evalPivot.setCdr(temp);
                    
                    // Move paramPivot one position down the right-edge of list
                        paramPivot = paramPivot.getCdr();
                    
                    // Move evalPivot pointer down list
                        evalPivot = evalPivot.getCdr();   
                }
                
            // Make the function call using the evalArgs, return result
                return func.apply(evalArgs, env);
        }
    }
}


