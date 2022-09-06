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
    class HomeViewModel : BaseViewModel
    {
        string duongdangoc = "D:\\data\\";
        #region List
        public ObservableCollection<Phim> _ListDeXuat;
        public ObservableCollection<Phim> ListDeXuat { get => _ListDeXuat; set { _ListDeXuat = value; OnPropertyChanged(); LoadAnh(); } }

        public ObservableCollection<Phim> _ListNews;
        public ObservableCollection<Phim> ListNews { get => _ListNews; set { _ListNews = value; OnPropertyChanged(); LoadAnh(); } }

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
                    ListCmt = new ObservableCollection<NguoiDung_BinhLuan_Phim>(SelectedItem.NguoiDung_BinhLuan_Phim);
                    MainViewModel.idphim = SelectedItem.Id;
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
        #endregion

        #region Command
        public ICommand UpdateValues { get; set; }
        #endregion


        public HomeViewModel()
        {
            Random rand = new Random();
            int toSkip = rand.Next(0, DataProvider.Instance.DB.Phims.Count());

            ListNews = new ObservableCollection<Phim>(DataProvider.Instance.DB.Phims.OrderByDescending(p => p.Id).Take(3));
            ListDeXuat = new ObservableCollection<Phim>(DataProvider.Instance.DB.Phims.OrderBy(r => Guid.NewGuid()).Skip(toSkip).Take(5));

            UpdateValues = new RelayCommand<object>((p) => true, (p) =>
            {
                ListCmt = new ObservableCollection<NguoiDung_BinhLuan_Phim>(SelectedItem.NguoiDung_BinhLuan_Phim);
            });
        }

        #region Method
        void LoadAnh()
        {
            if (ListNews != null)
                foreach (Phim p in ListNews)
                {
                    p.Anh = new Uri(duongdangoc + p.AnhDaiDiens.Single().tenFileAnh);
                }
            if (ListDeXuat != null)
                foreach (Phim p in ListDeXuat)
                {
                    p.Anh = new Uri(duongdangoc + p.AnhDaiDiens.Single().tenFileAnh);
                }
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