// SPP -- The main program of the Scheme pretty printer.

using System;
using Parse;
using Tokens;
using Tree;

public class Scheme4101
{
    public static int Main(string[] args)
    {
        
        // Create scanner that reads from standard input
            Scanner scanner = new Scanner(Console.In);
        
        // Args Check
            if (args.Length > 1 ||
                (args.Length == 1 && ! args[0].Equals("-d")))
            {
                Console.Error.WriteLine("Usage: mono SPP [-d]");
                return 1;
            }
        
        // Debug Option
            if (args.Length == 1 && args[0].Equals("-d"))
            {
                Console.Write("Scheme 4101> ");
                Token tok = scanner.getNextToken();
                while (tok != null)
                {
                    TokenType tt = tok.getType();
    
                    Console.Write(tt);
                    if (tt == TokenType.INT)
                        Console.WriteLine(", intVal = " + tok.getIntVal());
                    else if (tt == TokenType.STRING)
                        Console.WriteLine(", stringVal = " + tok.getStringVal());
                    else if (tt == TokenType.IDENT)
                        Console.WriteLine(", name = " + tok.getName());
                    else
                        Console.WriteLine();
    
                    Console.Write("Scheme 4101> ");
                    tok = scanner.getNextToken();
                }
                return 0;
            }

        // Create parser
            TreeBuilder builder = new TreeBuilder();
            Parser parser = new Parser(scanner, builder);
            Node root;

        // Create the built-in environment
            Tree.Environment biEnv = new Tree.Environment();
        
        // Built-In Binary Arithmetic Functions
            Ident funcName = new Ident("b+");
                biEnv.define(funcName, new BuiltIn(funcName));
            funcName = new Ident("b-");
                biEnv.define(funcName, new BuiltIn(funcName));
            funcName = new Ident("b*");
                biEnv.define(funcName, new BuiltIn(funcName));
            funcName = new Ident("b/");
                biEnv.define(funcName, new BuiltIn(funcName));
            funcName = new Ident("b=");
                biEnv.define(funcName, new BuiltIn(funcName));
            funcName = new Ident("b<");
                biEnv.define(funcName, new BuiltIn(funcName));
            
        // Built-In IO Functions
            funcName = new Ident("read");
                biEnv.define(funcName, new BuiltIn(funcName));
            funcName = new Ident("write");
                biEnv.define(funcName, new BuiltIn(funcName));
            funcName = new Ident("display");
                biEnv.define(funcName, new BuiltIn(funcName));
            funcName = new Ident("newline");
                biEnv.define(funcName, new BuiltIn(funcName));
            
        // Other Built-In Functions
            funcName = new Ident("car");
                biEnv.define(funcName, new BuiltIn(funcName));
            funcName = new Ident("cdr");
                biEnv.define(funcName, new BuiltIn(funcName));
            funcName = new Ident("cons");
                biEnv.define(funcName, new BuiltIn(funcName));
            funcName = new Ident("set-car!");
                biEnv.define(funcName, new BuiltIn(funcName));
            funcName = new Ident("set-cdr!");
                biEnv.define(funcName, new BuiltIn(funcName));
            funcName = new Ident("symbol?");
                biEnv.define(funcName, new BuiltIn(funcName));
            funcName = new Ident("number?");
                biEnv.define(funcName, new BuiltIn(funcName));
            funcName = new Ident("null?");
                biEnv.define(funcName, new BuiltIn(funcName));
            funcName = new Ident("pair?");
                biEnv.define(funcName, new BuiltIn(funcName));
            funcName = new Ident("eq?");
                biEnv.define(funcName, new BuiltIn(funcName));
            funcName = new Ident("procedure?");
                biEnv.define(funcName, new BuiltIn(funcName));
            
        Console.Error.WriteLine("All BI Functions Created!");

        // Create Top-Level Environment
            Tree.Environment env = new Tree.Environment(biEnv);
        
            /* TEST CASE: VARIABLE DEFINITION (WORKS)
                env.define(new Ident("x"), new IntLit(12));
                Console.WriteLine("Finding 'x':");
                env.lookup(new Ident("x")).print(0);
            */
            
            /* TEST CASE: BOOLEAN CHECKS
                env.define(new Ident("x"), new IntLit(12));
                bool status = env.lookup(new Ident("x")).isBool();
                Console.WriteLine("Is 'x' a Boolean? " + status);
            */
            
        
        // Initial Prompt & Parse
            Console.Write("Scheme > ");
            root = (Node) parser.parseExp();
        
        // Debug Section: Pre-Eval
                //Console.WriteLine("ROOT: " + root.getForm());
                //Console.WriteLine("CAR: " + root.getCar().getName());
                //Console.WriteLine("CDR: " + root.getCdr().getForm());
                //Console.WriteLine("CADR: " + root.getCdr().getCar().getForm()); // Parameters
                //Console.WriteLine("CAADR: " + root.getCdr().getCar().getCar().getName()); // x
                //Console.WriteLine("CDDR: " + root.getCdr().getCdr().getForm());
                //Console.WriteLine("CADDR: " + root.getCdr().getCdr().getCar()); // Body
                //Console.WriteLine("CAADDR: " + root.getCdr().getCdr().getCar().getCar().getName()); // +
        
        
        // Prompt, Read, Eval, Print Loop
        while (root != null) 
            {
                Node evalNode = root.eval(root, env);
                evalNode.print(0);
                    // Debug Section: Post-Eval
                        // Console.WriteLine();
                        //
                        //
                Console.Write("Scheme > ");
                root = (Node) parser.parseExp();
            }
        
        
        return 0;
    }
}
