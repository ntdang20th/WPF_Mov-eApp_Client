using Semester6_MainProject.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Semester6_MainProject.MVVM.ViewModel
{
    class VIPViewModel : BaseViewModel
    {
        string duongdangoc = "D:\\data\\";
        #region List
        public ObservableCollection<Phim> _ListPhim;
        public ObservableCollection<Phim> ListPhim { get => _ListPhim; set { _ListPhim = value; OnPropertyChanged(); LoadAnh(); } }
        public ObservableCollection<NguoiDung_BinhLuan_Phim> _ListCmt;
        public ObservableCollection<NguoiDung_BinhLuan_Phim> ListCmt { get => _ListCmt; set { _ListCmt = value; OnPropertyChanged(); LoadAnh(); } }
        #endregion

        #region Properties
        private bool _canWatch;
        public bool canWatch { get => _canWatch; set { _canWatch = value; OnPropertyChanged(); } }

        private bool _canComment;
        public bool canComment { get => _canComment; set { _canComment = value; OnPropertyChanged(); } }
        #endregion




        #region SelectedItem
        private VIDEO _SelectedTapPhim;
        public VIDEO SelectedTapPhim
        {
            get => _SelectedTapPhim; set
            {
                _SelectedTapPhim = value;
                OnPropertyChanged();
                if (SelectedTapPhim != null)
                {
                    PlayFilmWindow pfw = new PlayFilmWindow(SelectedTapPhim.tenFileVideo, SelectedTapPhim.SoTap + " - " + SelectedItem.TenPhim);
                    pfw.ShowDialog();
                }
            }
        }

        private Phim _SelectedItem;
        public Phim SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value;
                OnPropertyChanged();

                if (SelectedItem != null)
                {
                    SelectedVisibleVideos = new ObservableCollection<VIDEO>(DataProvider.Instance.DB.VIDEOs.Where(v => v.IdPhim == SelectedItem.Id && !String.IsNullOrEmpty(v.tenFileVideo)));
                    canWatch = KtraQuyenXem();
                    canComment = KtraQuyenBinhLuan();
                    MainViewModel.idphim = SelectedItem.Id;
                    ListCmt = new ObservableCollection<NguoiDung_BinhLuan_Phim>(SelectedItem.NguoiDung_BinhLuan_Phim);
                }
            }
        }

        private ObservableCollection<VIDEO> _SelectedVisibleVideos;
        public ObservableCollection<VIDEO> SelectedVisibleVideos
        {
            get => _SelectedVisibleVideos; set
            {
                _SelectedVisibleVideos = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Command
        public ICommand MuaSauCommand { get; set; }
        public ICommand NamSauCommand { get; set; }
        public ICommand TatCaCommand { get; set; }
        public ICommand UpdateValues { get; set; }

        #endregion


        public VIPViewModel()
        {
            var luatVip = DataProvider.Instance.DB.Cua_Phim_Luat.Where(l => l.Luat.TenLuat == "VIP").Select(i=>i.IdPhim);
            ListPhim = new ObservableCollection<Phim>(DataProvider.Instance.DB.Phims.Where(p => luatVip.Contains(p.Id)));
        }

        #region Method
        void LoadAnh()
        {
            foreach (Phim p in ListPhim)
            {
                p.Anh = new Uri(duongdangoc + p.AnhDaiDiens.Single().tenFileAnh);

            }
        }

        string getMua(int thang)
        {
            if (1 <= thang && thang <= 3)
                return "Xuân";
            if (4 <= thang && thang <= 6)
                return "Hè";
            if (7 <= thang && thang <= 9)
                return "Thu";
            if (10 <= thang && thang <= 12)
                return "Đông";
            return null;
        }


        bool KtraQuyenXem()
        {
            if (MainViewModel.nd == null)
                return false;
            if (MainViewModel.danhsachquyen.Contains("Cấm xem"))
            {
                return false;
            }

            foreach (Cua_Phim_Luat i in SelectedItem.Cua_Phim_Luat)
            {
                if (i.Luat.TenLuat == "VIP" && MainViewModel.danhsachquyen.Contains("VIP"))
                {
                    return false;
                }

                if (MainViewModel.danhsachquyen.Contains("Mười tám cộng") && i.Luat.TenLuat == "Mười tám cộng")
                {
                    return false;
                }
            }

            return true;
        }

        bool KtraQuyenBinhLuan()
        {
            if (MainViewModel.nd == null)
                return false;
            if (MainViewModel.danhsachquyen.Contains("Cấm bình luận"))
            {
                return false;
            }

            foreach (Cua_Phim_Luat i in SelectedItem.Cua_Phim_Luat)
            {
                if (i.Luat.TenLuat == "VIP" && MainViewModel.danhsachquyen.Contains("VIP"))
                {
                    return false;
                }

                if (i.Luat.TenLuat == "Mười tám cộng" && MainViewModel.danhsachquyen.Contains("Mười tám cộng"))
                {
                    return false;
                }
            }

            return true;
        }
        #endregion
    }
}
