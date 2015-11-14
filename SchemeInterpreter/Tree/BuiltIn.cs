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
using Parse;

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
            for (int i = 0; i < n; i++)
                Console.Write(' ');
            Console.Write("#{Built-in Procedure ");
            if (symbol != null)
                symbol.print(-Math.Abs(n));
            Console.Write('}');
            if (n >= 0)
                Console.WriteLine();
        }

        // Node args - should contain a list of already eval'd elements
        public override Node apply (Node args)
        {
           if (args == null)
           {
               return null;
           } 
           
           string name = symbol.getName();
           
           Node arg1 = args.getCar();
           if (arg1 == null || arg1.isNull())
           {
               arg1 = Nil.getInstance();
           }
           
           // Binary Arithmetic Section
           //  Node args should contain TWO IntLit Nodes
           //  Values are located in [args.getCar()] and [args.getCdr().getCar()]
                int val1 = args.getCar().getVal();
                int val2 = args.getCdr().getCar().getVal();
           
                if (name.Equals("b+"))
                {
                    return new IntLit(val1 + val2);
                }
                if (name.Equals("b-"))
                {
                    return new IntLit(val1 - val2);
                }
                if (name.Equals("b*"))
                {
                    return new IntLit(val1 * val2);
                }
                if (name.Equals("b/"))
                {
                    return new IntLit(val1 / val2);
                }
                if (name.Equals("b=")) // Integer Comparison Only
                {
                    if(val1 == val2)
                        return BoolLit.getInstance(true);
                    else
                        return BoolLit.getInstance(false);
                }
                if (name.Equals("b<")) // Integer Comparison Only
                {
                    if(val1 < val2)
                        return BoolLit.getInstance(true);
                    else
                        return BoolLit.getInstance(false);
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
                
           // I/O functions read, write, display, newline
                if (name.Equals("read")) 
                {
                     Scanner scanner = new Scanner(Console.In);
                     TreeBuilder builder = new TreeBuilder();
                     Parser parser = new Parser(scanner, builder);
			         Node a = (Node)parser.parseExp();
			         return a;
		        }
                if (name.Equals("write")) 
                {                    
			         if(arg1 is StringLit)
                     {
                         string withQuotes = "\"" + arg1.getStrVal() + "\"";
                         return new StringLit(withQuotes);
                     }
                     
                     // Else
                     return arg1;
		        }
                if (name.Equals("display")) 
                {
			         return arg1;
		        }
                if (name.Equals("newline")) 
                {
			         return new StringLit("\n");
                }
                
                
            return Nil.getInstance();
    	}
        
        public override Node eval(Node env) 
        {
            Console.Error.WriteLine("Error: BuiltIn Cannot be eval()");
            return Nil.getInstance();
        }
    }    
}

