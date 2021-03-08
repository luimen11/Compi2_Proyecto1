using Proyecto1.analizador;
using Proyecto1.ast;
using Proyecto1.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Proyecto1.Valores
{
    class Identificador : Expresion
    {
        public int linea { get; set; }
        public int columna { get; set; }

        private string id { get; set; }

        public Identificador(string id, int linea, int col)
        {
            this.id = id;
            this.linea = linea;
            this.columna = col;
        }

        public Simbolo.Tipos getTipo(Ambito ambito, AST arbol)
        {
            if (ambito.existe(id))
            {
                Simbolo simbolo = ambito.obtenerSimbolo(id);
                return simbolo.tipo;
            }
            else
            {
                //Error
                string tipo = "Semantico";
                string error = "El id no existe.";
                Sintactico.ObjSintactico.addError(tipo, error, linea, columna);
                return Simbolo.Tipos.VOID;
            }
        }

        public object getValor(Ambito ambito, AST arbol)
        {
            if (ambito.existe(id))
            {
                Simbolo simbolo = ambito.obtenerSimbolo(id);                
                return simbolo.valor;
            }
            else
            {
                //Error
                string tipo = "Semantico";
                string error = "El id no existe.";             
                Sintactico.ObjSintactico.addError(tipo, error, linea, columna);
                return null;
            }
        }
    }
}
