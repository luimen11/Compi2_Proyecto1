using Proyecto1.ast;
using Proyecto1.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Proyecto1.Instrucciones
{
    class If : Instruccion
    {
        public int linea { get; set; }
        public int columna { get; set; }
        public Expresion condicion;
        public LinkedList<Instruccion> instruccionesIf;
        public LinkedList<Instruccion> instruccionesElse;
        public LinkedList<If> lista_elseif;

        public If(Expresion condicion, LinkedList<Instruccion> instruccionesIf, LinkedList<Instruccion> instruccionesElse, LinkedList<If> lista_elseif, int linea, int columna)
        {
            this.condicion = condicion;
            this.instruccionesIf = instruccionesIf;
            this.instruccionesElse = instruccionesElse;
            this.lista_elseif = lista_elseif;
            this.linea = linea;
            this.columna = columna;
        }

        public object ejecutar(Ambito ambito, AST arbol)
        {
            //verifico si la condicion es valida y ejecuto las instrucciones del if
            if((bool)condicion.getValor(ambito, arbol))
            {
                Ambito local = new Ambito(ambito);
                foreach(Instruccion ins in instruccionesIf)
                {
                    ins.ejecutar(local, arbol);
                }
                return null;
            }
            //verifico si existen else if y se ejecutan sus instrucciones
            else
            {
                foreach(If ins in lista_elseif)
                {                    
                    if((bool)ins.condicion.getValor(ambito, arbol))
                    {
                        Ambito local2 = new Ambito(ambito);
                        foreach (Instruccion ins2 in ins.instruccionesIf)
                        {
                            ins2.ejecutar(local2, arbol);
                        }
                        return null;
                    }
                }

                //verificar si existe un else
                if (instruccionesElse.Count > 0)
                {
                    Ambito local3 = new Ambito(ambito);
                    foreach (Instruccion ins3 in instruccionesElse)
                    {
                        ins3.ejecutar(local3, arbol);
                    }
                    return null;
                }
            }
            return null;
        }
    }
}
