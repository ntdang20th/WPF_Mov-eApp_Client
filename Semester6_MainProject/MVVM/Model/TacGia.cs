
namespace Semester6_MainProject.MVVM.Model
{
    using System;
    using System.Collections.Generic;
    using Semester6_MainProject.MVVM.ViewModel;

    public partial class TacGia : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TacGia()
        {
            this.Cua_Phim_TacGia = new HashSet<Cua_Phim_TacGia>();
        }

        //my code
        private bool _check;
        public bool Check { get => _check; set { _check = value; OnPropertyChanged(); } }

        //end



        private string _TenTacGia;
        private string _GioiTinh;
        private string _QuocTich;
        private Nullable<System.DateTime> _NgaySinh;



        public int Id { get; set; }
        public string TenTacGia { get => _TenTacGia; set { _TenTacGia = value; OnPropertyChanged(); } }
        public string GioiTinh { get => _GioiTinh; set { _GioiTinh = value; OnPropertyChanged(); } }
        public Nullable<System.DateTime> NgaySinh { get => _NgaySinh; set { _NgaySinh = value; OnPropertyChanged(); } }
        public string QuocTich { get => _QuocTich; set { _QuocTich = value; OnPropertyChanged(); } }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cua_Phim_TacGia> Cua_Phim_TacGia { get; set; }
    }
}

