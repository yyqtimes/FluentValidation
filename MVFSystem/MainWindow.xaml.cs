using GalaSoft.MvvmLight.Messaging;
using MVFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace MVFSystem
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //接收消息
            Messenger.Default.Register<bool>(this, "close", message =>
            {
                this.Close();
            });
            //接受消息显示选择信息
            Messenger.Default.Register<StudentModel>(this, "showMessage", message =>
            {
                MessageBox.Show(message.sname);
            });
        }
    }
}
