using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CCPROJECT
{
    public partial class Form1 : Form
    {

        public static string MessageString = null;
                public Form1()
                {
                    InitializeComponent();
                    rtBox.Focus();
                }


                private void AnalyzeButton_Click_1(object sender, EventArgs e)
                {
                    MessageString = null;
                    string Alltext = rtBox.Text;
                    LexicalAnalyze AnalyzerObject = new LexicalAnalyze(Alltext);
                    listBox1.Items.Clear();
                    TokenReader T_Reader_Obj = new TokenReader();
                    try
                    {
                        listBox1.Items.AddRange(T_Reader_Obj.GetTokensArray());
                    }
                    catch (Exception ex)
                    { 
                    
                    }
                    if (MessageString != null)
                    {
                        MessageBox.Show(MessageString, "Reload");
                    
                    }
                }

                private void OpenFileButton_Click(object sender, EventArgs e)
                {
                    OpenFileDialog open = new OpenFileDialog();
                    if (open.ShowDialog() == DialogResult.OK)
                        rtBox.LoadFile(open.FileName, RichTextBoxStreamType.PlainText);
                    
                }

                private void NewFileButton_Click(object sender, EventArgs e)
                {
                    rtBox.Clear();
                }

                private void SaveFileButton_Click(object sender, EventArgs e)
                {
                    if (String.IsNullOrEmpty(rtBox.Text))
                    {
                        MessageBox.Show("Empty File! ", "Empty");

                    }
                    else
                    {
                        SaveFileDialog save = new SaveFileDialog();
                        save.Filter = "Text Document(*.txt)|*.txt|All Files(*.*)|*.*";
                        if (save.ShowDialog() == DialogResult.OK)
                            rtBox.SaveFile(save.FileName, RichTextBoxStreamType.PlainText);
                        
                    }
                }

                private void Form1_Load(object sender, EventArgs e)
                {

                }

                private void Syntax_Click(object sender, EventArgs e)
                {
                    SyntaxCheck SC = new SyntaxCheck();
                    MessageLabel.Text = SyntaxCheck.MessageString;
                }

                private void button1_Click(object sender, EventArgs e)
                {
                    Semantic Sem = new Semantic();
                    MessageLabel.Text = Semantic.MessageString;
                }
            }
        }
    

