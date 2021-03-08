using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto1
{
    class Errores
    {
        private string tipo;
        private string descricpion;
        private int fila;
        private int columna;

        public string Tipo
        {
            get { return tipo;  }
            set { tipo = value; }
        }
        public string Descricpion
        {
            get { return descricpion; }
            set { descricpion = value; }
        }
        
        public int Fila
        {
            get { return fila; }
            set { fila = value; }
        }

        public int Columna
        {
            get { return columna; }
            set { columna = value; }
        }

        public Errores(string t, string d, int f, int c)
        {
            tipo = t;
            descricpion = d;
            fila = f;
            columna = c;
        }
    }
}
