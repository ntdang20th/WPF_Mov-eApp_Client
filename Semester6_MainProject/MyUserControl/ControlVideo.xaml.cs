using Semester6_MainProject.MVVM.ViewModel;
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

namespace Semester6_MainProject.MyUserControl
{
    /// <summary>
    /// Interaction logic for ControlVideo.xaml
    /// </summary>
    public partial class ControlVideo : UserControl
    {
        public ControlVideo()
        {
            InitializeComponent();
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
        private void sliderForward_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            //get grid is parent of my usercontrol
            FrameworkElement gridParent = bangdieukhien.Parent as FrameworkElement;

            //get mediaElement is  a child of gridParent
            MediaElement video = GetMediaElement(gridParent);

            TimeSpan t = TimeSpan.FromSeconds(sliderForward.Value);

            // Overloaded constructor takes the arguments days, hours, minutes, seconds, milliseconds.
            // Create a TimeSpan with miliseconds equal to the slider value.
            video.Position = t;

            video.Play();

            ControlVideoViewModel.timerVideoTime.Start();
        }
        private void sliderForward_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            ControlVideoViewModel.timerVideoTime.Stop();
            //get grid is parent of my usercontrol
            FrameworkElement gridParent = bangdieukhien.Parent as FrameworkElement;

            //get mediaElement is  a child of gridParent
            MediaElement video = GetMediaElement(gridParent);

            video.Pause();
        }
    }
}
