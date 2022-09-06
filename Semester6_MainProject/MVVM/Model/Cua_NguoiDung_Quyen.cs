namespace Semester6_MainProject.MVVM.Model
{
    using System;
    using System.Collections.Generic;
    using Semester6_MainProject.MVVM.ViewModel;
    public partial class Cua_NguoiDung_Quyen : BaseViewModel
    {

        private Nullable<int> _IdNguoiDung;
        private Nullable<int> _IdQuyen;


        public int Id { get; set; }
        public Nullable<int> IdNguoiDung { get => _IdNguoiDung; set { _IdNguoiDung = value; OnPropertyChanged(); } }
        public Nullable<int> IdQuyen { get => _IdQuyen; set { _IdQuyen = value; OnPropertyChanged(); } }


        public virtual NguoiDung NguoiDung { get; set; }
        public virtual Quyen Quyen { get; set; }
    }
}
