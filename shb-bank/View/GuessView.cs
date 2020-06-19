using System;
using shb_bank.Controller;

namespace shb_bank.View
{
    public class GuessView
    {
        public static void GenerateGuessMenu()
        {
            var generateMenu = new GenerateMenu();
            var accountController = new AccountController();
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("—— Ngân hàng Spring Hero Bank ——");
                    Console.WriteLine("1. Đăng ký tài khoản.");
                    Console.WriteLine("2. Đăng nhập hệ thống.");
                    Console.WriteLine("3. Thoát.");
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("Nhập lựa chọn của bạn (1, 2, 3): ");
                    var choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            accountController.Register();
                            break;
                        case 2:
                            accountController.Login();
                            break;
                        case 3:
                            Console.WriteLine("Thoat");
                            break;
                        default:
                            Console.WriteLine("Chọn 1, 2 hoặc 3");
                            break;
                    }

                    Console.ReadLine();
                    if (choice == 3)
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Bạn phai nhập vào số");
                }
            }
        }
    }
}