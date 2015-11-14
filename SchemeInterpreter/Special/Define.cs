// Define -- Parse tree node strategy for printing the special form define

/*
    Types of Define:
    
        Variable Definition:
            From Constant       (define x 12)
            From Variable       (define x y)
            From Function       (define x (+ 2 3))
            
        Function Definition:
            From Lambda         (define (funz x) (+ x x))
            From Variable       (define (funz x) (y))

*/

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
            Node root = exp;
            
            Node ident = root.getCdr().getCar();
            
            // VARIABLE DEFINITION: THIS WORKS EXCEPT FOR FUNCTION EVAL (define x (+ 2 2))
            if(ident is Tree.Ident)
            {
                //Test Line: WORKS
                //    Console.WriteLine("Identifier Found!");
                
                Node resultIn = root.getCdr().getCdr().getCar();
                Node resultOut = resultIn.eval(exp, env);                // Calls Regular.eval()
                
                //Test Line: WORKS
                //    Console.WriteLine("Result: " + resultOut);
                    
                ((Environment)env).define(ident, resultOut);
            }
            
            // FUNCTION DEFINITION:
            if(ident is Tree.Cons)
            {
                // function name (Ident)    is CAADR
                ident = root.getCdr().getCar().getCar();
                
                // function parameters      is CDADR
                Node fParams = root.getCdr().getCar().getCdr();
                
                // function body            is CADDR
                Node fBody = root.getCdr().getCdr().getCar();
                
                // Concatenate the parameters and body appropriately
                Node func = new Cons(fParams, new Cons(fBody, Nil.getInstance()));
                
                // Concatenate the lambda label
                Node lambda = new Cons(new Ident("lambda"), func);
                
                /* Test Case to ensure that lambda reflect what Parser creates - CORRECT
                    Console.WriteLine("ROOT: " + lambda.getForm());                                         // Lambda
                    Console.WriteLine("CAR: " + lambda.getCar().getName());                                 // lambda
                    Console.WriteLine("CDR: " + lambda.getCdr().getForm());                                 // Regular
                    Console.WriteLine("CADR: " + lambda.getCdr().getCar());                                 // Regular (Parameters)
                    Console.WriteLine("CAADR: " + lambda.getCdr().getCar().getCar().getName());             // Ident (x)
                    Console.WriteLine("CDDR: " + lambda.getCdr().getCdr().getForm());                       // Regular
                    Console.WriteLine("CADDR: " + lambda.getCdr().getCdr().getCar());                       // Regular (Body)
                    Console.WriteLine("CAADDR: " + lambda.getCdr().getCdr().getCar().getCar().getName());   // Ident (+)
                */
                
                // Create a Closure here...
                Node closure = new Closure(lambda, ((Environment)env));
                
                ((Environment)env).define(ident, closure);
            }

            // DEFINE ALWAYS RETURNS NIL
            return Nil.getInstance();
        }
    }
}


