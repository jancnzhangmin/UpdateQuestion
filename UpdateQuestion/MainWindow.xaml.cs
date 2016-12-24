using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Threading;

namespace UpdateQuestion
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int clickcount = 0;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            wb.Navigate(new Uri("http://jiakao.cloudtimesoft.com/questions"));
            //wblist.Navigate(new Uri("http://jiakao.cloudtimesoft.com"));
        }

        private void wb_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.ToString() != wb.Url.ToString())
                return;
            if (wb.Url.ToString() == "http://jiakao.cloudtimesoft.com/questions")
            {
                opennew();
            }
            else if (wb.Url.ToString().Contains("edit"))
            {
                create_record();
            }
            clickcount++;

            HtmlElement url = wb.Document.GetElementById("login_login") as HtmlElement;
            if (url != null)
            {
                url.SetAttribute("value", "admin");
                wb.Document.GetElementById("login_password").SetAttribute("value", "admin");
                HtmlElementCollection btnAdd = wb.Document.GetElementsByTagName("button") as HtmlElementCollection;
                if (btnAdd[0].OuterText == "登 录")
                {
                    btnAdd[0].InvokeMember("Click");
                }
            }
            else
            {
                if (clickcount < 3)
                {
                    wb.Navigate(new Uri("http://jiakao.cloudtimesoft.com/questions"));
                }
            }
        }


        private void opennew()
        {
            HtmlElementCollection a = wb.Document.GetElementsByTagName("a") as HtmlElementCollection;
            foreach(HtmlElement newbtn in a)
            {
                if (newbtn.GetAttribute("className").Equals("btn btn-w-m btn-success mylink pull-right"))
                {
                    newbtn.InvokeMember("Click");
                }
            }




            //HtmlElement url = wb.Document.GetElementById("login_login") as HtmlElement;
            //if (url != null)
            //{
            //    url.SetAttribute("value", "admin");
            //    wb.Document.GetElementById("login_password").SetAttribute("value", "admin");
            //    HtmlElementCollection btnAdd = wb.Document.GetElementsByTagName("button") as HtmlElementCollection;
            //    if (btnAdd[0].OuterText == "登 录")
            //    {
            //        btnAdd[0].InvokeMember("Click");
            //    }
            //}
        }


        private void create_record()
        {
            wb.Document.GetElementById("question_questionname").SetAttribute("value", "控件值");
            wb.Document.GetElementById("driverlicensetype").SetAttribute("value", "C1");
            System.Windows.Clipboard.SetText("asdf");
            wb.Document.GetElementById("question_questionimage").InvokeMember("Click");

        }

    }
}
