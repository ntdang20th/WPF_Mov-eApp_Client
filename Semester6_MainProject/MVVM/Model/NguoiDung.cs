namespace Semester6_MainProject.MVVM.Model
{
    using System;
    using System.Collections.Generic;
    using Semester6_MainProject.MVVM.ViewModel;



    public partial class NguoiDung : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NguoiDung()
        {
            this.Cua_NguoiDung_Quyen = new HashSet<Cua_NguoiDung_Quyen>();
            this.NguoiDung_Xem_Phim = new HashSet<NguoiDung_Xem_Phim>();
            this.NguoiDung_BinhLuan_Phim = new HashSet<NguoiDung_BinhLuan_Phim>();
            this.NguoiDung_DanhGia_Phim = new HashSet<NguoiDung_DanhGia_Phim>();
        }
        private double _danhgiacuaban;
        public double danhgiacuaban { get => _danhgiacuaban; set { _danhgiacuaban = value; OnPropertyChanged(); } }

        private string _TenDangNhap;
        private string _MatKhau;
        private Nullable<int> _IdThongTin;

        public int Id { get; set; }
        public string TenDangNhap { get => _TenDangNhap; set { _TenDangNhap = value; OnPropertyChanged(); } }
        public string MatKhau { get => _MatKhau; set { _MatKhau = value; OnPropertyChanged(); } }
        public Nullable<int> IdThongTin { get => _IdThongTin; set { _IdThongTin = value; OnPropertyChanged(); } }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cua_NguoiDung_Quyen> Cua_NguoiDung_Quyen { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiDung_Xem_Phim> NguoiDung_Xem_Phim { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiDung_BinhLuan_Phim> NguoiDung_BinhLuan_Phim { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiDung_DanhGia_Phim> NguoiDung_DanhGia_Phim { get; set; }
        public virtual ThongTin_NguoiDung ThongTin_NguoiDung { get; set; }
    }
}
