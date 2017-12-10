﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DictionaryDemo
{
    public partial class FormAdd : Form
    {
        private MainScreen _frmMainScreen;

        public FormAdd(Form frmMainScreen)
        {
            _frmMainScreen = (MainScreen)frmMainScreen;
            InitializeComponent();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            string s1 = txt_AddName.Text;
            string s2 = txt_AddMeaning.Text;
            string s3 = txt_AddExplication.Text;

            try
            {
                if (s1 == "" || s2 == "")
                    throw new Exception("You have to fill enough word");
                bool check = _frmMainScreen.BB.Search_Check(s1);
                if (check == true)
                    MessageBox.Show("This word has been in this Dictionary", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                else
                {
                    Word wd = new Word(s1, s2, s3);
                    _frmMainScreen.BB.Add(wd);
                    _frmMainScreen.comboBox1.Items.Clear();
                    //MainScreen.AddComboBox(_frmMainScreen.BB, _frmMainScreen.comboBox1);
                    _frmMainScreen.DS.Add(wd);
                    _frmMainScreen.DS.Sort();

                    MessageBox.Show("This word has been added", "Congratulation", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    FileStream fi = new FileStream(
                        @"C:\Users\Trung Kien\Documents\GitHub\Do-An\DictionaryDemoFix\DictionaryDemo\input.txt",
                        FileMode.Create, FileAccess.Write);
                    StreamWriter write = new StreamWriter(fi);
                    for (int i = 0; i < _frmMainScreen.DS.Count; i++)
                    {
                        string k = _frmMainScreen.DS[i].Name + '@' + _frmMainScreen.DS[i].Meaning + '@' +
                                   _frmMainScreen.DS[i].Explication;
                        write.WriteLine(k);
                    }
                    write.Flush();
                    write.Close();
                    fi.Close();
                    _frmMainScreen.AddComboBox();

                    _frmMainScreen.Loading();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_AddName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                txt_AddMeaning.Focus();
        }

        private void txt_AddMeaning_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btn_Add.Focus();
        }
    }
}
