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
                Console.WriteLine("Đăng ký thất bại, số tài khoản, số điện thoại hoặc email đã có người sử dụng!!");
            }
        }

        public Account Login()
        {
            var generateMenu = new GenerateMenu();
            Console.WriteLine("Enter Your Username: ");
            var username = Console.ReadLine();
            if (username.Length <= 0)
            {
                throw new Exception("Username không được để trống");
            }

            Console.WriteLine("Enter Your Password: ");
            var password = Console.ReadLine();
            if (password.Length <= 0)
            {
                throw new Exception("Password không được để trống");
            }

            var account = _accountModel.GetAccountByUsername(username);
            if (account != null
                && _passwordHelper.ComparePassword(password, account.Salt, account.PasswordHash))
            {
                Console.WriteLine("Đăng nhập thành công");
                currentAccount = account;
                generateMenu.GetMenu(currentAccount);
                return currentAccount;
            }

            Console.WriteLine("Đăng nhập thất bại");
            return null;
        }

        public void ListUser()
        {
            var listUser = _accountModel.GetList(null, null);
            if (listUser.Count > 0)
            {
                foreach (var account in listUser)
                {
                    Console.WriteLine(account.ToString());
                }
            }
            else
            {
                Console.WriteLine("Không có tài khoản nào");
            }
        }

        public void BalanceQty()
        {
            Console.WriteLine($"Số dư trong tài khoản của bạn là: {currentAccount.Balance}");
        }

        public void UpdateAccountInfor()
        {
            if (currentAccount.Role == AccountRole.User)
            {
                _accountModel.UpdatetAccount(currentAccount.AccountNumber, "updateInfor");
            }
            else
            {
                var userAccount = new Account();
                Console.WriteLine("Nhập số tài khoản muốn thay đổi thông tin: ");
                userAccount.AccountNumber = Console.ReadLine();
                _accountModel.UpdatetAccount(userAccount.AccountNumber, "updateInfor");
            }
        }

        public void UpdateAccountPassword()
        {
            if (currentAccount.Role == AccountRole.User)
            {
                _accountModel.UpdatetAccount(currentAccount.AccountNumber, "updatePassword");
            }
            else
            {
                var userAccount = new Account();
                Console.WriteLine("Nhập số tài khoản muốn thay đổi mật khẩu: ");
                userAccount.AccountNumber = Console.ReadLine();
                _accountModel.UpdatetAccount(userAccount.AccountNumber, "updatePassword");
            }
        }

        public void UpdateAccountStatus()
        {
            var userAccount = new Account();
            Console.WriteLine("Nhập số tài khoản muốn thay đổi trạng thái: ");
            userAccount.AccountNumber = Console.ReadLine();
            _accountModel.UpdatetAccount(userAccount.AccountNumber, "activeAccount");
        }

        public void FindUserByUsername()
        {
            var acc = new Account();
            Console.WriteLine("Nhập tên người dùng: ");
            acc.Username = Console.ReadLine();
            _accountModel.GetList("username", acc.Username);
        }
        
        public void FindUserByAccountNumber()
        {
            var acc = new Account();
            Console.WriteLine("Nhập số tài khoản: ");
            acc.AccountNumber = Console.ReadLine();
            _accountModel.GetList("accountNumber", acc.AccountNumber);
        }
        
        public void FindUserByPhone()
        {
            var acc = new Account();
            Console.WriteLine("Nhập số điện thoại: ");
            acc.Phone = Console.ReadLine();
            _accountModel.GetList("phone",  acc.Phone);
        }
    }
}