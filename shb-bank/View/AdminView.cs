using System;
using shb_bank.Controller;
using shb_bank.Entity;

namespace shb_bank.View
{
    public class AdminView
    {
        public static void GenerateAdminMenu()
        {
            var generateMenu = new GenerateMenu();
            var accountController = new AccountController();
            var transactionController = new TransactionController();
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
                            transactionController.PrintlistTransactionHistory();
                            break;
                        case 3:
                            accountController.FindUserByUsername();
                            break;
                        case 4:
                            accountController.FindUserByAccountNumber();
                            break;
                        case 5:
                            accountController.FindUserByPhone();
                            break;
                        case 6:
                            accountController.Register();
                            break;
                        case 7:
                            accountController.UpdateAccountStatus();
                            break;
                        case 8:
                            transactionController.PrintListTransaction();
                            break;
                        case 9:
                            accountController.UpdateAccountInfor();
                            break;
                        case 10:
                            accountController.UpdateAccountPassword();
                            break;
                        case 11:
                            Console.WriteLine("Đăng xuất");
                            break;
                        case 12:
                            Console.WriteLine("Goodbye!");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Hãy nhập từ 1 đến 12");
                            break;
                    }
                    
                    if (choice == 11)
                    {
                        AccountController.currentAccount = null;
                        generateMenu.GetMenu(AccountController.currentAccount);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.ReadLine();
            }
        }
    }
}