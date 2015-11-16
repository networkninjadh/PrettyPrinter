// Closure.java -- the data structure for function closures

// Class Closure is used to represent the value of lambda expressions.
// It consists of the lambda expression itself, together with the
// environment in which the lambda expression was evaluated.

// The method apply() takes the environment out of the closure,
// adds a new frame for the function call, defines bindings for the
// parameters with the argument values in the new frame, and evaluates
// the function body.

using System;

namespace Tree
{
    public class Closure : Node
    {
        private Node fun;		             // a lambda expression
        private Environment containingEnv;	 // the environment in which the function was defined

        public Closure(Node f, Environment e)	{ fun = f;  containingEnv = e; }

        public Node getFun()		{ return fun; }
        public Environment getEnv()	{ return containingEnv; }

        public override bool isProcedure()	{ return true; }

        public override void print(int n) {
            // there got to be a more efficient way to print n spaces
            for (int i = 0; i < n; i++)
                Console.Write(' ');
            Console.WriteLine("#{Procedure");
            if (fun != null)
                fun.print(Math.Abs(n) + 4);
            for (int i = 0; i < Math.Abs(n); i++)
                Console.Write(' ');
            Console.WriteLine('}');
        }

        public override Node apply (Node args, Environment env)
        {
            
            // Must create new environment within which to execute
                Environment funcEnv = new Environment(containingEnv);
            
            // Define param-arg pairs
                Node parameters = this.fun.getCdr().getCar();
                
                Node paramPivot = parameters;
                Node argsPivot = args;
                
                while(paramPivot != Nil.getInstance())
                {
                    // Not enough arguments for parameters 
                        if(argsPivot == Nil.getInstance())
                        {
                            Console.Error.WriteLine("ERROR: Parameter Mismatch. Too few arguments.");
                            return Nil.getInstance();
                        }
                    
                    // Matching Pairs
                        funcEnv.define(paramPivot.getCar(), argsPivot.getCar());
                        Console.WriteLine();
                        
                    // Move both Pivots
                        paramPivot = paramPivot.getCdr();
                        argsPivot = argsPivot.getCdr();
                }
            
                // Too many arguments for parameters
                    if(argsPivot != Nil.getInstance())
                    {
                        Console.Error.WriteLine("ERROR: Parameter Mismatch. Too many arguments.");
                        return Nil.getInstance();
                    }
            
            // Execute function
                Node body = fun.getCdr().getCdr().getCar();
                
                Node result = body.eval(body, funcEnv);
                
                return result;
        }
        
        public override Node eval(Node exp, Environment env) 
        {
            Console.Error.WriteLine("Error: Closure Cannot be eval()");
            return Nil.getInstance();
        }
    }    
}
