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
                Node evalArgs   = new Cons(Nil.getInstance(), Nil.getInstance()); // Initially Empty
                Node paramPivot = exp.getCdr();
                Node evalPivot  = evalArgs;
                
                while(paramPivot != Nil.getInstance())
                {                    
                    // Evaluate argument, set result as evalPivot Car
                        evalPivot.setCar(paramPivot.getCar().eval(paramPivot.getCar(), env));
                        
                    // Create new empty Cons Node and set it as evalPivot Cdr
                        evalPivot.setCdr(new Cons(Nil.getInstance(), Nil.getInstance()));
                    
                    // Move paramPivot one position down the right-edge of list
                        paramPivot = paramPivot.getCdr();
                    
                    // Move evalPivot pointer down list
                        evalPivot = evalPivot.getCdr();   
                }
                
            // Make the function call using the evalArgs, store result (maybe not necessary to store)
                Node result = func.apply(evalArgs);
                
            return result;
        }
    }
}


