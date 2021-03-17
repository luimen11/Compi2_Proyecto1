using Proyecto1.analizador;
using Proyecto1.ast;
using Proyecto1.Interfaces;
using Proyecto1.Valores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using static Proyecto1.Valores.Simbolo;

namespace Proyecto1.Instrucciones
{
    class Declaracion : Instruccion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        private Tipos tipo { get; set; }
        private Expresion valor { get; set; }

        private LinkedList<Simbolo> identificadores;

        public Declaracion(LinkedList<Simbolo> identificadores, Tipos tipo, Expresion valor, int linea, int columna)
        {
            this.identificadores = identificadores;
            this.tipo = tipo;
            this.valor = valor;
            this.linea = linea;
            this.columna = columna;
        }

        public object ejecutar(Ambito ambito, AST arbol)
        {
            object valor_asignado = null;
            Tipos tipo_asignado;

            if(this.valor != null)
            {                
                valor_asignado = this.valor.getValor(ambito, arbol);
                tipo_asignado = this.valor.getTipo(ambito, arbol);                
            }
            else
            {                
                tipo_asignado = tipo;
                if(tipo == Tipos.BOOLEAN)
                {
                    valor_asignado = false;
                }
                else if(tipo == Tipos.INTEGER)
                {
                    valor_asignado = 0;
                }
                else if(tipo == Tipos.REAL)
                {
                    valor_asignado = 0.0;
                }
                else if(tipo == Tipos.STRING)
                {
                    valor_asignado = "";
                }
                else if (tipo == Tipos.CHAR)
                {
                    valor_asignado = "";
                }
            }

            foreach(Simbolo s in identificadores)
            {                
                if (!ambito.existe(s.identificador))
                {                    
                    if (tipo == tipo_asignado)
                    {                        
                        s.valor = valor_asignado;                        
                        ambito.agregar(s.identificador, s);
                    }
                    else
                    {                        
                        //Error    
                        string tipo = "Semantico";
                        string error = "El tipo es incorrecto";
                        int linea = s.linea;
                        int colu = s.columna;
                        Sintactico.ObjSintactico.addError(tipo, error, linea, colu);
                        return false;
                    }
                }
                else
                {                    
                    //Error
                    string tipo = "Semantico";
                    string error = "La variable YA existe en el ambito";
                    int linea = s.linea;
                    int colu = s.columna;
                    Sintactico.ObjSintactico.addError(tipo, error, linea, colu);
                    return false;
                }
            }
            return true;        
        }
    }
}
