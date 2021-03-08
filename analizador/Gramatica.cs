using System;
using System.Collections.Generic;
using System.Text;
using Irony.Ast;
using Irony.Parsing;


namespace Proyecto1.analizador
{
    class Gramatica : Grammar
    {

        public Gramatica()
        {

            #region ER
            StringLiteral CADENA = new StringLiteral("cadena", "'");
            var ENTERO = new NumberLiteral("entero");
            var DECIMAL = new RegexBasedTerminal("decimal", "[0-9]+'.'[0-9]+");
            IdentifierTerminal ID = new IdentifierTerminal("id");

            ConstantTerminal BOOLEANO = new ConstantTerminal("booleano");
            BOOLEANO.Add("true", true);
            BOOLEANO.Add("false", false);

            CommentTerminal comentarioLinea = new CommentTerminal("comentarioLinea", "//", "\n", "\r\n");
            CommentTerminal comentarioBloque = new CommentTerminal("comentarioBloque", "(*", "*)");
            #endregion

            #region Terminales
            var PROGRAM = ToTerm("program");
            var USES = ToTerm("uses");
            var BEGIN = ToTerm("begin");
            var END = ToTerm("end");
            var WRITE = ToTerm("write");
            var WRITELN = ToTerm("writeln");
            var READLN = ToTerm("readln");
            var EXIT = ToTerm("exit");
            var GRAFICARTS = ToTerm("graficar_ts");
            var SETLENGTH = ToTerm("setlength");
            var VAR = ToTerm("var");
            var CONST = ToTerm("const");
            var TYPE = ToTerm("type");
            var VOID = ToTerm("void");
            var INTEGER = ToTerm("integer");
            var DOUBLE = ToTerm("double");
            var REAL = ToTerm("real");
            var CHAR = ToTerm("char");
            var BOOLEAN = ToTerm("boolean");
            var STRING = ToTerm("string");
            var OBJECT = ToTerm("object");
            var IF = ToTerm("if");
            var THEN = ToTerm("then");
            var ELSE = ToTerm("else");
            var CASE = ToTerm("case");
            var OF = ToTerm("of");
            var AND = ToTerm("and");
            var OR = ToTerm("or");
            var NOT = ToTerm("not");
            var WHILE = ToTerm("while");
            var FOR = ToTerm("for");
            var TO = ToTerm("to");
            var DO = ToTerm("do");
            var REPEAT = ToTerm("repeat");
            var UNTIL = ToTerm("until");
            var BREAK = ToTerm("break");
            var CONTINUE = ToTerm("continue");
            var ARRAY = ToTerm("array");
            var FUNCTION = ToTerm("function");
            var PROCEDURE = ToTerm("procedure");

            var PTCOMA = ToTerm(";");
            var PTO = ToTerm(".");
            var COMA = ToTerm(",");
            var PARIZQ = ToTerm("(");
            var PARDER = ToTerm(")");
            var COMILLA = ToTerm("'");
            var DOSPTS = ToTerm(":");
            var CORIZQ = ToTerm("[");
            var CORDER = ToTerm("]");
            var MAS = ToTerm("+");
            var MENOS = ToTerm("-");
            var POR = ToTerm("*");
            var DIV = ToTerm("/");
            var MOD = ToTerm("%");
            var MAYQUE = ToTerm(">");
            var MENQUE = ToTerm("<");
            var MAYIGQUE = ToTerm(">=");
            var MENIGQUE = ToTerm("<=");
            var DIF = ToTerm("<>");
            var IGUAL = ToTerm("=");

            RegisterOperators(1, IGUAL, DIF, MENQUE, MENIGQUE, MAYQUE, MAYIGQUE);
            RegisterOperators(2, MAS, MENOS, OR);
            RegisterOperators(3, POR, DIV, MOD, AND);
            RegisterOperators(4, NOT);

            NonGrammarTerminals.Add(comentarioLinea);
            NonGrammarTerminals.Add(comentarioBloque);

            #endregion

            #region No Terminales

            NonTerminal inicio = new NonTerminal("inicio");
            NonTerminal inicio_bloques = new NonTerminal("inicio_bloques");
            NonTerminal bloque_general = new NonTerminal("bloque_general");
            NonTerminal bloque_uses = new NonTerminal("bloque_uses");
            NonTerminal lista_uses = new NonTerminal("lista_uses");
            NonTerminal bloques = new NonTerminal("bloques");
            NonTerminal bloques2 = new NonTerminal("bloques2");
            NonTerminal bloque_principal = new NonTerminal("bloque_principal");
            NonTerminal bloque_variables = new NonTerminal("bloque_variables");
            NonTerminal bloque_variables2 = new NonTerminal("bloque_variables2");
            NonTerminal declaraciones_var = new NonTerminal("declaraciones_var");
            NonTerminal declaracion_var = new NonTerminal("declaracion_var");
            NonTerminal dec_asig = new NonTerminal("dec_asig");
            NonTerminal lista_ids = new NonTerminal("lista_ids");
            NonTerminal tipo_var = new NonTerminal("tipo_var");
            NonTerminal bloque_tipos = new NonTerminal("bloque_tipos");
            NonTerminal declaraciones_tipos = new NonTerminal("declaraciones_tipos");
            NonTerminal declaracion_tipos = new NonTerminal("declaracion_tipos");
            NonTerminal tipo_index = new NonTerminal("tipo_index");
            NonTerminal index = new NonTerminal("index");
            NonTerminal tipo_object = new NonTerminal("tipo_object");
            NonTerminal bloque_constantes = new NonTerminal("bloque_constantes");
            NonTerminal definiciones_const = new NonTerminal("definiciones_const");
            NonTerminal definicion_const = new NonTerminal("definicion_const");
            NonTerminal asignacion = new NonTerminal("asignacion");
            NonTerminal lista_expresiones = new NonTerminal("lista_expresiones");
            NonTerminal instruccion = new NonTerminal("instruccion");
            NonTerminal instrucciones = new NonTerminal("instrucciones");
            NonTerminal instruccion_sin_ptc = new NonTerminal("instruccion_sin_ptc");
            NonTerminal bloque_compuesto = new NonTerminal("bloque_compuesto");
            NonTerminal print = new NonTerminal("print");
            NonTerminal lista_exprs = new NonTerminal("lista_exprs");
            NonTerminal read = new NonTerminal("read");
            NonTerminal length_array = new NonTerminal("length_array");
            NonTerminal graficar = new NonTerminal("graficar");
            NonTerminal salir = new NonTerminal("salir");
            NonTerminal sent_if = new NonTerminal("sent_if");
            NonTerminal sent_if2 = new NonTerminal("sent_if2");
            NonTerminal sent_case = new NonTerminal("sent_case");
            NonTerminal lista_casos = new NonTerminal("lista_casos");
            NonTerminal caso = new NonTerminal("caso");
            NonTerminal ciclo_for = new NonTerminal("ciclo_for");
            NonTerminal ciclo_while = new NonTerminal("ciclo_while");
            NonTerminal ciclo_repeat = new NonTerminal("ciclo_repeat");
            NonTerminal bloque_funcion = new NonTerminal("bloque_funcion");
            NonTerminal argumentos_header = new NonTerminal("argumentos_header");
            NonTerminal argumentos_header2 = new NonTerminal("argumentos_header2");
            NonTerminal cuerpo_funcion = new NonTerminal("cuerpo_funcion");
            NonTerminal lista_argumentos = new NonTerminal("lista_argumentos");
            NonTerminal lista_argumentos2 = new NonTerminal("lista_argumentos2");
            NonTerminal argumentos = new NonTerminal("argumentos");
            NonTerminal llamada_funcion = new NonTerminal("llamada_funcion");
            NonTerminal bloque_procedimiento = new NonTerminal("bloque_procedimiento");
            NonTerminal llamada_procedimiento = new NonTerminal("llamada_procedimiento");
            NonTerminal expresion = new NonTerminal("expresion");
            NonTerminal expresion_imprimible = new NonTerminal("expresion_imprimible");
            NonTerminal dato = new NonTerminal("dato");

            #endregion

            #region Gramatica

            inicio.Rule = inicio_bloques;

            inicio_bloques.Rule = PROGRAM + ID + ";" + bloque_principal
                        | PROGRAM + ID + ";" + bloques + bloque_principal;

            ///bloques.Rule = this.MakePlusRule(bloques, bloques2);
            bloques.Rule = bloques + bloques2
                        | bloques2;

            bloques2.Rule = bloque_funcion
                    | bloque_procedimiento
                    | bloque_variables
                    | bloque_tipos
                    | bloque_constantes;

            bloque_principal.Rule = BEGIN + instrucciones + END + ".";

            //---------------------- DECLARACIONES DE VARIABLES----------------------------
            #region variables
            bloque_variables.Rule = bloque_constantes + bloque_variables2
                              | bloque_variables2;

            bloque_variables2.Rule = VAR + declaraciones_var;

            //declaraciones_var.Rule = this.MakePlusRule(declaraciones_var, declaracion_var);
            declaraciones_var.Rule = declaraciones_var + declaracion_var
                                    | declaracion_var;

            declaracion_var.Rule = lista_ids + DOSPTS + tipo_var + dec_asig;
            //| lista_ids + DOSPTS + tipo_var + IGUAL + expresion + PTCOMA;
            //| ID + DOSPTS + ARRAY + CORIZQ + tipo_index + CORDER + OF + tipo_var + PTCOMA

            dec_asig.Rule = IGUAL + expresion + PTCOMA
                            | PTCOMA;

            lista_ids.Rule = this.MakePlusRule(lista_ids, COMA, ID);
            //lista_ids.Rule = lista_ids + COMA + ID
            //| ID;

            tipo_var.Rule = INTEGER | DOUBLE | REAL | CHAR | BOOLEAN | STRING | PARIZQ + expresion + PARDER | ID;
            #endregion
            //-------------------------------- DECLARACIONES DE TIPOS----------------------------
            #region tipos
            bloque_tipos.Rule = TYPE + declaraciones_tipos;

            declaraciones_tipos.Rule = this.MakePlusRule(declaraciones_tipos, declaracion_tipos);            

            declaracion_tipos.Rule = lista_ids + IGUAL + tipo_var + PTCOMA //declarando un tipo simple
                             | ID + IGUAL + ARRAY + CORIZQ + tipo_index + CORDER + OF + tipo_var + PTCOMA //declarando un tipo array
                             | ID + IGUAL + ARRAY + OF + tipo_var + PTCOMA //declarando un tipo array dinamico
                             | tipo_object + PTCOMA;

            tipo_index.Rule = this.MakeListRule(tipo_index, ToTerm(","), index);            

            index.Rule = dato + PTO + PTO + dato;

            tipo_object.Rule = ID + IGUAL + OBJECT + VAR + bloque_variables2 + END;
            #endregion
            //----------------------------------BLOQUE DE CONSTANTES ----------------------------------------
            #region constantes
            bloque_constantes.Rule = CONST + definiciones_const;

            definiciones_const.Rule = definiciones_const + definicion_const
                              | definicion_const;

            definicion_const.Rule = ID + IGUAL + dato + PTCOMA;
            #endregion
            //-------------------------------- ASIGNACIONES - ACCESOS----------------------------------------
            #region asignaciones
            asignacion.Rule = ID + DOSPTS + IGUAL + expresion
                    //| ID + CORIZQ + lista_expresiones + CORDER + DOSPTS + IGUAL + expresion
                    //| ID + DOSPTS + IGUAL + ID + CORIZQ + lista_expresiones + CORDER
                    ;

            lista_expresiones.Rule = this.MakeListRule(lista_expresiones, ToTerm(","), expresion);
            //lista_expresiones=lista_expresiones ',' expresion
            //| expresion
            #endregion
            //-------------------------------------LISTADO DE INSTRUCCIONES----------------------------
            #region instrucciones

            //instrucciones.Rule = this.MakePlusRule(instrucciones, instruccion);
            instrucciones.Rule = instrucciones + instruccion
                                | instruccion;

            instruccion.Rule = print + ";"
                            | read + PTCOMA
                            | asignacion + PTCOMA
                            | sent_if + PTCOMA
                            | sent_case + PTCOMA
                            | ciclo_for + PTCOMA
                            | ciclo_while + PTCOMA
                            | ciclo_repeat + PTCOMA
                            | bloque_compuesto + PTCOMA
                            | llamada_funcion + PTCOMA
                            | llamada_procedimiento + PTCOMA
                            | length_array + PTCOMA
                            | graficar + PTCOMA
                            | salir + PTCOMA
                            | graficar;

            instruccion_sin_ptc.Rule = print | read | asignacion | sent_if | sent_case | ciclo_for | ciclo_while | ciclo_repeat | bloque_compuesto | llamada_funcion | llamada_procedimiento | length_array | graficar | salir;

            bloque_compuesto.Rule = BEGIN + instrucciones + END;

            print.Rule = WRITE + "(" + lista_exprs + ")"
                 | WRITELN + "(" + lista_exprs + ")"
                 | WRITELN;

            graficar.Rule = GRAFICARTS + PARIZQ + PARDER ;

            lista_exprs.Rule = lista_exprs + COMA + expresion_imprimible
                      | expresion_imprimible;

            read.Rule = READLN + PARIZQ + dato + PARDER;

            length_array.Rule = SETLENGTH + PARIZQ + ID + COMA + lista_expresiones + PARDER;

            graficar.Rule = GRAFICARTS + PARIZQ + PARDER
                    | GRAFICARTS;

            salir.Rule = EXIT + PARIZQ + lista_expresiones + PARDER
                  | EXIT;
            #endregion
            //------------------------------------- CONDICIONES Y CICLOS----------------------------------------
            #region condicionales
            sent_if.Rule = IF + expresion + THEN + sent_if;

            sent_if2.Rule = instruccion
                    | instruccion_sin_ptc + ELSE + sent_if
                    | instruccion_sin_ptc + ELSE + instruccion;

            sent_case.Rule = CASE + PARIZQ + expresion + PARDER + OF + lista_casos + END + PTCOMA
                     | CASE + PARIZQ + expresion + PARDER + OF + lista_casos + ELSE + instruccion + END + PTCOMA;

            lista_casos.Rule = this.MakePlusRule(lista_casos, caso);
            //lista_casos=lista_casos caso
            //| caso

            caso.Rule = expresion + DOSPTS + instruccion;

            ciclo_for.Rule = FOR + asignacion + TO + dato + DO + instruccion;

            ciclo_while.Rule = WHILE + PARIZQ + expresion + PARDER + DO + instruccion;

            ciclo_repeat.Rule = REPEAT + instrucciones + UNTIL + expresion;

            #endregion
            //-------------------------------------------FUNCIONES--------------------------------------------------
            #region funciones
            bloque_funcion.Rule = FUNCTION + ID + argumentos_header + DOSPTS + tipo_var + PTCOMA + cuerpo_funcion;

            argumentos_header.Rule = PARIZQ + argumentos_header2
                               | Empty;

            argumentos_header2.Rule = lista_argumentos + PARDER
                             | PARDER;

            cuerpo_funcion.Rule = bloque_variables2 + bloque_compuesto + PTCOMA
                          | bloque_compuesto + PTCOMA;

            lista_argumentos.Rule = argumentos + lista_argumentos2;

            lista_argumentos2.Rule = PTCOMA + argumentos + lista_argumentos2
                              | Empty;

            argumentos.Rule = VAR + lista_ids + DOSPTS + tipo_var
                      | lista_ids + DOSPTS + tipo_var;

            llamada_funcion.Rule = ID + DOSPTS + IGUAL + ID + PARIZQ + lista_argumentos + PARDER
                          | ID + DOSPTS + IGUAL + ID + PARIZQ + PARDER;
            #endregion
            //----------------------------------------PROCEDIMIENTOS-----------------------------------------------
            #region procedimientos
            bloque_procedimiento.Rule = PROCEDURE + ID + argumentos_header + PTCOMA + cuerpo_funcion;

            llamada_procedimiento.Rule = ID + PARIZQ + lista_argumentos + PARDER
                                  | ID + PARIZQ + PARDER;
            #endregion
            //------------------------------------ OPERACIONES Y EXPRESIONES----------------------------------------------
            #region expresiones
            expresion.Rule = expresion + MAS + expresion
                         | expresion + MENOS + expresion
                         | expresion + POR + expresion
                         | expresion + DIV + expresion
                         | expresion + MOD + expresion
                         | expresion + IGUAL + expresion
                         | expresion + DIF + expresion
                         | expresion + MAYQUE + expresion
                         | expresion + MENQUE + expresion
                         | expresion + MAYIGQUE + expresion
                         | expresion + MENIGQUE + expresion
                         | expresion + AND + expresion
                         | expresion + OR + expresion
                         | NOT + expresion
                         | PARIZQ + expresion + PARDER
                         | dato;

            expresion_imprimible.Rule = expresion + DOSPTS + ENTERO + DOSPTS + ENTERO
                               | expresion;

            dato.Rule = ID | CADENA | ENTERO | DECIMAL | BOOLEANO ;
            #endregion

            #endregion

            #region Preferencias
            this.Root = inicio;
            MarkPunctuation("(", ")",";", ".", ",", ":");
            #endregion

        }

    }
}
