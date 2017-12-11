using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DictionaryDemo
{
    public class SpeakText
    {
        private WebBrowser wbWeb;

        public WebBrowser WbWeb
        {
            get { return wbWeb; }
            set { wbWeb = value; }
        }

        public SpeakText(WebBrowser wb)
        {
            this.WbWeb = wb;
        }

        private void SetText(string data)
        {
            try
            {
                HtmlElement element = WbWeb.Document.GetElementById("text"); //Set thuộc tính của textbox trang web
                element.SetAttribute("value", data);//Lấy giá trị của textbox trang web
            }
            catch (Exception e)
            {
                MessageBox.Show("Can not Access to Internet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Speak()
        {
            try
            {
                HtmlElement element = WbWeb.Document.GetElementById("playbutton");//Set thuộc tính của button trang web
                element.InvokeMember("click");//Khởi tạo sự kiện click của button
            }
            catch (Exception e)
            {
                
            }
        }

        public void Speaking(string data)
        {
            SetText(data);
            Speak();
        }
    }
}
