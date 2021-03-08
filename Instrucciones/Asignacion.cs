using Proyecto1.analizador;
using Proyecto1.ast;
using Proyecto1.Interfaces;
using Proyecto1.Valores;
using System;
using System.Collections.Generic;
using System.Text;
using static Proyecto1.Valores.Simbolo;

namespace Proyecto1.Instrucciones
{
    class Asignacion : Instruccion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        string id;
        Expresion valor;
   
        public Asignacion(string id, Expresion valor, int linea, int columna)
        {
            this.id = id;
            this.valor = valor;
            this.linea = linea;
            this.columna = columna;
        }

        public object ejecutar(Ambito ambito, AST arbol)
        {
            object valor_asignado = this.valor.getValor(ambito, arbol);
            Tipos tipo_asignado = this.valor.getTipo(ambito, arbol);

            if (ambito.existe(id))
            {
                Simbolo sim = ambito.obtenerSimbolo(id);
                if (sim.tipo == tipo_asignado)
                {
                    sim.valor = valor_asignado;
                    ambito.modificar(id, sim);
                }
                else
                {
                    //Error    
                    string tipo = "Semantico";
                    string error = "El tipo es incorrecto";
                    int linea = sim.linea;
                    int colu = sim.columna;
                    Sintactico.ObjSintactico.addError(tipo, error, linea, colu);
                    return false;
                }
            }
            else
            {
                //Error
                string tipo = "Semantico";
                string error = "El id no existe.";
                Sintactico.ObjSintactico.addError(tipo, error, linea, columna);
                return null;
            }
            return null;
        }
    }
}
