using Proyecto1.ast;
using System;
using System.Collections.Generic;
using System.Text;
using static Proyecto1.Valores.Simbolo;

namespace Proyecto1.Interfaces
{
    interface Expresion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        Tipos getTipo(Ambito ambito, AST arbol);
        Object getValor(Ambito ambito, AST arbol);
    }
}
