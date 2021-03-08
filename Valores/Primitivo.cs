using Proyecto1.ast;
using Proyecto1.Interfaces;
using static Proyecto1.Valores.Simbolo;

namespace Proyecto1.Valores
{
    class Primitivo : Expresion
    {
        public int linea { get; set; }
        public int columna { get; set; }

        private object valor;

        public Primitivo(object valor, int linea, int columna)
        {
            this.valor = valor;
            this.linea = linea;
            this.columna = columna;
        }      

        public Tipos getTipo(Ambito ambito, AST arbol)
        {
            object valor = this.getValor(ambito, arbol);
            if (valor is bool)
            {
                return Tipos.BOOL;
            }
            else if (valor is string)
            {
                return Tipos.STRING;
            }
            else if (valor is int)
            {
                return Tipos.INT;
            }
            else if (valor is double)
            {
                return Tipos.DOUBLE;
            }
            else
            {
                return Tipos.OBJETO;
            }
        }

        public object getValor(Ambito ambito, AST arbol)
        {
            return valor;
        }
    }
}
