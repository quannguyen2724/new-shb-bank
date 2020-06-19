using System;
using shb_bank.Controller;

namespace shb_bank.View
{
    public class UserView
    {
        public static void GenerateUserMenu()
        {
            var transactionController = new TransactionController();
            var generateMenu = new GenerateMenu();
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("—— Ngân hàng Spring Hero Bank ——");
                    Console.WriteLine(
                        $"Chào mừng {AccountController.currentAccount.Fullname} quay trở lại. Vui lòng chọn thao tác:");
                    Console.WriteLine("1. Gửi tiền.");
                    Console.WriteLine("2. Rút tiền.");
                    Console.WriteLine("3. Chuyển khoản.");
                    Console.WriteLine("4. Truy vấn số dư.");
                    Console.WriteLine("5. Thay đổi thông tin cá nhân.");
                    Console.WriteLine("6. Thay đổi thông tin mật khẩu.");
                    Console.WriteLine("7. Truy vấn lịch sử giao dịch.");
                    Console.WriteLine("8. Đăng xuất");
                    Console.WriteLine("9. Thoát.");
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("Nhập lựa chọn của bạn (1-8): ");
                    var choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            transactionController.Deposit();
                            break;
                        case 2:
                            transactionController.Withdraw();
                            break;
                        case 3:
                            transactionController.Transfer();
                            break;
                        case 4:
                            Console.WriteLine("Truy van so du");
                            break;
                        case 5:
                            Console.WriteLine("Thay doi thong tin");
                            break;
                        case 6:
                            Console.WriteLine("Thay doi mat khau");
                            break;
                        case 7:
                            Console.WriteLine("Truy van lich su giao dich");
                            break;
                        case 8:
                            Console.WriteLine("Dang xuat");
                            break;
                        case 9:
                            Console.WriteLine("Goodbye!!!");
                            return;
                        default:
                            Console.WriteLine("Chọn 1 - 9");
                            break;
                    }


                    if (choice == 8)
                    {
                        AccountController.currentAccount = null;
                        generateMenu.GetMenu();
                    }

                    if (choice == 9)
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    
                }
                Console.ReadLine();

            }
        }
    }
}