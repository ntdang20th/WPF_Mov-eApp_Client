
namespace Semester6_MainProject.MVVM.Model
{
    using System;
    using System.Collections.Generic;
    using Semester6_MainProject.MVVM.ViewModel;

    public partial class Luat : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Luat()
        {
            this.Cua_Phim_Luat = new HashSet<Cua_Phim_Luat>();
        }
        //my code
        private bool _check;
        public bool Check { get => _check; set { _check = value; OnPropertyChanged(); } }

        //end

        private string _TenLuat;
        public int Id { get; set; }
        public string TenLuat { get => _TenLuat; set { _TenLuat = value; OnPropertyChanged(); } }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cua_Phim_Luat> Cua_Phim_Luat { get; set; }
    }
}

