using Semester6_MainProject.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Semester6_MainProject.MVVM.ViewModel
{
    class AccountInforViewModel : BaseViewModel
    {
        public NguoiDung nd { get; set; }

        public ObservableCollection<Quyen> _ListQuyen;
        public ObservableCollection<Quyen> ListQuyen { get => _ListQuyen; set { _ListQuyen = value; OnPropertyChanged(); } }


        private string _HoLot;
        public string HoLot { get => _HoLot; set { _HoLot = value; OnPropertyChanged(); } }

        private string _Ten;
        public string Ten { get => _Ten; set { _Ten = value; OnPropertyChanged(); } }

        private string _dmkMatKhauCu;
        public string dmkMatKhauCu { get => _dmkMatKhauCu; set { _dmkMatKhauCu = value; OnPropertyChanged(); } }

        private string _dmkNhapLaiMatKhauCu;
        public string dmkNhapLaiMatKhauCu { get => _dmkNhapLaiMatKhauCu; set { _dmkNhapLaiMatKhauCu = value; OnPropertyChanged(); } }

        private string _dmkMatKhauMoi;
        public string dmkMatKhauMoi { get => _dmkMatKhauMoi; set { _dmkMatKhauMoi = value; OnPropertyChanged(); } }

        #region Command
        public ICommand DoiTenCommand { get; set; }
        public ICommand DoiMatKhauCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand MatKhauChangedCommmand { get; set; }
        public ICommand MatKhauNhapLaiChangedCommmand { get; set; }

        #endregion


        public AccountInforViewModel()
        {
            //get nd
            
            if (MainViewModel.nd == null)
                return;
            nd = MainViewModel.nd;
            HoLot = nd.ThongTin_NguoiDung.Ho_HoLot;
            Ten = nd.ThongTin_NguoiDung.Ten;

            //load list quyen
            ListQuyen = new ObservableCollection<Quyen>(DataProvider.Instance.DB.Quyens);
            var listidquyen = DataProvider.Instance.DB.Cua_NguoiDung_Quyen.Where(x => x.IdNguoiDung == nd.Id).Select(y => y.IdQuyen);
            foreach (Quyen q in ListQuyen)
            {
                foreach (int i in listidquyen)
                {
                    if (q.Id == i)
                    {
                        q.Check = true;
                    }
                }
            }

            DoiTenCommand = new RelayCommand<Object>((p) =>
            {
                if (String.IsNullOrEmpty(HoLot) || String.IsNullOrEmpty(Ten))
                    return false;
                return true;
            }, set =>
            {
                var hoten = DataProvider.Instance.DB.ThongTin_NguoiDung.Where(x => x.Id == nd.IdThongTin).SingleOrDefault();
                hoten.Ho_HoLot = HoLot;
                hoten.Ten = Ten;
                DataProvider.Instance.DB.SaveChanges();
                MessageBox.Show("Đổi tên thành công!");
            });

            DoiMatKhauCommand = new RelayCommand<Object>((p) =>
            {
                if (String.IsNullOrEmpty(dmkMatKhauCu) || String.IsNullOrEmpty(dmkNhapLaiMatKhauCu) || String.IsNullOrEmpty(dmkMatKhauMoi))
                    return false;
                return true;
            }, set =>
            {
                if (dmkMatKhauCu != dmkNhapLaiMatKhauCu)
                {
                    MessageBox.Show("Mật khẩu nhập lại không khớp! Vui lòng kiểm tra lại.");
                    return;
                }

                string mkmh = MatKhauMD5(Base64Encode(dmkMatKhauCu));
                NguoiDung nguoidung = DataProvider.Instance.DB.NguoiDungs.Where(x => x.TenDangNhap==nd.TenDangNhap && x.MatKhau== mkmh).SingleOrDefault();
                
                if (nguoidung == null)
                {
                    MessageBox.Show("Sai mật khẩu!", "Thông báo");
                    return;
                }

                string matkhauMH = MatKhauMD5(Base64Encode(dmkMatKhauMoi));
                nguoidung.MatKhau = matkhauMH;
                DataProvider.Instance.DB.SaveChanges();
                MessageBox.Show("Đổi mật khẩu thành công!");
            });

            //doi mat khau
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { dmkMatKhauMoi = p.Password; });//mk moi

            MatKhauChangedCommmand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { dmkMatKhauCu = p.Password; }); //mat khau cu
            MatKhauNhapLaiChangedCommmand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>                           //nhap lai mk cu
            {
                dmkNhapLaiMatKhauCu = p.Password;
                if (dmkMatKhauCu != dmkNhapLaiMatKhauCu)
                {
                    p.BorderBrush = Brushes.Red;
                    p.BorderThickness = new Thickness(1, 1, 1, 1);
                }
                else
                {
                    p.BorderThickness = new Thickness(0);
                }
            });
        }

        //md5 hash
        private string MatKhauMD5(string s)
        {
            MD5 md5Hash = MD5.Create();
            string matkhauMH = getMD5Hash(md5Hash, s);
            return matkhauMH;
        }
        private static string getMD5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        //base64 hash
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }     
}