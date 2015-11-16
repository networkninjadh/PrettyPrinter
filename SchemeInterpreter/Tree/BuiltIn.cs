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
using System.Linq;
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
        public override Node apply (Node args, Environment env)
        {
            string[] bArith = {"b+", "b-", "b*", "b/", "b=", "b<"};
            string name = symbol.getName();  
            
            // Binary Arithmetic Section
            //  Node args should contain TWO IntLit Nodes
            //  Values are located in [args.getCar()] and [args.getCdr().getCar()]
            if(bArith.Contains(name))
            {
                if(!intArgCheck(args))
                {
                    return Nil.getInstance();
                }
                                                
                // Local Vars
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
                if (name.Equals("cons"))
                {
                    // Create new Cons node, using two parameters
                    return new Cons(args.getCar(), args.getCdr().getCar());
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
                    // Ident Comparisons
                    if(args.getCar() is Ident)
                    {
                        if(!(args.getCdr().getCar() is Ident))
                            return BoolLit.getInstance(false);
                        
                        string name1 = args.getCar().getName();
                        string name2 = args.getCdr().getCar().getName();
                        
                        if(name1 == name2)
                            return BoolLit.getInstance(true);
                        else
                            return BoolLit.getInstance(false);
                    }
                    else
                    {
                        if(args.getCar() == args.getCdr().getCar())
                            return BoolLit.getInstance(true);
                        else
                            return BoolLit.getInstance(false);
                    }
                }
                
            // I/O Built-In Section
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
                     args.getCar().print(0);
                     
                     return Nil.getInstance();
		        }
                if (name.Equals("display")) 
                {
                    if(args.getCar() is StringLit)
                    {
                        Console.WriteLine(((StringLit)args.getCar()).getStrVal());
                    }
                    else
                    {
                        args.getCar().print(0);
                    }
                    
			        return Nil.getInstance();
		        }
                if (name.Equals("newline")) 
                {
			        Console.WriteLine();
                    
                    return Nil.getInstance();
                }
                
            // Other Built-In Section
                if (name.Equals("interaction-environment"))
                {
                    // Trace environments until reaching Global (not Built-In)
                    
                    Environment current = env;
                    Environment next = current.getParentEnv();
                    
                    // Loop breaks when "next" is the built-in Environment (whose env == null)
                    //  Thus, current is the global Environment
                    while(next.getParentEnv() != null)
                    {
                        current = next;
                        next = current.getParentEnv();
                    }
                    
                    return current;
                }
                
                
            return Nil.getInstance();
    	}
        
        // Helper Method for Binary Arihmetic
        //  Returns TRUE if BOTH ARGS are VALID
        private bool intArgCheck(Node args)
        {
            return (args.getCar().isNumber() && args.getCdr().getCar().isNumber());
        }
        
        public override Node eval(Node exp, Environment env)  
        {
            Console.Error.WriteLine("Error: BuiltIn Cannot be eval()");
            return Nil.getInstance();
        }
    }    
}

