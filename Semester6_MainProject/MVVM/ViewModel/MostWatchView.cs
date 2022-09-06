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
    class MostWatchView : BaseViewModel
    {
        double diem;

        bool isLoaded = false;
        string duongdangoc = "D:\\data\\";
        #region List
        public ObservableCollection<Phim> _ListPhim;
        public ObservableCollection<Phim> ListPhim { get => _ListPhim; set { _ListPhim = value; OnPropertyChanged(); LoadAnh(); } }

        public ObservableCollection<VIDEO> _ListVideo;
        public ObservableCollection<VIDEO> ListVideo { get => _ListVideo; set { _ListVideo = value; OnPropertyChanged();} }

        public ObservableCollection<NguoiDung_BinhLuan_Phim> _ListCmt;
        public ObservableCollection<NguoiDung_BinhLuan_Phim> ListCmt { get => _ListCmt; set { _ListCmt = value; OnPropertyChanged(); } }

        #endregion

        #region Properties
        private bool _canWatch;
        public bool canWatch { get => _canWatch; set { _canWatch = value; OnPropertyChanged(); } }

        private bool _canComment;
        public bool canComment { get => _canComment; set { _canComment = value; OnPropertyChanged(); } }


        #endregion




        #region SelectedItem
        private Phim _SelectedItem;
        public Phim SelectedItem { get => _SelectedItem; set 
            { 
                _SelectedItem = value;
                OnPropertyChanged();

                
                if (SelectedItem!=null)
                {
                    SelectedVisibleVideos = new ObservableCollection<VIDEO>( DataProvider.Instance.DB.VIDEOs.Where(v => v.IdPhim == SelectedItem.Id && !String.IsNullOrEmpty(v.tenFileVideo)));

                    ListCmt = new ObservableCollection<NguoiDung_BinhLuan_Phim>(SelectedItem.NguoiDung_BinhLuan_Phim);
                    
                    canWatch = KtraQuyenXem();
                    canComment = KtraQuyenBinhLuan();

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
        public VIDEO SelectedTapPhim { get => _SelectedTapPhim; set 
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
        public ICommand NgayCommand { get; set; }
        public ICommand TuanCommand { get; set; }
        public ICommand ThangCommand { get; set; }
        public ICommand MuaCommand { get; set; }
        public ICommand NamCommand { get; set; }
        public ICommand ToanBoCommand { get; set; }
        public ICommand UpdateValues { get; set; }
        #endregion


        public MostWatchView()
        {
            
            ListPhim = new ObservableCollection<Phim>(DataProvider.Instance.DB.Phims.OrderByDescending(p=>p.NguoiDung_Xem_Phim.Count));

            NgayCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                List<int?> listIdPhim = DataProvider.Instance.DB.NguoiDung_Xem_Phim.Where(t => t.NgayXem == DateTime.Today).Select(i => i.IdPhim).Distinct().ToList(); ;
                ListPhim = new ObservableCollection<Phim>(DataProvider.Instance.DB.Phims.Where(t=> listIdPhim.Contains(t.Id)).OrderByDescending(n => n.NguoiDung_Xem_Phim.Count));
                
            });

            TuanCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                int thisWeekNumber = GetIso8601WeekOfYear(DateTime.Today);
                DateTime firstDayOfWeek = FirstDateOfWeek(DateTime.Today.Year, thisWeekNumber, CultureInfo.CurrentCulture);
                DateTime lastDayOfWeek = firstDayOfWeek.AddDays(6);

                List<int?> listIdPhim = DataProvider.Instance.DB.NguoiDung_Xem_Phim.Where(t => t.NgayXem >= firstDayOfWeek && t.NgayXem <=lastDayOfWeek).Select(i => i.IdPhim).Distinct().ToList(); ;
                ListPhim = new ObservableCollection<Phim>(DataProvider.Instance.DB.Phims.Where(t => listIdPhim.Contains(t.Id)).OrderByDescending(n => n.NguoiDung_Xem_Phim.Count));
            });

            ThangCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                //ngay dau thang nay`
                DateTime firstDayOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                List<int?> listIdPhim = DataProvider.Instance.DB.NguoiDung_Xem_Phim.Where(t => t.NgayXem >= firstDayOfMonth).Select(i => i.IdPhim).Distinct().ToList(); ;
                ListPhim = new ObservableCollection<Phim>(DataProvider.Instance.DB.Phims.Where(t => listIdPhim.Contains(t.Id)).OrderByDescending(n => n.NguoiDung_Xem_Phim.Count));
            });

            MuaCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                int thangdaumua = FistMonthOfSeason(DateTime.Today.Month);
                //ngay dau mua` nay`
                DateTime firstDayOfSeason= new DateTime(DateTime.Today.Year, thangdaumua, 1);
                

                List<int?> listIdPhim = DataProvider.Instance.DB.NguoiDung_Xem_Phim.Where(t => t.NgayXem.Value >= firstDayOfSeason).Select(i => i.IdPhim).Distinct().ToList(); ;
                ListPhim = new ObservableCollection<Phim>(DataProvider.Instance.DB.Phims.Where(t => listIdPhim.Contains(t.Id)).OrderByDescending(n => n.NguoiDung_Xem_Phim.Count));
            });

            NamCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                var listbyday = DataProvider.Instance.DB.NguoiDung_Xem_Phim.Where(t => t.NgayXem.Value.Year == DateTime.Today.Year).Select(i => i.IdPhim).Distinct();
                ListPhim = new ObservableCollection<Phim>(DataProvider.Instance.DB.Phims.Where(t => listbyday.Contains(t.Id)).OrderByDescending(n => n.NguoiDung_Xem_Phim.Count));
            });

            ToanBoCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                ListPhim = new ObservableCollection<Phim>(DataProvider.Instance.DB.Phims.OrderByDescending(n => n.NguoiDung_Xem_Phim.Count));
            });

            UpdateValues = new RelayCommand<object>((p) => true, (p) =>
            {
                ListCmt = new ObservableCollection<NguoiDung_BinhLuan_Phim>(SelectedItem.NguoiDung_BinhLuan_Phim);
            });
        }

        #region Method
        public static int GetIso8601WeekOfYear(DateTime time)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static DateTime FirstDateOfWeek(int year, int weekOfYear, System.Globalization.CultureInfo ci)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = (int)ci.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            DateTime firstWeekDay = jan1.AddDays(daysOffset);
            int firstWeek = ci.Calendar.GetWeekOfYear(jan1, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
            if ((firstWeek <= 1 || firstWeek >= 52) && daysOffset >= -3)
            {
                weekOfYear -= 1;
            }
            return firstWeekDay.AddDays(weekOfYear * 7);
        }

        public static int FistMonthOfSeason(int month)
        {
            if(month < 4)
            {
                return 1;
            }
            else if(month < 7)
            {
                return 4;
            }
            else if(month < 10)
            {
                return 7;
            }
            return 10;
        }


        void LoadAnh()
        {
            if (ListPhim.Count ==0)
            {
                return;
            }
            var temp = ListPhim.First();
            bool flag = false;
            if (!String.IsNullOrEmpty(temp.toStringStudio))
                flag = true;
            foreach (Phim p in ListPhim)
            {
                //anh
                p.Anh = new Uri(duongdangoc + p.AnhDaiDiens.Single().tenFileAnh);


                if (flag)
                    break;
                #region tostring
                
                //tostring the loai
                List<int?> ListIdTheLoai = DataProvider.Instance.DB.Cua_Phim_TheLoai.Where(c => c.IdPhim == p.Id).Select(t => t.IdTheLoai).ToList();
                foreach (int i in ListIdTheLoai)
                {
                    p.toStringTheLoai = p.toStringTheLoai + DataProvider.Instance.DB.TheLoais.Where(t => t.Id == i).Select(t => t.TenTheLoai).SingleOrDefault() + ", ";
                }

                //tostring tac gia
                List<int?> ListIdTacGia = DataProvider.Instance.DB.Cua_Phim_TacGia.Where(c => c.IdPhim == p.Id).Select(t => t.IdTacGia).ToList();
                foreach (int i in ListIdTacGia)
                {
                    p.toStringTacGia = p.toStringTacGia + DataProvider.Instance.DB.TacGias.Where(t => t.Id == i).Select(t => t.TenTacGia).SingleOrDefault() + ", ";
                }

                //tostring studio
                List<int?> ListIdStudio = DataProvider.Instance.DB.Cua_Phim_Studio.Where(c => c.IdPhim == p.Id).Select(t => t.IdStudio).ToList();
                foreach (int i in ListIdStudio)
                {
                    p.toStringStudio = p.toStringStudio + DataProvider.Instance.DB.Studios.Where(t => t.Id == i).Select(t => t.TenStudio).SingleOrDefault() + ", ";
                }

                p.LuotXem = p.NguoiDung_Xem_Phim.Count;
                p.LuotBinhLuan = p.NguoiDung_BinhLuan_Phim.Count;

                p.LuotDanhGia = p.NguoiDung_DanhGia_Phim.Count;
                if (p.LuotDanhGia == 0)
                    p.DiemSo = 0;
                else
                {
                    p.DiemSo = Math.Round((double)p.NguoiDung_DanhGia_Phim.Sum(t => t.DiemSo) / p.LuotDanhGia, 1);
                }
                #endregion
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

                if (MainViewModel.danhsachquyen.Contains("Mười tám cộng") && i.Luat.TenLuat=="Mười tám cộng")
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
