using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Irony.Ast;
using Irony.Parsing;
using Proyecto1.ast;
using Proyecto1.Instrucciones;
using Proyecto1.Interfaces;
using Proyecto1.Reportes;
using Proyecto1.Valores;
using static Proyecto1.Valores.Simbolo;

namespace Proyecto1.analizador
{
    class Sintactico
    {
        private string salida = ">> ";
        private ParseTreeNode raiz = null;
        private List<Errores> listaErrores { get; set; }

        private static Sintactico _objSintactico = new Sintactico();

        public static Sintactico ObjSintactico
        {
            get
            {
                return _objSintactico;
            }
        }

        public Sintactico()
        {
            listaErrores = new List<Errores>();
        }

        public void addError(string tipo, string descripcion, int linea, int columna)
        {
            Errores e = new Errores(tipo, descripcion, linea, columna);
            this.listaErrores.Add(e);
        }

        //Metodos get y set de los atributos
        public ParseTreeNode getRaiz()
        {
            return raiz;
        }

        public String getSalida()
        {
            return salida;
        }

        public void setSalida(string texto)
        {
            salida = salida + texto;
        }
        
        public void limpiarSalida()
        {
            salida = ">> ";
        }

        public bool hayErrores()
        {
            return listaErrores.Count > 0 ? true: false;
        }

        //Metodo para analizar la entrada
        public void analizar(String cadena)
        {
            Gramatica gramatica = new Gramatica();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(cadena);            
            raiz = arbol.Root;            
            
            //Revisar si hubo problemas en la gramatica
            if(arbol == null || raiz == null)
            {
                MessageBox.Show("Hay errores con la entrada");
                return;
            }

            
            //TABLA DE ERRORES
            /*
            Errores error;
            for (int i = 0; i < arbol.ParserMessages.Count(); i++)
            {
                error = new Errores("Sintactico", "Sintactico", arbol.ParserMessages.ElementAt(i).Location.Line, arbol.ParserMessages.ElementAt(i).Location.Column);
                listaErrores.Add(error);
            }
            */

            if(!hayErrores())
            {                               
                AST ast = new AST(inicio_bloques(raiz.ChildNodes.ElementAt(0)));
                Ambito ambito = new Ambito(null);
                foreach(Instruccion ins in ast.Instrucciones)
                {
                    ins.ejecutar(ambito, ast);                    
                }
               
            }                                                
        }

        public LinkedList<Instruccion> inicio_bloques(ParseTreeNode nodo)
        {
            //inicio_bloques.Rule = PROGRAM + ID + ";" + bloque_principal
            if (nodo.ChildNodes.Count == 3)
            {                                
                LinkedList<Instruccion> lista = bloque_principal(nodo.ChildNodes.ElementAt(2));
                return lista;
            }
            //inicio_bloques.Rule = PROGRAM + ID + ";" + bloques + bloque_principal;
            else
            {
                LinkedList<Instruccion> lista = bloques(nodo.ChildNodes.ElementAt(2));
                foreach (Instruccion a in bloque_principal(nodo.ChildNodes.ElementAt(3)))
                {
                    lista.AddLast(a);
                }                
                return lista;                
            }
        }

        public LinkedList<Instruccion> bloques(ParseTreeNode nodo)
        {
            //bloques.Rule = bloques + bloques2
            if (nodo.ChildNodes.Count == 2)
            {
                LinkedList<Instruccion> lista = bloques(nodo.ChildNodes.ElementAt(0));
                foreach (Instruccion a in bloques2(nodo.ChildNodes.ElementAt(1)))
                {
                    lista.AddLast(a);
                }                
                return lista;
            }
            //bloques.Rule = bloques2
            else
            {
                LinkedList<Instruccion> lista = new LinkedList<Instruccion>();
                foreach (Instruccion a in bloques2(nodo.ChildNodes.ElementAt(0)))
                {
                    lista.AddLast(a);
                }                
                return lista;
            }
        }

        public LinkedList<Instruccion> bloques2(ParseTreeNode nodo)
        {
            //bloques2.Rule = bloque_variables
            ParseTreeNode nodoInstruccion = nodo.ChildNodes.ElementAt(0);
            string ins = nodoInstruccion.ToString();            
            switch (ins)
            {
                case "bloque_variables":
                    LinkedList<Instruccion> lista = bloque_variables(nodo.ChildNodes.ElementAt(0));
                    return lista;

                default:
                    LinkedList<Instruccion> lista1 = bloque_variables(nodo.ChildNodes.ElementAt(0));
                    return lista1;
            }
        }

