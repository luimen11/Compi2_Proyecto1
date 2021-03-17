using Proyecto1.ast;
using Proyecto1.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Valores
{
    class Simbolo: Expresion
    {
        public enum Tipos
        {
            STRING,
            CHAR,
            INTEGER,
            REAL,
            BOOLEAN,
            VOID,
            OBJETO,
            FUNCTION,
            PROCEDURE
        }

        public string identificador { get; set; }
        public object valor { get; set; }
        public Tipos tipo { get; set; }
        public int linea { get; set; }
        public int columna { get; set; }

        public Simbolo(string id, Tipos tipo, int l, int c)
        {
            this.identificador = id;
            this.tipo = tipo;
            this.linea = l;
            this.columna = c;
        }
        public Tipos getTipo(Ambito ambito, AST arbol)
        {
            return tipo;
        }

        public object getValor(Ambito ambito, AST arbol)
        {
            return valor;
        }
    }
}
