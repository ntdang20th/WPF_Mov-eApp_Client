using Semester6_MainProject.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Semester6_MainProject.MVVM.ViewModel
{
    class MostCommentViewModel : BaseViewModel
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
        #endregion

        #region Command
        public ICommand NgayCommand { get; set; }
        public ICommand TuanCommand { get; set; }
        public ICommand ThangCommand { get; set; }
        public ICommand MuaCommand { get; set; }
        public ICommand NamCommand { get; set; }
        public ICommand ToanBoCommand { get; set; }
        public ICommand UpdateValues { get; set; }
        #endregion


        public MostCommentViewModel()
        {
            ListPhim = new ObservableCollection<Phim>(DataProvider.Instance.DB.Phims.OrderByDescending(p => p.NguoiDung_BinhLuan_Phim.Count));

            NgayCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                List<int?> listIdPhim = DataProvider.Instance.DB.NguoiDung_BinhLuan_Phim.Where(t => t.NgayBinhLuan == DateTime.Today).Select(i => i.IdPhim).Distinct().ToList(); ;
                ListPhim = new ObservableCollection<Phim>(DataProvider.Instance.DB.Phims.Where(t => listIdPhim.Contains(t.Id)).OrderByDescending(n => n.NguoiDung_BinhLuan_Phim.Count));

            });
            TuanCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                int thisWeekNumber = MostWatchView.GetIso8601WeekOfYear(DateTime.Today);
                DateTime firstDayOfWeek = MostWatchView.FirstDateOfWeek(DateTime.Today.Year, thisWeekNumber, CultureInfo.CurrentCulture);
                DateTime lastDayOfWeek = firstDayOfWeek.AddDays(6);

                List<int?> listIdPhim = DataProvider.Instance.DB.NguoiDung_BinhLuan_Phim.Where(t => t.NgayBinhLuan >= firstDayOfWeek && t.NgayBinhLuan <= lastDayOfWeek).Select(i => i.IdPhim).Distinct().ToList(); ;
                ListPhim = new ObservableCollection<Phim>(DataProvider.Instance.DB.Phims.Where(t => listIdPhim.Contains(t.Id)).OrderByDescending(n => n.NguoiDung_BinhLuan_Phim.Count));
            });
            ThangCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                //ngay dau thang nay`
                DateTime firstDayOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                List<int?> listIdPhim = DataProvider.Instance.DB.NguoiDung_BinhLuan_Phim.Where(t => t.NgayBinhLuan >= firstDayOfMonth).Select(i => i.IdPhim).Distinct().ToList(); ;
                ListPhim = new ObservableCollection<Phim>(DataProvider.Instance.DB.Phims.Where(t => listIdPhim.Contains(t.Id)).OrderByDescending(n => n.NguoiDung_BinhLuan_Phim.Count));
            });

            MuaCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                int thangdaumua = MostWatchView.FistMonthOfSeason(DateTime.Today.Month);
                //ngay dau mua` nay`
                DateTime firstDayOfSeason = new DateTime(DateTime.Today.Year, thangdaumua, 1);


                List<int?> listIdPhim = DataProvider.Instance.DB.NguoiDung_BinhLuan_Phim.Where(t => t.NgayBinhLuan.Value >= firstDayOfSeason).Select(i => i.IdPhim).Distinct().ToList(); ;
                ListPhim = new ObservableCollection<Phim>(DataProvider.Instance.DB.Phims.Where(t => listIdPhim.Contains(t.Id)).OrderByDescending(n => n.NguoiDung_BinhLuan_Phim.Count));
            });

            NamCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                var listbyday = DataProvider.Instance.DB.NguoiDung_BinhLuan_Phim.Where(t => t.NgayBinhLuan.Value.Year == DateTime.Today.Year).Select(i => i.IdPhim).Distinct();
                ListPhim = new ObservableCollection<Phim>(DataProvider.Instance.DB.Phims.Where(t => listbyday.Contains(t.Id)).OrderByDescending(n => n.NguoiDung_BinhLuan_Phim.Count));
            });

            ToanBoCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                ListPhim = new ObservableCollection<Phim>(DataProvider.Instance.DB.Phims.OrderByDescending(n => n.NguoiDung_BinhLuan_Phim.Count));
            });

            UpdateValues = new RelayCommand<object>((p) => true, (p) =>
            {
                ListCmt = new ObservableCollection<NguoiDung_BinhLuan_Phim>(SelectedItem.NguoiDung_BinhLuan_Phim);
            });
        }

            #region Method
            void LoadAnh()
            {
                foreach (Phim p in ListPhim)
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
