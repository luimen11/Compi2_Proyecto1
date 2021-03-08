using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Proyecto1.analizador;
using Irony.Ast;
using Irony.Parsing;

namespace Proyecto1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();            
        }
        private void btnAnalizar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            string texto = this.richTextBox1.Text;
            if (texto != String.Empty)
            {
                //Sintactico sin = new Sintactico();                
                //sin.analizar(texto);
                Sintactico.ObjSintactico.analizar(texto);
                String salida = Sintactico.ObjSintactico.getSalida();
                if(salida != String.Empty)
                {
                    MessageBox.Show("Analisis completado.");
                    this.txtConsola.Text = String.Empty;
                    this.txtConsola.Text = salida;
                    Sintactico.ObjSintactico.limpiarSalida();
                }
                else
                {
                    MessageBox.Show("Hubieron errores en el proceso de analisis.");
                }
                
            }
            else
            {
                MessageBox.Show("No hay una entrada para analizar");
            }
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {      
            
            ParseTreeNode raiz = null;
            raiz = Sintactico.ObjSintactico.getRaiz();
            if(raiz != null)
            {
                Sintactico.ObjSintactico.generarGraficaAST(raiz);
                Sintactico.ObjSintactico.generarTablaErrores();

                MessageBox.Show("Reportes generados");
            }
            else
            {
                MessageBox.Show("Realice un analisis.");
            }
            

        }


        //*********************************** LINEAS DE EDITOR *********************************************************
        public int getWidth()
        {
            int w = 25;
            // get total lines of richTextBox1    
            int line = richTextBox1.Lines.Length;

            if (line <= 99)
            {
                w = 20 + (int)richTextBox1.Font.Size;
            }
            else if (line <= 999)
            {
                w = 30 + (int)richTextBox1.Font.Size;
            }
            else
            {
                w = 50 + (int)richTextBox1.Font.Size;
            }

            return w;
        }

        public void AddLineNumbers()
        {
            // create & set Point pt to (0,0)    
            Point pt = new Point(0, 0);
            // get First Index & First Line from richTextBox1    
            int First_Index = richTextBox1.GetCharIndexFromPosition(pt);
            int First_Line = richTextBox1.GetLineFromCharIndex(First_Index);
            // set X & Y coordinates of Point pt to ClientRectangle Width & Height respectively    
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            // get Last Index & Last Line from richTextBox1    
            int Last_Index = richTextBox1.GetCharIndexFromPosition(pt);
            int Last_Line = richTextBox1.GetLineFromCharIndex(Last_Index);
            // set Center alignment to LineNumberTextBox    
            LineNumberTextBox.SelectionAlignment = HorizontalAlignment.Center;
            // set LineNumberTextBox text to null & width to getWidth() function value    
            LineNumberTextBox.Text = "";
            LineNumberTextBox.Width = getWidth();
            // now add each line number to LineNumberTextBox upto last line    
            for (int i = First_Line; i <= Last_Line + 1; i++)
            {
                LineNumberTextBox.Text += i + 1 + "\n";
            }
        }        
        
        private void Form1_Load(object sender, EventArgs e)
        {
            LineNumberTextBox.Font = richTextBox1.Font;
            richTextBox1.Select();
            AddLineNumbers();
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers();
            }
        }

        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            LineNumberTextBox.Text = "";
            AddLineNumbers();
            LineNumberTextBox.Invalidate();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                AddLineNumbers();
            }
        }

        private void richTextBox1_FontChanged(object sender, EventArgs e)
        {
            LineNumberTextBox.Font = richTextBox1.Font;
            richTextBox1.Select();
            AddLineNumbers();
        }

        private void LineNumberTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            richTextBox1.Select();
            LineNumberTextBox.DeselectAll();
        }
        //********************************************************************************************

       

        

    }
}
