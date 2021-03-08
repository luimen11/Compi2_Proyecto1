using Proyecto1.ast;
using Proyecto1.Interfaces;
using Proyecto1.Valores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Proyecto1.Instrucciones
{
    class Graficar : Instruccion
    {
        public int linea { get; set; }
        public int columna { get; set; }

        public Graficar(int linea, int ciolumna)
        {
            this.linea = linea;
            this.columna = columna;
        }

        public object ejecutar(Ambito ambito, AST arbol)
        {            
            int veri = 0;
            string folderName = @"C:\compiladores2";
            string pathString = System.IO.Path.Combine(folderName);
            System.IO.Directory.CreateDirectory(pathString);
            string fileName = "ListaSimbolos.html";
            pathString = System.IO.Path.Combine(pathString, fileName);
            string path = pathString;

            // Create a file to write to.
            using (StreamWriter ht = File.CreateText(path))
            {
                ht.WriteLine("<!DOCTYPE html>");
                ht.WriteLine("<html>");
                ht.WriteLine("<title>Tabla de simbolos</title>");
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
                ht.WriteLine("<th scope=\"col\">" + "Identificador" + "</th>");
                ht.WriteLine("<th scope=\"col\">" + "Tipo" + "</th>");
                ht.WriteLine("</tr>");
                ht.WriteLine("</thead>");

                ht.WriteLine("<tbody>");
                int i = 0;

                for (Ambito amb = ambito; amb != null; amb = amb.getAnterior())
                {
                    foreach (DictionaryEntry s in amb.getTabla())
                    {
                        i++;
                        veri++;
                        ht.WriteLine("<tr>");
                        ht.WriteLine("<td>&nbsp;" + i + "</td>"); //col1    
                        Simbolo sim = (Simbolo)s.Value;
                        ht.WriteLine("<td>&nbsp;" + sim.identificador + "</td>"); //col3                        
                        ht.WriteLine("<td>&nbsp;" + sim.tipo + "</td>"); //col2

                        ht.WriteLine("</tr>");
                    }
                }

                

                ht.WriteLine("</tbody>");
                ht.WriteLine("</table>");
                ht.WriteLine(" </body>");
                ht.WriteLine("</html>");

            }
            return null;            
        }

    }
}
