namespace Semester6_MainProject.MVVM.Model
{
    using System;
    using System.Collections.Generic;
    using Semester6_MainProject.MVVM.ViewModel;
    public partial class Cua_Phim_TheLoai : BaseViewModel
    {

        public Nullable<int> _IdPhim;
        public Nullable<int> _IdTheLoai;

        public int Id { get; set; }
        public Nullable<int> IdPhim { get => _IdPhim; set { _IdPhim = value; OnPropertyChanged(); } }
        public Nullable<int> IdTheLoai { get => _IdTheLoai; set { _IdTheLoai = value; OnPropertyChanged(); } }

        public virtual Phim Phim { get; set; }
        public virtual TheLoai TheLoai { get; set; }
    }
}
