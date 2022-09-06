using Semester6_MainProject.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Semester6_MainProject.MVVM.ViewModel
{
    class HighRatingViewModel : BaseViewModel
    {
        bool isDesc = true;

        string duongdangoc = "D:\\data\\";
        #region List
        public ObservableCollection<Phim> _ListPhim;
        public ObservableCollection<Phim> ListPhim { get => _ListPhim; set { _ListPhim = value; OnPropertyChanged(); LoadAnh(); } }

        public ObservableCollection<ChuDe> _ListChuDe;
        public ObservableCollection<ChuDe> ListChuDe { get => _ListChuDe; set { _ListChuDe = value; OnPropertyChanged(); } }

        private ObservableCollection<TrangThai> _ListTrangThai;
        public ObservableCollection<TrangThai> ListTrangThai { get => _ListTrangThai; set { _ListTrangThai = value; OnPropertyChanged(); } }

        private ObservableCollection<Mua> _ListMua;
        public ObservableCollection<Mua> ListMua { get => _ListMua; set { _ListMua = value; OnPropertyChanged(); } }

        private ObservableCollection<int?> _ListNam;
        public ObservableCollection<int?> ListNam { get => _ListNam; set { _ListNam = value; OnPropertyChanged(); } }

        public ObservableCollection<TheLoai> _ListTheLoai;
        public ObservableCollection<TheLoai> ListTheLoai { get => _ListTheLoai; set { _ListTheLoai = value; OnPropertyChanged(); } }
        
        public ObservableCollection<NguoiDung_BinhLuan_Phim> _ListCmt;
        public ObservableCollection<NguoiDung_BinhLuan_Phim> ListCmt { get => _ListCmt; set { _ListCmt = value; OnPropertyChanged(); } }

        

        #endregion

        #region Properties
        private string _IconKind;
        public string IconKind { get => _IconKind; set { _IconKind = value; OnPropertyChanged(); } }
        private bool _canWatch;
        public bool canWatch { get => _canWatch; set { _canWatch = value; OnPropertyChanged(); } }

        private bool _canComment;
        public bool canComment { get => _canComment; set { _canComment = value; OnPropertyChanged(); } }

        private int _LuotXem;
        public int LuotXem { get=>_LuotXem; set { _LuotXem = value; OnPropertyChanged(); } }

        private int _LuotBinhLuan;
        public int LuotBinhLuan { get => _LuotBinhLuan; set { _LuotBinhLuan = value; OnPropertyChanged(); } }

        private double _DiemSo;
        public double DiemSo { get => _DiemSo; set { _DiemSo = value; OnPropertyChanged(); } }
            
        private int _LuotDanhGia;
        public int LuotDanhGia { get => _LuotDanhGia; set { _LuotDanhGia = value; OnPropertyChanged(); } }
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

        public List<int?> _SelectedListTheLoai;
        public List<int?> SelectedListTheLoai { get => _SelectedListTheLoai; set { _SelectedListTheLoai = value; OnPropertyChanged(); } }

        private TrangThai _SelectedTrangThai;
        public TrangThai SelectedTrangThai { get => _SelectedTrangThai; set { _SelectedTrangThai = value; OnPropertyChanged(); } }

        private ChuDe _SelectedChuDe;
        public ChuDe SelectedChuDe { get => _SelectedChuDe; set { _SelectedChuDe = value; OnPropertyChanged(); } }

        private Mua _SelectedMua;
        public Mua SelectedMua { get => _SelectedMua; set { _SelectedMua = value; OnPropertyChanged(); } }

        private int? _SelectedNam;
        public int? SelectedNam { get => _SelectedNam; set { _SelectedNam = value; OnPropertyChanged(); } }
        #endregion

        #region Command
        public ICommand SortCommand { get; set; }
        public ICommand FilterCommand { get; set; }
        public ICommand UpdateValues { get; set; }
        #endregion


        public HighRatingViewModel()
        {
            IconKind = "ArrowDownThin";
            //ListPhim = new ObservableCollection<Phim>(DataProvider.Instance.DB.Phims.Where(p => p.AnPhim == 0).OrderByDescending(p => p.LuotXem.LX_Tong));

            ListTrangThai = new ObservableCollection<TrangThai>(DataProvider.Instance.DB.TrangThais);
            ListChuDe = new ObservableCollection<ChuDe>(DataProvider.Instance.DB.ChuDes);
            ListMua = new ObservableCollection<Mua>(DataProvider.Instance.DB.Muas);
            ListNam = new ObservableCollection<int?>(DataProvider.Instance.DB.Phims.Select(p => p.NamPhatHanh).Distinct());
            ListTheLoai = new ObservableCollection<TheLoai>(DataProvider.Instance.DB.TheLoais);

            SortCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                if (isDesc)
                    IconKind = "ArrowUpThin";
                else
                    IconKind = "ArrowDownThin";

                isDesc = !isDesc;
            });
            FilterCommand = new RelayCommand<ComboBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                //loc chu de truoc
                if (SelectedChuDe == null || SelectedChuDe.TenChuDe == "Tất cả")
                {
                    ListPhim = new ObservableCollection<Phim>(DataProvider.Instance.DB.Phims);
                }
                else
                {
                    ListPhim = new ObservableCollection<Phim>(DataProvider.Instance.DB.Phims.Where(x => x.ChuDe.TenChuDe == SelectedChuDe.TenChuDe));
                }

                //loc trang thai
                if (SelectedTrangThai != null && SelectedTrangThai.TenTrangThai != "Tất cả")
                {
                    ListPhim = new ObservableCollection<Phim>(ListPhim.Where(n => n.TrangThai.TenTrangThai == SelectedTrangThai.TenTrangThai));
                }

                //loc mua`
                if (SelectedMua != null && SelectedMua.TenMua != "Tất cả")
                {
                    ListPhim = new ObservableCollection<Phim>(ListPhim.Where(n => n.Mua.TenMua == SelectedMua.TenMua));
                }

                //loc nam
                if (SelectedNam != null && SelectedNam != 0)
                {
                    ListPhim = new ObservableCollection<Phim>(ListPhim.Where(n => n.NamPhatHanh == SelectedNam));
                }

                //loc the loai
                GetListTheLoai();
                if (SelectedListTheLoai.Count != 0)
                {
                    LocTheLoai();
                }


                if (isDesc)
                {
                    ListPhim = new ObservableCollection<Phim>(ListPhim.OrderByDescending(x => x.NguoiDung_DanhGia_Phim.Sum(y=>y.DiemSo)/x.NguoiDung_DanhGia_Phim.Count));
                }
                else
                {
                    ListPhim = new ObservableCollection<Phim>(ListPhim.OrderBy(x => x.NguoiDung_DanhGia_Phim.Sum(y => y.DiemSo) / x.NguoiDung_DanhGia_Phim.Count));
                }
            });

            UpdateValues = new RelayCommand<object>((p) => true, (p) =>
            {
                ListCmt = new ObservableCollection<NguoiDung_BinhLuan_Phim>(SelectedItem.NguoiDung_BinhLuan_Phim);
            });
        }

        #region Method

        bool checkNullTheLoai()
        {
            foreach (TheLoai t in ListTheLoai)
            {
                if (t.Check == true)
                    return true;
            }
            return false;
        }


        void LoadAnh()
        {
            foreach (Phim p in ListPhim)
            {
                p.Anh = new Uri(duongdangoc + p.AnhDaiDiens.Single().tenFileAnh);     
                

            }
        }

        void GetListTheLoai()
        {
            SelectedListTheLoai = new List<int?>();
            foreach (TheLoai t in ListTheLoai)
            {
                if (t.Check == true)
                {
                    SelectedListTheLoai.Add(t.Id);
                }
            }
        }

        void LocTheLoai()
        {
            foreach (Phim p in ListPhim.ToList())
            {
                List<int?> ListId = DataProvider.Instance.DB.Cua_Phim_TheLoai.Where(c => c.IdPhim == p.Id).Select(t => t.IdTheLoai).ToList();

                if (SelectedListTheLoai.Except(ListId).Any())
                {
                    ListPhim.Remove(p);
                }
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