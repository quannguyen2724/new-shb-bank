using System;
using shb_bank.Controller;

namespace shb_bank.View
{
    public class AdminView
    {
        public static void GenerateAdminMenu()
        {
            var generateMenu = new GenerateMenu();
            AccountController accountController = new AccountController();
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("—— Ngân hàng Spring Hero Bank ——");
                    Console.WriteLine(
                        $"Chào mừng Admin {AccountController.currentAccount.Fullname} quay trở lại. Vui lòng chọn thao tác:");
                    Console.WriteLine("1. Danh sách người dùng.");
                    Console.WriteLine("2. Danh sách lịch sử giao dịch.");
                    Console.WriteLine("3. Tìm kiếm người dùng theo tên.");
                    Console.WriteLine("4. Tìm kiếm người dùng theo số tài khoản.");
                    Console.WriteLine("5. Tìm kiếm người dùng theo số điện thoại.");
                    Console.WriteLine("6. Thêm người dùng mới.");
                    Console.WriteLine("7. Khoá và mở tài khoản người dùng.");
                    Console.WriteLine("8. Tìm kiếm lịch sử giao dịch theo số tài khoản.");
                    Console.WriteLine("9. Thay đổi thông tin tài khoản.");
                    Console.WriteLine("10. Thay đổi thông tin mật khẩu.");
                    Console.WriteLine("11. Đăng xuất.");
                    Console.WriteLine("12. Thoát.");
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("Nhập lựa chọn của bạn (1-12): ");
                    var choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            accountController.ListUser();
                            break;
                        case 2:
                            Console.WriteLine("Danh sách lịch sử giao dịch");
                            break;
                        case 3:
                            Console.WriteLine("Tìm kiếm người dùng theo tên");
                            break;
                        case 4:
                            Console.WriteLine("Tim kiem nguoi dung theo stk");
                            break;
                        case 5:
                            Console.WriteLine("Tìm kiếm người dùng theo số điện thoại");
                            break;
                        case 6:
                            Console.WriteLine("Them nguoi dung moi");
                            break;
                        case 7:
                            Console.WriteLine("Khoa va mo tai khoan nguoi dung");
                            break;
                        case 8:
                            Console.WriteLine("Tim kiem lich su giao dich theo stk");
                            break;
                        case 9:
                            Console.WriteLine("Thay doi thong tin tai khoan");
                            break;
                        case 10:
                            Console.WriteLine("Thay doi thong tin mat khau");
                            break;
                        case 11:
                            Console.WriteLine("Dang xuat");
                           
                            break;
                        case 12:
                            Console.WriteLine("Thoat");
                            break;
                        default:
                            Console.WriteLine("Hãy nhập từ 1 đến 12");
                            break;
                    }
                    
                    if (choice == 12)
                    {
                        break;
                    }

                    if (choice == 11)
                    {
                        AccountController.currentAccount = null;
                        generateMenu.GetMenu();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Bạn phải nhập vào 1 số");
                }
                Console.ReadLine();
            }
        }
    }
}