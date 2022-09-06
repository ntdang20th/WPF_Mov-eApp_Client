namespace Semester6_MainProject.MVVM.Model
{
    using System;
    using System.Collections.Generic;
    using Semester6_MainProject.MVVM.ViewModel;

    public partial class VIDEO : BaseViewModel
    {

        public Nullable<int> _IdPhim;
        public string _SoTap;
        public string _tenFileVideo;


        public int Id { get; set; }
        public string SoTap { get => _SoTap; set { _SoTap = value; OnPropertyChanged(); } }
        public Nullable<int> IdPhim { get => _IdPhim; set { _IdPhim = value; OnPropertyChanged(); } }
        public string tenFileVideo { get => _tenFileVideo; set { _tenFileVideo = value; OnPropertyChanged(); } }


        public virtual Phim Phim { get; set; }
    }
}

