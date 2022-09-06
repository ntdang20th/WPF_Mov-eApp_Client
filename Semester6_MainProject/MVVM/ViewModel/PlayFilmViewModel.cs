using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Semester6_MainProject.MVVM.ViewModel
{
    public class PlayPhimViewModel : BaseViewModel
    {
        DispatcherTimer timer;
        int count = 0;
        public ICommand MouseEnterGridCommand { get; set; }
        public ICommand MouseLeaveGridCommand { get; set; }
        public ICommand MouseDownGridCommand { get; set; }


        public PlayPhimViewModel()
        {
            MouseEnterGridCommand = new RelayCommand<Semester6_MainProject.MyUserControl.ControlVideo>((p) => { return true; }, (p) =>
            {
                p.Visibility = Visibility.Visible;
                StartDispatcherTimer(p);
            });

            MouseLeaveGridCommand = new RelayCommand<Semester6_MainProject.MyUserControl.ControlVideo>((p) => { return true; }, (p) =>
            {
                p.Visibility = Visibility.Hidden;
            });

            MouseDownGridCommand = new RelayCommand<Semester6_MainProject.MyUserControl.ControlVideo>((p) => { return true; }, (p) =>
            {
                p.Visibility = Visibility.Visible;
                StartDispatcherTimer(p);
            });
        }

        private Semester6_MainProject.MyUserControl.ControlVideo GetMediaElement(FrameworkElement window)
        {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(window); i++)
            {
                var child = VisualTreeHelper.GetChild(window, i);
                if (child is Semester6_MainProject.MyUserControl.ControlVideo)
                {
                    return child as Semester6_MainProject.MyUserControl.ControlVideo;
                }
            }
            return null;
        }

        void StartDispatcherTimer(Semester6_MainProject.MyUserControl.ControlVideo p)
        {
            // Create a timer that will update the counters and the time slider
            count = 0;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0);
            timer.Tick += (sender, e) => TimerTick(sender, e, p);

            timer.Start();
        }
        void TimerTick(object sender, EventArgs e, Semester6_MainProject.MyUserControl.ControlVideo p)
        {
            count++;
            if (count > 10000)
            {
                p.Visibility = Visibility.Hidden;
                timer.Stop();
            }
        }

    }

}