        public LinkedList<Instruccion> bloque_variables(ParseTreeNode nodo)
        {
            //bloque_variables.Rule = bloque_constantes + bloque_variables2
            if (nodo.ChildNodes.Count == 2)
            {
                LinkedList<Instruccion> lista = new LinkedList<Instruccion>();
                foreach (Instruccion a in bloque_variables2(nodo.ChildNodes.ElementAt(1)))
                {
                    lista.AddLast(a);
                }                                
                return lista;
            }
            //bloque_variables.Rule = bloque_variables2
            else
            {
                LinkedList<Instruccion> lista = bloque_variables2(nodo.ChildNodes.ElementAt(0));
                return lista;
            }
        }

        public LinkedList<Instruccion> bloque_variables2(ParseTreeNode nodo)
        {
            //bloque_variables2.Rule = VAR + declaraciones_var;
            LinkedList<Instruccion> lista = declaraciones_var(nodo.ChildNodes.ElementAt(1));
            return lista;            
        }

        public LinkedList<Instruccion> declaraciones_var(ParseTreeNode nodo)
        {
            //declaraciones_var.Rule = declaraciones_var + declaracion_var
            if (nodo.ChildNodes.Count == 2)
            {
                LinkedList<Instruccion> lista = declaraciones_var(nodo.ChildNodes.ElementAt(0));
                lista.AddLast(declaracion_var(nodo.ChildNodes.ElementAt(1)));
                return lista;
            }
            //declaraciones_var.Rule = declaracion_var
            else
            {
                LinkedList<Instruccion> lista = new LinkedList<Instruccion>();
                lista.AddLast(declaracion_var(nodo.ChildNodes.ElementAt(0)));
                return lista;
            }
        }

        public Instruccion declaracion_var(ParseTreeNode nodo)
        {           
            //Lista de simbolos
            LinkedList<Simbolo> sims = new LinkedList<Simbolo>();
            //obteniendo el tipo
            Tipos tipo = tipo_var(nodo.ChildNodes.ElementAt(1));
            //obteniendo cada id y agregandolo a la lista
            foreach (ParseTreeNode n in nodo.ChildNodes[0].ChildNodes)
            {                
                Simbolo s = new Simbolo(n.Token.Text, tipo, n.Token.Location.Line, n.Token.Location.Column);
                sims.AddLast(s);
            }
            //obteniendo la parte final de la declaracion
            ParseTreeNode asignacion = nodo.ChildNodes[2];
            //si viene una expresion            
            if (asignacion.ChildNodes.Count.Equals(2))
            {
                //verificar primero que solo venga un id
                if (sims.Count() == 1)
                {
                    ParseTreeNode expre = asignacion.ChildNodes.ElementAt(1);                    
                    return new Declaracion(sims, tipo, expresion(expre), 0, 0);
                }
                else
                {
                    int linea = asignacion.ChildNodes[0].Token.Location.Line + 1;
                    int columna = asignacion.ChildNodes[0].Token.Location.Column;
                    MessageBox.Show("entraaa error");

                    Errores error = new Errores("Sintactico", "Error en la asignacion", linea, columna);
                    listaErrores.Add(error);
                    ParseTreeNode expre = asignacion.ChildNodes.ElementAt(1);
                    return new Declaracion(sims, tipo, expresion(expre), 0, 0);
                }
            }
            //si no viene una expresion en la declaracion
            else
            {
                return new Declaracion(sims, tipo, null, 0, 0);
            }                                      
        }        
        
        public Tipos tipo_var(ParseTreeNode nodo)
        {
            string tipo = nodo.ChildNodes.ElementAt(0).ToString().Split(' ')[0].ToLower();           
            switch (tipo)
            {
                case "string":
                    return Tipos.STRING;
                case "char":
                    return Tipos.CHAR;
                case "integer":
                    return Tipos.INTEGER;
                case "real":
                    return Tipos.REAL;
                case "boolean":
                    return Tipos.BOOLEAN;
                default:
                    return Tipos.VOID;
            }
        }

        public LinkedList<Instruccion> bloque_principal(ParseTreeNode nodo)
        {
            //bloque_principal.Rule = BEGIN + instrucciones + END + ".";
            LinkedList<Instruccion> lista = instrucciones(nodo.ChildNodes.ElementAt(1));            
            return lista;

        }

