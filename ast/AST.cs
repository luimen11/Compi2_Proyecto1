using Proyecto1.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1.ast
{
    class AST
    {

        //private LinkedList<Objeto> objetos;
        public LinkedList<Instruccion> Instrucciones { get; set; }

        public AST(LinkedList<Instruccion> instrucciones)
        {
            //objetos = new LinkedList<Objetos>();
            this.Instrucciones = instrucciones;
        }

        /*
public void agregarObjetos(Objeto o)
{
Objetos.addLast(s);
}
*/



    }
}
