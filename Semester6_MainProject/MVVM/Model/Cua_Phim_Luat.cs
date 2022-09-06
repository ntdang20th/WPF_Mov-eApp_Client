namespace Semester6_MainProject.MVVM.Model
{
    using System;
    using System.Collections.Generic;
    using Semester6_MainProject.MVVM.ViewModel;
    public partial class Cua_Phim_Luat : BaseViewModel
    {

        private Nullable<int> _IdPhim;
        private Nullable<int> _IdLuat;


        public int Id { get; set; }
        public Nullable<int> IdPhim { get => _IdPhim; set { _IdPhim = value; OnPropertyChanged(); } }
        public Nullable<int> IdLuat { get => _IdLuat; set { _IdLuat = value; OnPropertyChanged(); } }

        public virtual Luat Luat { get; set; }
        public virtual Phim Phim { get; set; }
    }
}


