using MaterialDesignThemes.Wpf;
using Semester6_MainProject.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Semester6_MainProject.MVVM.ViewModel
{
    class MainViewModel:BaseViewModel
    {
        bool IsLoaded = false;
        private static NguoiDung _nd;
        public static NguoiDung nd { get=>_nd; set { _nd = value; } }
        public static int idphim;
        public static List<string> danhsachquyen;
        public static string textSearch;

        private bool _BtnVip = false;
        public bool BtnVip { get => _BtnVip; set { _BtnVip = value; OnPropertyChanged(); } }

        #region Relaycommand
        public ICommand HomeViewCommand { get; set; }
        public ICommand MostWatchCommand { get; set; }
        public ICommand ComingSoonCommand { get; set; }
        public ICommand FilterCommand { get; set; }
        public ICommand HighRatingCommand { get; set; }
        public ICommand MostCommentCommand { get; set; }
        public ICommand HistoryCommand { get; set; }
        public ICommand VIPCommand { get; set; }
        public ICommand InforCommand { get; set; }

        public ICommand ItemCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }

        public ICommand LogoutCommand { get; set; }
        public ICommand PhanQuyenCommand { get; set; }

        public static ICommand DanhGiaCommand { get; set; }
        public static ICommand BinhLuanCommand { get; set; }
        public static ICommand XemCommand { get; set; }
        public static ICommand EnterCommand { get; set; }
        #endregion


        #region Viewmodel
        public HomeViewModel HomeVM { get; set; }
        public MostWatchView MostWatchVM { get; set; }
        public ComingSoonViewModel ComingSoonVM { get; set; }
        public FilterViewModel FilterVM { get; set; }
        public HighRatingViewModel HighRatingVM { get; set; }
        public MostCommentViewModel MostCommentVM { get; set; }
        public HistoryViewModel HistoryVM { get; set; }
        public ItemViewModel ItemVM { get; set; }
        public ResultSearchViewModel SearchVM { get; set; }
        public VIPViewModel VIPVM { get; set; }
        public AccountInforViewModel AccountInforVM { get; set; }


        #endregion

        #region propẻtíe

        #endregion
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel() 
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                if (p == null)
                    return;
                IsLoaded = true;

                p.Hide();
                LoginWindow l = new LoginWindow();
                l.ShowDialog();

                var loginVM = l.DataContext as LoginViewModel;
                if (loginVM == null)
                    return;
                if (loginVM.IsLogin)
                {
                    p.Show();
                }
                else
                {
                    p.Close();
                    return;
                }

                nd = DataProvider.Instance.DB.NguoiDungs.Where(n => n.Id == loginVM.idnguoidung).SingleOrDefault();
                DanhSachQUyen();

                if (danhsachquyen.Contains("BAN"))
                {
                    MessageBox.Show("Tài khoản này đã bị khóa.", "Xin là xin vĩnh biệt!");
                    p.Close();
                    return;
                }
                
            });




            HomeVM = new HomeViewModel();
            MostWatchVM = new MostWatchView();
            ComingSoonVM = new ComingSoonViewModel();
            FilterVM = new FilterViewModel();
            HighRatingVM = new HighRatingViewModel();
            MostCommentVM = new MostCommentViewModel();
            HistoryVM = new HistoryViewModel();
            SearchVM = new ResultSearchViewModel();
            VIPVM = new VIPViewModel();
            AccountInforVM = new AccountInforViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                CurrentView = HomeVM;
            });
            MostWatchCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                CurrentView = MostWatchVM;
            });
            ComingSoonCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                CurrentView = ComingSoonVM;
            });
            FilterCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                CurrentView = FilterVM;
            });
            HighRatingCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                CurrentView = HighRatingVM;
            });
            MostCommentCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                CurrentView = MostCommentVM;
            });
            HistoryCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                CurrentView = HistoryVM;
            });
            ItemCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                CurrentView = ItemVM;
            });

            InforCommand = new RelayCommand<StackPanel>((p) => true, (p) =>
            {
                CurrentView = AccountInforVM;
                foreach (object child in p.Children)
                {
                    if (child is FrameworkElement && child is RadioButton)
                    {
                        (child as RadioButton).IsChecked = false;
                    }
                }
            });

            VIPCommand = new RelayCommand<StackPanel>((p) => true, (p) =>
            {
                CurrentView = VIPVM;
                foreach (object child in p.Children)
                {
                    if (child is FrameworkElement && child is RadioButton)
                    {
                        (child as RadioButton).IsChecked = false;
                    }
                }
            });

            LogoutCommand = new RelayCommand<Window>((p) => true, (p) =>
            {
                MainWindow newWindow = new MainWindow();
                Application.Current.MainWindow = newWindow;
                p.Close();
                newWindow.Show();
                
            });


            DanhGiaCommand = new RelayCommand<RatingBar>((p) => true, (p) =>
            {
                var x = DataProvider.Instance.DB.NguoiDung_DanhGia_Phim.Where(t => t.IdNguoiDung == nd.Id && t.IdPhim == idphim).SingleOrDefault();
                if (x != null)
                {
                    //nguowif dungf dadx dandsh gia, chỉ cần sửa kết quả lại thôi
                    x.DiemSo = p.Value;
                    DataProvider.Instance.DB.SaveChanges();

                    var ph = DataProvider.Instance.DB.Phims.Where(t => t.Id == idphim).SingleOrDefault();
                    ph.DiemSo = Math.Round((double)ph.NguoiDung_DanhGia_Phim.Sum(t => t.DiemSo) / ph.NguoiDung_DanhGia_Phim.Count, 1);
                    MessageBox.Show("Cảm ơn bạn đã dánh giá lại <3", "Tích cực weitei vận may sẽ tới!");
                    return;
                }
                //them danh gia
                NguoiDung_DanhGia_Phim d = new NguoiDung_DanhGia_Phim() {IdNguoiDung= nd.Id, IdPhim = idphim, DiemSo=p.Value};
                DataProvider.Instance.DB.NguoiDung_DanhGia_Phim.Add(d);
                DataProvider.Instance.DB.SaveChanges();

                var phim = DataProvider.Instance.DB.Phims.Where(t => t.Id == idphim).SingleOrDefault();
                phim.DiemSo = Math.Round((double)phim.NguoiDung_DanhGia_Phim.Sum(t => t.DiemSo) / phim.NguoiDung_DanhGia_Phim.Count, 1);
                phim.LuotDanhGia = phim.NguoiDung_DanhGia_Phim.Count;


                MessageBox.Show("Cảm ơn bạn đã dánh giá phim <3", "Tích cực weitei vận may sẽ tới!");

                OnPropertyChanged("SelectedItem");
            });

            BinhLuanCommand = new RelayCommand<TextBox>((p) =>
            {
                if (p==null || String.IsNullOrEmpty(p.Text))
                    return false;
                return true;
            }, (p) =>
            {
                //them binh luan
                NguoiDung_BinhLuan_Phim d = new NguoiDung_BinhLuan_Phim() {NgayBinhLuan=DateTime.Now, IdNguoiDung = nd.Id, IdPhim = idphim, NoiDung = p.Text };
                DataProvider.Instance.DB.NguoiDung_BinhLuan_Phim.Add(d);
                DataProvider.Instance.DB.SaveChanges();

                var phim = DataProvider.Instance.DB.Phims.Where(t => t.Id == idphim).SingleOrDefault();
                phim.LuotBinhLuan = phim.NguoiDung_BinhLuan_Phim.Count;
                MessageBox.Show("Cảm ơn bạn đã bình luận phim <3", "Tích cực weitei vận may sẽ tới!");
            });

            XemCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                //them luot xem
                NguoiDung_Xem_Phim d = new NguoiDung_Xem_Phim() { NgayXem = DateTime.Now, IdNguoiDung = nd.Id, IdPhim = idphim};
                DataProvider.Instance.DB.NguoiDung_Xem_Phim.Add(d);
                DataProvider.Instance.DB.SaveChanges();

                var phim = DataProvider.Instance.DB.Phims.Where(t => t.Id == idphim).SingleOrDefault();
                phim.LuotXem = phim.NguoiDung_Xem_Phim.Count;
            });

            EnterCommand = new RelayCommand<TextBox>((p) => true, (p) =>
            {
                textSearch = p.Text;

                if (CurrentView == SearchVM)
                {
                    CurrentView = null;
                    
                    return;
                }
                    
                CurrentView = SearchVM = new ResultSearchViewModel();
                p.Text = "";
            });
        }

        //hamf
        void DanhSachQUyen()
        {
            danhsachquyen = new List<string>();
            var listquyen = DataProvider.Instance.DB.Cua_NguoiDung_Quyen.Where(t => t.IdNguoiDung == nd.Id);
            foreach(Cua_NguoiDung_Quyen i in listquyen)
            {
                if (i.Quyen.TenQuyen == "VIP")
                    BtnVip = true;
                danhsachquyen.Add(i.Quyen.TenQuyen);
            }
        }


    }
}
