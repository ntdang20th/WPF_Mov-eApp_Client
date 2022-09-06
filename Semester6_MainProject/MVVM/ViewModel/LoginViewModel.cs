using Semester6_MainProject.MVVM.Model;
using System;
using System.Collections.Generic;
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
    class LoginViewModel : BaseViewModel
    {
        public bool IsLogin { get; set; }
        public int idnguoidung { get; set; }


        private string _username, _passowrd;
        public string Username { get => _username; set { _username = value; OnPropertyChanged(); } }
        public string Passowrd { get => _passowrd; set { _passowrd = value; OnPropertyChanged(); } }

        private string _TenDangNhap;
        public string TenDangNhap { get => _TenDangNhap; set { _TenDangNhap = value; OnPropertyChanged(); } }

        private string _MatKhau;
        public string MatKhau { get => _MatKhau; set { _MatKhau = value; OnPropertyChanged(); } }

        private string _NhapLaiMatKhau;
        public string NhapLaiMatKhau { get => _NhapLaiMatKhau; set { _NhapLaiMatKhau = value; OnPropertyChanged(); } }

        private string _HoLot;
        public string HoLot { get => _HoLot; set { _HoLot = value; OnPropertyChanged(); } }

        private string _Ten;
        public string Ten { get => _Ten; set { _Ten = value; OnPropertyChanged(); } }

        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }

        private DateTime _NgaySinh;
        public DateTime NgaySinh { get => _NgaySinh; set { _NgaySinh = value; OnPropertyChanged(); } }



        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand MatKhauChangedCommmand { get; set; }
        public ICommand MatKhauNhapLaiChangedCommmand { get; set; }
        public ICommand RegisterCommand { get; set; }


        public LoginViewModel()
        {
            IsLogin = false;
            NgaySinh = DateTime.Parse("01/01/2000");

            //form login
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });

            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Passowrd = p.Password; });


            //register
            MatKhauChangedCommmand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { MatKhau = p.Password; });
            MatKhauNhapLaiChangedCommmand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => 
            { 
                NhapLaiMatKhau = p.Password; 
                if(MatKhau != NhapLaiMatKhau)
                {
                    p.BorderBrush = Brushes.Red;
                    p.BorderThickness = new Thickness(1,1,1,1);
                }
                else
                {
                    p.BorderThickness = new Thickness(0);
                }
            });


            RegisterCommand = new RelayCommand<Window>((p) => 
            {
                if (String.IsNullOrEmpty(TenDangNhap) || String.IsNullOrEmpty(MatKhau) || String.IsNullOrEmpty(NhapLaiMatKhau) || String.IsNullOrEmpty(Ten) || String.IsNullOrEmpty(HoLot) || NgaySinh == null || String.IsNullOrEmpty(Email))
                    return false;
                return true;
            }, (p) =>
            {
                if(MatKhau != NhapLaiMatKhau)
                {
                    MessageBox.Show("Mật khẩu nhập lại không khớp! Vui lòng kiểm tra lại.");
                }


                //thong tin tai khoan 
                var thongtin = new ThongTin_NguoiDung() { Ho_HoLot = HoLot, Ten = Ten, NgaySinh = NgaySinh, Email = Email};
                DataProvider.Instance.DB.ThongTin_NguoiDung.Add(thongtin);
                DataProvider.Instance.DB.SaveChanges();

                //them tai khoan
                string matkhauMH = MatKhauMD5(Base64Encode(MatKhau));

                int idmax = DataProvider.Instance.DB.ThongTin_NguoiDung.Max(x => x.Id);
                var taikhoan = new NguoiDung() { TenDangNhap = TenDangNhap, MatKhau = matkhauMH, IdThongTin = idmax};
                DataProvider.Instance.DB.NguoiDungs.Add(taikhoan);
                DataProvider.Instance.DB.SaveChanges();

                //quyen co ban: Check 18+
                // Save today's date.
                var today = DateTime.Today;

                // Calculate the age.
                var age = today.Year - NgaySinh.Year;

                // Go back to the year in which the person was born in case of a leap year
                if (NgaySinh.Date > today.AddYears(-age)) 
                    age--;

                if(age < 18)
                {
                    //theem quyeenf
                    var quyen = new Cua_NguoiDung_Quyen() { IdNguoiDung = DataProvider.Instance.DB.NguoiDungs.Max(x => x.Id), IdQuyen = 5 };
                    DataProvider.Instance.DB.Cua_NguoiDung_Quyen.Add(quyen);
                    DataProvider.Instance.DB.SaveChanges();
                }

                MessageBox.Show("Tạo thành công!");

                MainWindow newWindow = new MainWindow();
                Application.Current.MainWindow = newWindow;
                p.Close();
                newWindow.Show();
            });
        }



        void Login(Window p)
        {
            if (p == null)
                return;

            string matkhau = MatKhauMD5(Base64Encode(Passowrd));
            var res = DataProvider.Instance.DB.NguoiDungs.Where(t => t.TenDangNhap == Username && t.MatKhau == matkhau).Count();

            if (res > 0)
            {
                IsLogin = true;
                idnguoidung  = DataProvider.Instance.DB.NguoiDungs.Where(t => t.TenDangNhap == Username && t.MatKhau == matkhau).Select(t=>t.Id).SingleOrDefault();
                p.Close();
            }
            else
            {
                IsLogin = false;
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!");
            }

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