        public LinkedList<Instruccion> instrucciones(ParseTreeNode nodo)
        {
            //instrucciones -> instrucciones instruccion
            if (nodo.ChildNodes.Count == 2)
            {
                LinkedList<Instruccion> lista = instrucciones(nodo.ChildNodes.ElementAt(0));
                lista.AddLast(instruccion(nodo.ChildNodes.ElementAt(1)));
                return lista;
            }
            //instrucciones -> instruccion
            else
            {
                LinkedList<Instruccion> lista = new LinkedList<Instruccion>();
                lista.AddLast(instruccion(nodo.ChildNodes.ElementAt(0)));
                return lista;
            }            
        }        

        public Instruccion instruccion(ParseTreeNode nodo)
        {
            //instruccion.Rule = print + ";"
            ParseTreeNode nodoInstruccion = nodo.ChildNodes.ElementAt(0);
            //string ins = nodoInstruccion.ToString().Split(' ')[0].ToLower();
            string ins = nodoInstruccion.ToString();

            int linea = nodoInstruccion.ChildNodes[0].Token.Location.Line;
            int columna = nodoInstruccion.ChildNodes[0].Token.Location.Column;
            
            switch (ins)
            {
                // print.Rule = WRITE + "(" + lista_exprs + ")"
                case "print":                                        
                    if (nodoInstruccion.ChildNodes.ElementAt(0).ToString().Split(' ')[0].ToLower() == "writeln")
                    {                        
                        return new Print(expresiones(nodoInstruccion.ChildNodes.ElementAt(1)), linea, columna, true);
                    }
                    else
                    {                        
                        return new Print(expresiones(nodoInstruccion.ChildNodes.ElementAt(1)), linea, columna, false);
                    }

                case "asignacion":
                    ParseTreeNode expre = nodoInstruccion.ChildNodes.ElementAt(2);                    
                    int lin = nodoInstruccion.ChildNodes[1].Token.Location.Line +1;
                    int col = nodoInstruccion.ChildNodes[1].Token.Location.Column;
                    return new Asignacion(nodoInstruccion.ChildNodes.ElementAt(0).Token.Text, expresion(expre), lin, col);

                case "graficar":
                    return new Graficar(linea, columna);

                case "sent_if":
                    ParseTreeNode cond = nodoInstruccion.ChildNodes.ElementAt(1);
                    ParseTreeNode sent_if2 = nodoInstruccion.ChildNodes.ElementAt(3);
                    LinkedList<Instruccion> instruccionesIf = new LinkedList<Instruccion>();
                    LinkedList<Instruccion> instruccionesElse = new LinkedList<Instruccion>();
                    LinkedList<If> lista_elseif = new LinkedList<If>();
                    //instruccionesIf = bloque_if(nodoInstruccion.ChildNodes.ElementAt(3));

                    //solo viene un bloque de instrucciones
                    if (sent_if2.ChildNodes.Count == 2)
                    {
                        ParseTreeNode bloqueif = sent_if2.ChildNodes.ElementAt(0);
                        ParseTreeNode listaelseif = sent_if2.ChildNodes.ElementAt(1);

                        instruccionesIf = bloque_if(bloqueif);

                        if (listaelseif.ChildNodes.Count > 0)
                        {
                            foreach (ParseTreeNode elseif in listaelseif.ChildNodes)
                            {
                                //MessageBox.Show("venga");
                                ParseTreeNode cond2 = elseif.ChildNodes.ElementAt(2);
                                ParseTreeNode bloqueif2 = elseif.ChildNodes.ElementAt(4);
                                LinkedList<Instruccion> instruccionesIf2 = new LinkedList<Instruccion>();
                                LinkedList<Instruccion> instruccionesElse2 = new LinkedList<Instruccion>();
                                LinkedList<If> lista_elseif2 = new LinkedList<If>();

                                instruccionesIf2 = bloque_if(bloqueif2);

                                lista_elseif.AddLast(new If(expresion(cond2), instruccionesIf2, instruccionesElse2, lista_elseif2, 0, 0));
                            }
                        }
                    }
                    else if (sent_if2.ChildNodes.Count == 3)
                    {
                        ParseTreeNode bloqueif = sent_if2.ChildNodes.ElementAt(0);                        
                        ParseTreeNode bloque_else = sent_if2.ChildNodes.ElementAt(2);

                        instruccionesIf = bloque_if(bloqueif);
                        instruccionesElse = bloque_if(bloque_else);                        
                    }

                    else if (sent_if2.ChildNodes.Count == 4)
                    {
                        ParseTreeNode bloqueif = sent_if2.ChildNodes.ElementAt(0);
                        ParseTreeNode listaelseif = sent_if2.ChildNodes.ElementAt(1);
                        ParseTreeNode bloque_else = sent_if2.ChildNodes.ElementAt(3);

                        instruccionesIf = bloque_if(bloqueif);
                        instruccionesElse = bloque_if(bloque_else);

                        if (listaelseif.ChildNodes.Count > 0)
                        {
                            foreach (ParseTreeNode elseif in listaelseif.ChildNodes)
                            {
                                //MessageBox.Show("venga");
                                ParseTreeNode cond2 = elseif.ChildNodes.ElementAt(2);
                                ParseTreeNode bloqueif2 = elseif.ChildNodes.ElementAt(4);
                                LinkedList<Instruccion> instruccionesIf2 = new LinkedList<Instruccion>();
                                LinkedList<Instruccion> instruccionesElse2 = new LinkedList<Instruccion>();
                                LinkedList<If> lista_elseif2 = new LinkedList<If>();

                                instruccionesIf2 = bloque_if(bloqueif2);

                                lista_elseif.AddLast(new If(expresion(cond2), instruccionesIf2, instruccionesElse2, lista_elseif2, 0, 0));
                            }
                        }

                    }
                    return new If(expresion(cond), instruccionesIf, instruccionesElse, lista_elseif, 0, 0);
                default:
                    return new Print(expresiones(nodo.ChildNodes.ElementAt(1)), 0, 0, true); //solo para que corra
            }                        
        }

