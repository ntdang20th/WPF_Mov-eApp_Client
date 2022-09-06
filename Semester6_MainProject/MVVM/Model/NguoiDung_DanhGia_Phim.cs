

namespace Semester6_MainProject.MVVM.Model
{
    using System;
    using System.Collections.Generic;

    public partial class NguoiDung_DanhGia_Phim
    {
        public int Id { get; set; }
        public Nullable<int> IdNguoiDung { get; set; }
        public Nullable<int> IdPhim { get; set; }
        public Nullable<double> DiemSo { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
        public virtual Phim Phim { get; set; }
    }
}

