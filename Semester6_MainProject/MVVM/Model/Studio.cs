
namespace Semester6_MainProject.MVVM.Model
{
    using System;
    using System.Collections.Generic;
    using Semester6_MainProject.MVVM.ViewModel;

    public partial class Studio : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Studio()
        {
            this.Cua_Phim_Studio = new HashSet<Cua_Phim_Studio>();
        }

        //my code
        private bool _check;
        public bool Check { get => _check; set { _check = value; OnPropertyChanged(); } }

        //end

        private string _TenStudio;
        private string _GhiChu;

        public int Id { get; set; }
        public string TenStudio { get => _TenStudio; set { _TenStudio = value; OnPropertyChanged(); } }
        public string GhiChu { get => _GhiChu; set { _GhiChu = value; OnPropertyChanged(); } }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cua_Phim_Studio> Cua_Phim_Studio { get; set; }
    }
}

