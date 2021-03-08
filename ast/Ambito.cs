using Proyecto1.Valores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Proyecto1.ast
{
    class Ambito
    {

        private Hashtable TablaSimbolos { get; set; }
        private Ambito anterior { get; set;  }

        public Ambito(Ambito anterior)
        {
            TablaSimbolos = new Hashtable();
            this.anterior = anterior;
        }

        public Ambito getAnterior()
        {
            return anterior;
        }
        public Hashtable getTabla()
        {
            return TablaSimbolos;
        }

        public void agregar(string id, Simbolo simbolo)
        {
            id = id.ToLower();
            simbolo.identificador = simbolo.identificador.ToLower();
            TablaSimbolos.Add(id, simbolo);
            /*
            id = id.ToLower();
            if(simbolo.tipo == Simbolo.Tipos.OBJETO)
            {
                simbolo.identificador = id.ToLower();
            }
            else
            {
                simbolo.identificador = simbolo.identificador.ToLower();
            }            
            TablaSimbolos.Add(id, simbolo);
            */
        }

        public Ambito Eliminar(string id)
        {
            id = id.ToLower();
            for(Ambito amb = this; amb != null; amb = amb.anterior)
            {
                if (amb.TablaSimbolos.Contains(id))
                {
                    amb.TablaSimbolos.Remove(id);
                    return amb;
                }
            }
            return this;
        }

        public bool existe(string id)
        {
            id = id.ToLower();
            for (Ambito amb = this; amb != null; amb = amb.anterior)
            {
                if (amb.TablaSimbolos.Contains(id))
                {
                    return true;
                }
            }
            return false;
        }

        public void agregarAmbito(Ambito amb)
        {
            bool bandera = false;
            Ambito local = this;

            while(bandera == false)
            {
                if(local.anterior == null)
                {
                    local.anterior = amb;
                    bandera = true;
                }
                else
                {
                    local = local.anterior;
                }
            }
        }

        public Simbolo obtenerSimbolo(string id)
        {
            id = id.ToLower();
            for (Ambito amb = this; amb != null; amb = amb.anterior)
            {
                Simbolo s = (Simbolo)(amb.TablaSimbolos[id]);
                if(s != null)
                {
                    return s;
                }
            }
            return null;
        }

        public void modificar(string id, Simbolo nuevo)
        {
            id = id.ToLower();
            for (Ambito amb = this; amb != null; amb = amb.anterior)
            {
                Simbolo s = (Simbolo)(amb.TablaSimbolos[id]);
                if (s != null)
                {
                    amb.TablaSimbolos[id] = nuevo;
                }
            }            
        }

    }
}
