
namespace Semester6_MainProject.MVVM.Model
{
    using System;
    using System.Collections.Generic;
    using Semester6_MainProject.MVVM.ViewModel;

    public partial class TheLoai : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TheLoai()
        {
            this.Cua_Phim_TheLoai = new HashSet<Cua_Phim_TheLoai>();
        }

        //my code
        private bool _check;
        public bool Check { get => _check; set { _check = value; OnPropertyChanged(); } }

        //end

        private string _TenTheLoai;

        public int Id { get; set; }
        public string TenTheLoai { get => _TenTheLoai; set { _TenTheLoai = value; OnPropertyChanged(); } }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cua_Phim_TheLoai> Cua_Phim_TheLoai { get; set; }
    }
}