        public LinkedList<Instruccion> bloque_if(ParseTreeNode nodo)
        {
            //bloque_if.Rule = BEGIN + instrucciones + END                            
            if (nodo.ChildNodes.Count == 3)
            {
                LinkedList<Instruccion> lista = instrucciones(nodo.ChildNodes.ElementAt(1));                
                return lista;
            }
            //bloque_if.Rule = instruccion
            else
            {
                LinkedList<Instruccion> lista = new LinkedList<Instruccion>();
                lista.AddLast(instruccion(nodo.ChildNodes.ElementAt(0)));
                return lista;
            }
        }

        public LinkedList<Expresion> expresiones(ParseTreeNode nodo)
        {
            //lista_exprs.Rule = lista_exprs + COMA + expresion_imprimible                      
            if (nodo.ChildNodes.Count == 2)
            {
                LinkedList<Expresion> lista = expresiones(nodo.ChildNodes.ElementAt(0));
                lista.AddLast(expresion_imp(nodo.ChildNodes.ElementAt(1)));
                return lista;
            }
            //lista_exprs.Rule = expresion_imprimible;
            else
            {
                LinkedList<Expresion> lista = new LinkedList<Expresion>();
                lista.AddLast(expresion_imp(nodo.ChildNodes.ElementAt(0)));
                return lista;
            }
        }

        public Expresion expresion_imp(ParseTreeNode nodo)
        {
            //expresion_imprimible.Rule = expresion;
            if (nodo.ChildNodes.Count == 1)
            {
                return expresion(nodo.ChildNodes.ElementAt(0));
            }
            //expresion_imprimible.Rule = expresion + DOSPTS + ENTERO + DOSPTS + ENTERO              
            else
            {
                return expresion(nodo.ChildNodes.ElementAt(0));
            }
        }

        public Expresion expresion(ParseTreeNode nodo)
        {
            //expresion.Rule = dato
            //expresion.Rule = PARIZQ + expresion + PARDER
            if (nodo.ChildNodes.Count == 1)
            {
                //MessageBox.Show(nodo.ChildNodes.ElementAt(0).ToString());
                if(nodo.ChildNodes.ElementAt(0).ToString() == "expresion")
                {
                    return expresion(nodo.ChildNodes.ElementAt(0));
                }
                else
                {
                    return dato(nodo.ChildNodes.ElementAt(0));
                }                
            }

            else if (nodo.ChildNodes.Count == 2)
            {
                return new Operacion(expresion(nodo.ChildNodes.ElementAt(1)), Operacion.getOperador(nodo.ChildNodes.ElementAt(0).Token.Text));

            }
            //operaciones
            //expresion.Rule = expresion + MAS + expresion
            else if (nodo.ChildNodes.Count == 3)
            {
                return new Operacion(expresion(nodo.ChildNodes.ElementAt(0)), expresion(nodo.ChildNodes.ElementAt(2)), Operacion.getOperador(nodo.ChildNodes.ElementAt(1).Token.Text));
            }
            else
            {
                return dato(nodo.ChildNodes.ElementAt(0));
            }            
        }

