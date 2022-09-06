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
using System.Windows.Shapes;

namespace Semester6_MainProject
{
    /// <summary>
    /// Interaction logic for PlayFilmWindow.xaml
    /// </summary>
    public partial class PlayFilmWindow : Window
    {
        string duongdangoc = "D:\\data\\";
        public PlayFilmWindow(string tenFileVideo, string tap_phim)
        {
            InitializeComponent();
            VideoControl.Source = new Uri(duongdangoc + tenFileVideo);
            this.Title = tap_phim;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            VideoControl.Stop();
        }
    }
}
