using Proyecto1.ast;
using Proyecto1.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Proyecto1.Valores
{
    class Cadena : Expresion
    {
        public int linea { get; set; }
        public int columna { get;  set; }

        private object valor;

        public Cadena(Object valor, int linea, int columna)
        {
            this.valor = valor;
            this.linea = linea;
            this.columna = columna;
        }
        public Simbolo.Tipos getTipo(Ambito ambito, AST arbol)
        {
            if(valor.ToString().Trim().Length > 1)
            {                
                return Simbolo.Tipos.STRING;
            }
            else
            {
                return Simbolo.Tipos.CHAR;
            }
            
        }

        public object getValor(Ambito ambito, AST arbol)
        {
            //MessageBox.Show("valor"+valor.ToString());
            return valor;
        }
    }
}
