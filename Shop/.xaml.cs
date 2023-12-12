using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shop
{
    public partial class Entry : Window
    {

        public Entry()
        {
            InitializeComponent();
            LoadPage("Login.xaml");
        }
        private void LoadPage(string pageName)
        {
            Page page = (Page)Application.LoadComponent(new Uri(pageName, UriKind.Relative));
            frame.Content = page;
            page.Background = this.Background;
            page.Width = this.Width;
            page.Height = this.Height;
        }
    }
}
