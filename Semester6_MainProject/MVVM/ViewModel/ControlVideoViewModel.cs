using Semester6_MainProject.MyUserControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Semester6_MainProject.MVVM.ViewModel
{
    public class ControlVideoViewModel : BaseViewModel
    {
        bool isPause = false;
        bool isFullScreen = false;
        bool isMute = false;
        public static DispatcherTimer timerVideoTime;

        private double _totalSeconds;
        public double totalSeconds { get => _totalSeconds; set { _totalSeconds = value; OnPropertyChanged(); } }

        private double _duringSeconds;
        public double duringSeconds
        {
            get => _duringSeconds; set
            {
                _duringSeconds = value;
                OnPropertyChanged();

            }
        }

        private string _IconKind;
        public string IconKind { get => _IconKind; set { _IconKind = value; OnPropertyChanged(); } }

        private string _FullCreenIcon;
        public string FullCreenIcon { get => _FullCreenIcon; set { _FullCreenIcon = value; OnPropertyChanged(); } }

        private string _MuteIcon;
        public string MuteIcon { get => _MuteIcon; set { _MuteIcon = value; OnPropertyChanged(); } }

        private string _TotalTime;
        public string TotalTime { get => _TotalTime; set { _TotalTime = value; OnPropertyChanged(); } }

        private string _DuringTime;
        public string DuringTime { get => _DuringTime; set { _DuringTime = value; OnPropertyChanged(); } }



        public ICommand PlayCommand { get; set; }
        public ICommand PauseCommand { get; set; }
        public ICommand FullScreenCommand { get; set; }

        public ICommand MouseEnterVolumeCommand { get; set; }
        public ICommand MuteVolumeCommand { get; set; }
        public ICommand MouseLeaveVolumeCommand { get; set; }
        public ICommand MouseLeaveSliderCommand { get; set; }
        public ICommand ValueChangedSliderCommand { get; set; }
        public ICommand Rewind15Command { get; set; }
        public ICommand Fast15Command { get; set; }
        public ICommand Speed075 { get; set; }
        public ICommand Speed100 { get; set; }
        public ICommand Speed125 { get; set; }
        public ICommand Speed150 { get; set; }
        public ICommand Speed175 { get; set; }
        public ICommand ValueChangedSliderForwardCommand { get; set; }
        public ICommand ThumbDragStartedCommand { get; set; }
        public ICommand ThumbDragCompletedCommand { get; set; }


        public ControlVideoViewModel()
        {
            IconKind = "Pause";
            FullCreenIcon = "FullScreen";
            MuteIcon = "VolumeHigh";

            PlayCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                //get grid is parent of my usercontrol
                FrameworkElement gridParent = GetUsercontorlParent(p);

                //get mediaElement is  a child of gridParent
                MediaElement video = GetMediaElement(gridParent);

                video.Play();
                p.Visibility = Visibility.Hidden;

                while (video.NaturalDuration.HasTimeSpan == false)
                {
                    Thread.Sleep(100);
                }

                TotalTime = video.NaturalDuration.TimeSpan.ToString(@"mm\:ss");
                totalSeconds = (int)video.NaturalDuration.TimeSpan.TotalSeconds;
                DisplayDispatcherTimer(video);
            });

            PauseCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                //get grid is parent of my usercontrol
                FrameworkElement gridParent = GetUsercontorlParent(p);

                //get mediaElement is  a child of gridParent
                MediaElement video = GetMediaElement(gridParent);

                if (!isPause)
                {
                    video.Pause();
                    IconKind = "Play";
                }
                else
                {
                    video.Play();
                    IconKind = "Pause";
                }
                isPause = !isPause;
            });

            MouseEnterVolumeCommand = new RelayCommand<Slider>((p) => { return true; }, (p) =>
            {
                p.Visibility = Visibility.Visible;
            });

            MuteVolumeCommand = new RelayCommand<Slider>((p) => { return true; }, (p) =>
            {
                if (!isMute)
                {
                    p.Value = 0;
                    MuteIcon = "VolumeMute";
                }
                else
                {
                    p.Value = 0.25;
                    MuteIcon = "VolumeHigh";
                }
                isMute = !isMute;
            });

            MouseLeaveSliderCommand = new RelayCommand<Slider>((p) => { return true; }, (p) =>
            {
                p.Visibility = Visibility.Hidden;
            });

            ValueChangedSliderCommand = new RelayCommand<Slider>((p) => { return true; }, (p) =>
            {
                //get grid is parent of my usercontrol
                FrameworkElement gridParent = GetUsercontorlParent(p);

                //get mediaElement is  a child of gridParent
                MediaElement video = GetMediaElement(gridParent);

                video.Volume = (double)p.Value;
            });

            FullScreenCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                //get window is parent of my usercontrol
                Window window = GetWindowParent(p) as Window;

                if (!isFullScreen)
                {
                    window.WindowStyle = WindowStyle.None;
                    window.WindowState = WindowState.Maximized;
                }
                else
                {
                    window.WindowStyle = WindowStyle.SingleBorderWindow;
                    window.WindowState = WindowState.Normal;
                }

                isFullScreen = !isFullScreen;
                FullCreenIcon = "FullscreenExit";
            });

            Rewind15Command = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                //get grid is parent of my usercontrol
                FrameworkElement gridParent = GetUsercontorlParent(p);

                //get mediaElement is  a child of gridParent
                MediaElement video = GetMediaElement(gridParent);

                TimeSpan t = TimeSpan.FromSeconds(duringSeconds - 15);

                // Overloaded constructor takes the arguments days, hours, minutes, seconds, milliseconds.
                // Create a TimeSpan with miliseconds equal to the slider value.
                video.Position = t;


            });

            Fast15Command = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                //get grid is parent of my usercontrol
                FrameworkElement gridParent = GetUsercontorlParent(p);

                //get mediaElement is  a child of gridParent
                MediaElement video = GetMediaElement(gridParent);

                TimeSpan t = TimeSpan.FromSeconds(duringSeconds + 15);

                // Overloaded constructor takes the arguments days, hours, minutes, seconds, milliseconds.
                // Create a TimeSpan with miliseconds equal to the slider value.
                video.Position = t;


            });

            Speed075 = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                //get grid is parent of my usercontrol
                FrameworkElement gridParent = GetUsercontorlParent(p);

                //get mediaElement is  a child of gridParent
                MediaElement video = GetMediaElement(gridParent);

                video.SpeedRatio = 0.75;
            });

            Speed100 = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                //get grid is parent of my usercontrol
                FrameworkElement gridParent = GetUsercontorlParent(p);

                //get mediaElement is  a child of gridParent
                MediaElement video = GetMediaElement(gridParent);

                video.SpeedRatio = 1;
            });

            Speed125 = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                //get grid is parent of my usercontrol
                FrameworkElement gridParent = GetUsercontorlParent(p);

                //get mediaElement is  a child of gridParent
                MediaElement video = GetMediaElement(gridParent);

                video.SpeedRatio = 1.25;
            });

            Speed150 = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                //get grid is parent of my usercontrol
                FrameworkElement gridParent = GetUsercontorlParent(p);

                //get mediaElement is  a child of gridParent
                MediaElement video = GetMediaElement(gridParent);

                video.SpeedRatio = 1.50;
            });

            Speed175 = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                //get grid is parent of my usercontrol
                FrameworkElement gridParent = GetUsercontorlParent(p);

                //get mediaElement is  a child of gridParent
                MediaElement video = GetMediaElement(gridParent);

                video.SpeedRatio = 1.75;
            });


            #region khu khu vực command không hoạt động được nên đành viết code behind :D
            //ValueChangedSliderForwardCommand = new RelayCommand<Slider>((p) => { return true; }, (p) =>
            //{
            //    //timerVideoTime.Stop();
            //    //get grid is parent of my usercontrol
            //    FrameworkElement gridParent = GetUsercontorlParent(p);

            //    //get mediaElement is  a child of gridParent
            //    MediaElement video = GetMediaElement(gridParent);

            //    video.Pause();

            //    TimeSpan t = TimeSpan.FromSeconds(p.Value);

            //    // Overloaded constructor takes the arguments days, hours, minutes, seconds, milliseconds.
            //    // Create a TimeSpan with miliseconds equal to the slider value.
            //    video.Position = t;
            //    video.Play();

            //    //DisplayDispatcherTimer(video, SeekValue);

            //});

            //ThumbDragStartedCommand = new RelayCommand<Slider>((p) => { return true; }, (p) =>
            //{
            //    //get grid is parent of my usercontrol
            //    FrameworkElement gridParent = GetUsercontorlParent(p);

            //    //get mediaElement is  a child of gridParent
            //    MediaElement video = GetMediaElement(gridParent);

            //    video.Pause();
            //    //DisplayDispatcherTimer(video, SeekValue);

            //});

            //ThumbDragCompletedCommand = new RelayCommand<Slider>((p) => { return true; }, (p) =>
            //{
            //    //get grid is parent of my usercontrol
            //    FrameworkElement gridParent = GetUsercontorlParent(p);

            //    //get mediaElement is  a child of gridParent
            //    MediaElement video = GetMediaElement(gridParent);

            //    TimeSpan t = TimeSpan.FromSeconds(p.Value);

            //    // Overloaded constructor takes the arguments days, hours, minutes, seconds, milliseconds.
            //    // Create a TimeSpan with miliseconds equal to the slider value.
            //    video.Position = t;
            //    video.Play();
            //});
            #endregion
        }

        //thoi luong video
        public void DisplayDispatcherTimer(MediaElement video)
        {
            // Create a timer that will update the counters and the time slider

            timerVideoTime = new DispatcherTimer();
            timerVideoTime.Interval = TimeSpan.FromSeconds(0);
            timerVideoTime.Tick += (sender, e) => TimerTick(sender, e, video);

            timerVideoTime.Start();
        }

        public void TimerTick(object sender, EventArgs e, MediaElement video)
        {
            duringSeconds = video.Position.TotalSeconds;
            TimeSpan t = TimeSpan.FromSeconds(duringSeconds);
            DuringTime = t.ToString(@"mm\:ss");
        }

        private FrameworkElement GetWindowParent(UserControl p)
        {
            FrameworkElement parent = p;
            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }
            return parent;
        }

        private FrameworkElement GetUsercontorlParent(FrameworkElement p)
        {
            FrameworkElement parent = p;
            while (parent != null)
            {
                parent = parent.Parent as FrameworkElement;
                if (parent is Semester6_MainProject.MyUserControl.ControlVideo)
                {
                    return parent.Parent as FrameworkElement;
                }
            }
            return null;
        }

        private MediaElement GetMediaElement(FrameworkElement grid)
        {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(grid); i++)
            {
                var child = VisualTreeHelper.GetChild(grid, i);
                if (child is MediaElement)
                {
                    return child as MediaElement;
                }
            }
            return null;
        }
    }
}
