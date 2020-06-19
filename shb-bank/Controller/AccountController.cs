using System;
using shb_bank.Entity;
using shb_bank.Helper;
using shb_bank.Model;
using shb_bank.View;

namespace shb_bank.Controller
{
    public class AccountController
    {
        private AccountModel _accountModel = new AccountModel();
        private PasswordHelper _passwordHelper = new PasswordHelper();
        public static Account currentAccount;

        public void Register()
        {
            var account = new Account();
            Console.WriteLine("Create a new account!");
            Console.WriteLine("Enter Account Number:");
            account.AccountNumber = Console.ReadLine();
            Console.WriteLine("Enter Username:");
            account.Username = Console.ReadLine();
            Console.WriteLine("Enter Password:");
            var password = Console.ReadLine();
            account.Salt = PasswordHelper.GenerateSalt();
            account.PasswordHash = PasswordHelper.MD5Hash(password + account.Salt);
            Console.WriteLine("Enter Email:");
            account.Email = Console.ReadLine();
            Console.WriteLine("Enter Fullname:");
            account.Fullname = Console.ReadLine();
            Console.WriteLine("Enter Phone:");
            account.Phone = Console.ReadLine();
            account.Balance = 0;
            account.Role = AccountRole.User;
            account.Status = AccountStatus.Active;
            var result = _accountModel.InsertAccount(account);
            if (result)
            {
                Console.WriteLine("Đăng ký thành công!!");
            }
            else
            {
                Console.WriteLine("Đăn ký thất bại, số tài khoản, số điện thoại hoặc email đã có người sử dụng!!");
            }
        }
        
        public Account Login()
        {
            GenerateMenu generateMenu = new GenerateMenu();
            try 
            {
                Console.WriteLine("Enter Your Username: ");
                var username = Console.ReadLine();
                Console.WriteLine("Enter Your Password: ");
                var password = Console.ReadLine();
                var account = _accountModel.GetAccountByUsername(username);
                if (account !=null
                    && _passwordHelper.ComparePassword(password, account.Salt, account.PasswordHash))
                {
                    Console.WriteLine("Đăng nhập thành công");
                    currentAccount = account;
                    generateMenu.GetMenu();
                }
                Console.WriteLine("Đăng nhập thất bại");
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public void ListUser()
        {
            foreach (var account in _accountModel.GetList())
            {
                Console.WriteLine(account.ToString());
            }
        }
        
        
    }
}