        public Expresion dato(ParseTreeNode nodo)
        {
   
            ParseTreeNode tipoDato1 = nodo.ChildNodes.ElementAt(0);
            MessageBox.Show(tipoDato1.ToString());
            string ins = tipoDato1.ToString().Split("(")[1].ToLower();
            MessageBox.Show("ins " + ins);
            object valor = tipoDato1.ToString().Split("(")[0].ToLower();
            MessageBox.Show("valor " + valor.ToString());
            int linea = tipoDato1.Token.Location.Line;
            int columna = tipoDato1.Token.Location.Column;            
            switch (ins)
            {                
                //dato.Rule =  CADENA
                case "cadena)":                    
                    return new Cadena(valor, linea, columna);

                case "entero)":
                    MessageBox.Show("entra a entero");
                    return new Primitivo(Convert.ToInt32(valor), linea, columna);
                
                case "decimal)":
                    MessageBox.Show("entra a decimal");
                    return new Primitivo(Convert.ToDouble(valor), linea, columna);

                case "booleano)":                    
                    return new Primitivo(Convert.ToBoolean(valor), linea, columna);
                
                case "id)":                    
                    return new Identificador(tipoDato1.Token.Text, linea, columna);

                default:
                    return new Cadena(tipoDato1, linea, columna);
            }            
            
        }
            
        public void generarGraficaAST(ParseTreeNode raiz)
        {
            String grafoDOT = ControlRep.getDOT(raiz);
            File.Create("C:\\compiladores2\\GraficaAST.dot").Dispose();
            TextWriter tw = new StreamWriter("C:\\compiladores2\\GraficaAST.dot");
            tw.WriteLine(grafoDOT);
            tw.Close();

            ProcessStartInfo startinfo = new ProcessStartInfo("C:\\Program Files (x86)\\Graphviz2.38\\bin\\dot.exe");
            Process Process;
            startinfo.RedirectStandardOutput = true;
            startinfo.UseShellExecute = false;
            startinfo.CreateNoWindow = true;
            startinfo.Arguments = "-Tpng C:\\compiladores2\\GraficaAST.dot -o C:\\compiladores2\\graficaAST.png";
            Process = Process.Start(startinfo);
            Process.Close();          
        }

        public void generarTablaErrores()
        {
            int veri = 0;
            string folderName = @"C:\compiladores2";
            string pathString = System.IO.Path.Combine(folderName);
            System.IO.Directory.CreateDirectory(pathString);
            string fileName = "ListaErrores.html";
            pathString = System.IO.Path.Combine(pathString, fileName);
            string path = pathString;

            // Create a file to write to.
            using (StreamWriter ht = File.CreateText(path))
            {
                ht.WriteLine("<!DOCTYPE html>");
                ht.WriteLine("<html>");
                ht.WriteLine("<title>Lista de errores</title>");
                ht.WriteLine("<link rel=\"stylesheet\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css\">");
                ht.WriteLine("</head>");

                ht.WriteLine("<body>");

                ht.WriteLine("<script src=\"https://code.jquery.com/jquery-3.4.1.slim.min.js\"></script>");
                ht.WriteLine("<script src=\"https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js\"></script>");
                ht.WriteLine("<script src=\"https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js\"></script>");                                         
                
                ht.WriteLine("<table class=\"table\">");
                ht.WriteLine("<thead class=\"thead - dark\">");                                                             
                ht.WriteLine("<tr>");
                ht.WriteLine("<th scope=\"col\">" + "No." + "</th>");
                ht.WriteLine("<th scope=\"col\">" + "Tipo" + "</th>");
                ht.WriteLine("<th scope=\"col\">" + "Descripcion" + "</th>");
                ht.WriteLine("<th scope=\"col\">" + "Linea" + "</th>");
                ht.WriteLine("<th scope=\"col\">" + "Columna" + "</th>");
                ht.WriteLine("</tr>");
                ht.WriteLine("</thead>");

                ht.WriteLine("<tbody>");
                int i = 0;
                foreach (Errores dato in listaErrores)
                {
                    i++;
                    veri++;
                    ht.WriteLine("<tr>");
                    ht.WriteLine("<td>&nbsp;" + i + "</td>"); //col1                    
                    ht.WriteLine("<td>&nbsp;" + dato.Tipo + "</td>"); //col3
                    ht.WriteLine("<td>&nbsp;" + dato.Descricpion + "</td>"); //col2
                    ht.WriteLine("<td>&nbsp;" + dato.Fila + "</td>"); //col4
                    ht.WriteLine("<td>&nbsp;" + dato.Columna + "</td>"); //col5                     

                    ht.WriteLine("</tr>");                   
                }

                ht.WriteLine("</tbody>");
                ht.WriteLine("</table>");                                   
                ht.WriteLine(" </body>");
                ht.WriteLine("</html>");

            }
          

        }        

    }
}
