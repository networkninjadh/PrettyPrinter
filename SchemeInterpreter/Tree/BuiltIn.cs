// BuiltIn -- the data structure for built-in functions

// Class BuiltIn is used for representing the value of built-in functions
// such as +.  Populate the initial environment with
// (name, new BuiltIn(name)) pairs.

// The object-oriented style for implementing built-in functions would be
// to include the C# methods for implementing a Scheme built-in in the
// BuiltIn object.  This could be done by writing one subclass of class
// BuiltIn for each built-in function and implementing the method apply
// appropriately.  This requires a large number of classes, though.
// Another alternative is to program BuiltIn.apply() in a functional
// style by writing a large if-then-else chain that tests the name of
// the function symbol.

using System;

namespace Tree
{
    public class BuiltIn : Node
    {
        private Node symbol;            // the Ident for the built-in function

        public BuiltIn(Node s)		{ symbol = s; }

        public Node getSymbol()		{ return symbol; }

        public override bool isProcedure()	{ return true; }

        public override void print(int n)
        {
            // there got to be a more efficient way to print n spaces
            for (int i = 0; i < n; i++)
                Console.Write(' ');
            Console.Write("#{Built-in Procedure ");
            if (symbol != null)
                symbol.print(-Math.Abs(n));
            Console.Write('}');
            if (n >= 0)
                Console.WriteLine();
        }

        // TODO: The method apply() should be defined in class Node
        // to report an error.  It should be overridden only in classes
        // BuiltIn and Closure.
        public /* override */ Node apply (Node args)
        {
           string name = symbol.getName();
           
           // Binary Arithmetic Section
           //  Node args should be a (Regular)Cons:Node with properties:
           //    args.car is a (Regular)Cons:Node
           //       args.car.car is Ident:Node of first
                if (name.Equals("b+"))
                {
                    return Nil.getInstance();
                }
                if (name.Equals("b-"))
                {
                    return Nil.getInstance();
                }
                if (name.Equals("b*"))
                {
                    return Nil.getInstance();
                }
                if (name.Equals("b/"))
                {
                    return Nil.getInstance();
                }
                if (name.Equals("b=")) // Integer Comparison Only
                {
                    return Nil.getInstance();
                }
                if (name.Equals("b<")) // Integer Comparison Only
                {
                    return Nil.getInstance();
                }
                
           // List Built-Ins Section
                if (name.Equals("car"))
                {
                    // Call Cons.getCar() function on single parameter and return
                    return args.getCar().getCar();
                }
                if (name.Equals("cdr"))
                {
                    // Call Cons.getCdr() function on single parameter and return
                    return args.getCar().getCdr();
                }
                if (name.Equals("set-car!"))
                {
                    // Call Cons.setCar() function on first parameter, return Nil Node
                    args.getCar().setCar(args.getCdr().getCar());
                    return Nil.getInstance();
                }
                if (name.Equals("set-cdr!"))
                {
                    // Call Cons.setCdr() function on first parameter, return Nil Node
                    args.getCar().setCdr(args.getCdr().getCar());
                }
                
           // Checks? Built-Ins Section (Single Argument)
                if (name.Equals("symbol?"))
                {
                    return BoolLit.getInstance(args.getCar().isSymbol());
                }
                if (name.Equals("number?"))
                {
                    return BoolLit.getInstance(args.getCar().isNumber());
                }
                if (name.Equals("null?"))
                {
                    return BoolLit.getInstance(args.getCar().isNull());
                }
                if (name.Equals("pair?"))
                {
                    return BoolLit.getInstance(args.getCar().isPair());
                }
                if (name.Equals("procedure?"))
                {
                    return BoolLit.getInstance(args.getCar().isProcedure());
                }
                if (name.Equals("eq?"))
                {
                    // TODO
                    return Nil.getInstance();
                }
                
            return Nil.getInstance();
    	}
        
        public Node eval(Node fun, Environment env) 
        {
            Console.Error.WriteLine("Error: BuiltIn Cannot be eval()");
            return Nil.getInstance();
        }
    }    
}

