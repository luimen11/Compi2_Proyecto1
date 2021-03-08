using Proyecto1.ast;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.Interfaces
{
    interface Instruccion
    {
        public int linea { get; set; }
        public int columna { get; set;  }
        object ejecutar(Ambito ambito, AST arbol);


    }
}
