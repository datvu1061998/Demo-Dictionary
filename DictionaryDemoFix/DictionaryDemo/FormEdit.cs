using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DictionaryDemo
{
    public partial class FormEdit : Form
    {
        private MainScreen _frmMainScreen;
        public FormEdit(Form frmMainScreen)
        {
            _frmMainScreen = (MainScreen) frmMainScreen;
            InitializeComponent();
        }


        private void btn_Check_Click(object sender, EventArgs e)
        {
            string s1 = txt_EditName.Text;
            try
            {
                if (txt_EditName == null)
                    throw new Exception("You have to fill an right word");
                bool check = _frmMainScreen.BB.Search_Check(s1);
                if (check == false)
                {
                    MessageBox.Show("This word is not had in Dictionary", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("You can edit this word", "", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_EditName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txt_EditMeaning.Focus();
        }

        private void txt_EditMeaning_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btn_Edit.Focus();
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_EditName.Text == "")
                    throw new Exception("You have to fill an right word");

                _frmMainScreen.BB.EditWord(txt_EditName.Text, txt_EditMeaning.Text, txt_EditExplication.Text);
                MessageBox.Show("This word has been edited", "Congratulation", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                //MainScreen.AddComboBox(_frmMainScreen.BB, _frmMainScreen.comboBox1);
                //_frmMainScreen.Loading();
                for (int i = 0; i < _frmMainScreen.DS.Count; i++)
                {
                    if (_frmMainScreen.DS[i].Name == txt_EditName.Text)
                    {
                        _frmMainScreen.DS[i].Name = txt_EditName.Text;
                        _frmMainScreen.DS[i].Meaning = txt_EditMeaning.Text;
                        _frmMainScreen.DS[i].Explication = txt_EditExplication.Text;
                    }
                }

                FileStream file = new FileStream(
                    Link.filepath,
                    FileMode.Create, FileAccess.Write);
                StreamWriter write = new StreamWriter(file);

                for (int i = 0; i < _frmMainScreen.DS.Count; i++)
                {
                    string k = _frmMainScreen.DS[i].Name + '@' + _frmMainScreen.DS[i].Meaning + '@' +
                               _frmMainScreen.DS[i].Explication;
                    write.WriteLine(k);
                }
                write.Flush();
                write.Close();
                file.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
