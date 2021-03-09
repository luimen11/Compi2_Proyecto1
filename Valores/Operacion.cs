using Proyecto1.ast;
using Proyecto1.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Proyecto1.Valores
{
    class Operacion : Expresion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        public Expresion operandoIzq { get; set; }
        public Expresion operandoDer { get; set; }
        public Operador operador { get; set;  }

        public Operacion(Expresion operandoIzq, Expresion operandoDer, Operador operador)
        {
            this.operandoIzq = operandoIzq;
            this.operandoDer = operandoDer;
            this.operador = operador;
        }

        public Operacion(Expresion operandoDer, Operador operador)
        {
            this.operandoIzq = null;
            this.operandoDer = operandoDer;
            this.operador = operador;
        }


        public enum Operador
        {
            SUMA,
            MENOS,
            MULTIPICACION,
            DIVISION,
            MODULO,
            MAYOR,
            MENOR,
            MAYORIG,
            MENORIG,
            IGUAL,
            DIFERENTE,
            AND,
            OR,
            NOT
        }

        public Simbolo.Tipos getTipo(Ambito ambito, AST arbol)
        {
            return Simbolo.Tipos.BOOL;
        }

        public static Operador getOperador(string operador)
        {
            if (operador == "+")
            {
                return Operador.SUMA;
            }
            else if (operador == "-")
            {
                return Operador.MENOS;
            }
            else if (operador == "*")
            {
                return Operador.MULTIPICACION;
            }
            else if (operador == "/")
            {
                return Operador.DIVISION;
            }
            else if (operador == ">")
            {
                return Operador.MAYOR;
            }
            else if (operador == "<")
            {
                return Operador.MENOR;
            }
            else if (operador == ">=")
            {
                return Operador.MAYORIG;
            }
            else if (operador == "<=")
            {
                return Operador.MENORIG;
            }
            else if (operador == "=")
            {
                return Operador.IGUAL;
            }
            else if (operador == "<>")
            {
                return Operador.DIFERENTE;
            }
            else if (operador.ToLower() == "and")
            {
                return Operador.AND;
            }
            else if (operador.ToLower() == "or")
            {
                return Operador.OR;
            }
            else if (operador.ToLower() == "not")
            {
                return Operador.NOT;
            }
            else
            {
                return Operador.MODULO;
            }
        }

        public object getValor(Ambito ambito, AST arbol)
        {
            //MessageBox.Show(this.operador.ToString());
            if (this.operador == Operador.SUMA)
            {
                return (Int32)operandoIzq.getValor(ambito, arbol) + (Int32)operandoDer.getValor(ambito, arbol);
            }
            else if (this.operador == Operador.MENOS)
            {
                return (Int32)operandoIzq.getValor(ambito, arbol) - (Int32)operandoDer.getValor(ambito, arbol);
            }
            else if (this.operador == Operador.MULTIPICACION)
            {
                return (Int32)operandoIzq.getValor(ambito, arbol) * (Int32)operandoDer.getValor(ambito, arbol);
            }
            else if (this.operador == Operador.DIVISION)
            {
                return (Int32)operandoIzq.getValor(ambito, arbol) / (Int32)operandoDer.getValor(ambito, arbol);
            }
            else if (this.operador == Operador.MODULO)
            {
                return (Int32)operandoIzq.getValor(ambito, arbol) % (Int32)operandoDer.getValor(ambito, arbol);
            }
            else if (this.operador == Operador.MAYOR)
            {
                return (Int32)operandoIzq.getValor(ambito, arbol) > (Int32)operandoDer.getValor(ambito, arbol);
            }
            else if (this.operador == Operador.MENOR)
            {
                return (Int32)operandoIzq.getValor(ambito, arbol) < (Int32)operandoDer.getValor(ambito, arbol);
            }
            else if (this.operador == Operador.MAYORIG)
            {
                return (Int32)operandoIzq.getValor(ambito, arbol) >= (Int32)operandoDer.getValor(ambito, arbol);
            }
            else if (this.operador == Operador.MENORIG)
            {
                return (Int32)operandoIzq.getValor(ambito, arbol) <= (Int32)operandoDer.getValor(ambito, arbol);
            }
            else if (this.operador == Operador.IGUAL)
            {
                return (Int32)operandoIzq.getValor(ambito, arbol) == (Int32)operandoDer.getValor(ambito, arbol);
            }
            else if (this.operador == Operador.DIFERENTE)
            {
                return (Int32)operandoIzq.getValor(ambito, arbol) != (Int32)operandoDer.getValor(ambito, arbol);
            }
            else if (this.operador == Operador.AND)
            {
                return (Boolean)operandoIzq.getValor(ambito, arbol) && (Boolean)operandoDer.getValor(ambito, arbol);
            }
            else if (this.operador == Operador.OR)
            {
                return (Boolean)operandoIzq.getValor(ambito, arbol) || (Boolean)operandoDer.getValor(ambito, arbol);
            }
            else if (this.operador == Operador.NOT)
            {
                return !(Boolean)operandoDer.getValor(ambito, arbol);
            }

            return false;
        }
    }
}
