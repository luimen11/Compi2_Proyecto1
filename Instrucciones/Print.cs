using Proyecto1.analizador;
using Proyecto1.ast;
using Proyecto1.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Proyecto1
{
    class Print : Instruccion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        public bool salto { get; set; }

        public LinkedList<Expresion> expresiones { get; set; }

        public Print(LinkedList<Expresion> expresiones, int linea, int columna, bool salto)
        {
            this.expresiones = expresiones;
            this.linea = linea;
            this.columna = columna;
            this.salto = salto;

        }

        public object ejecutar(Ambito ambito, AST arbol)
        {
            object valor = null;
            string cadena = "";

            foreach (Expresion exp in expresiones)
            {
                valor = exp.getValor(ambito, arbol);
                cadena = cadena + valor.ToString();
                
            }            

            if (salto)
            {
                cadena = cadena + Environment.NewLine + ">> ";
                Sintactico.ObjSintactico.setSalida(cadena);
                return true;
            }

            Sintactico.ObjSintactico.setSalida(cadena);

            return true;

        }
    }
}
