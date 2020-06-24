using System;

namespace shb_bank.Entity
{
    public class Account
    {
        public string AccountNumber
        {
            get { return _accountNumber; }
            set
            {
                if (value.Length < 8)
                {
                    throw new Exception("Số tài khoản phải có ít nhất 8 kí tự!!");
                }

                _accountNumber = value;
            }
        }

        private string _accountNumber;
        public double Balance { get; set; }

        public string Username
        {
            get { return _username; }
            set
            {
                if (value.Length < 3)
                {
                    throw new Exception("Tên tài khoản phải có ít nhất 3 kí tự!!");
                }

                _username = value;
            }
        }

        private string _username;
        public string PasswordHash { get; set; }
        public string Salt { get; set; }

        public string Email
        {
            get { return _email; }
            set
            {
                if (value.Length == 0)
                {
                    throw new Exception("Email không được để trống!!");
                }

                _email = value;
            }
        }

        private string _email;

        public string Phone
        {
            get { return _phone; }
            set
            {
                if (value.Length != 10)
                {
                    throw new Exception("Số điện thoại phải có 10 kí tự!!");
                }

                _phone = value;
            }
        }

        private string _phone;

        public string Fullname
        {
            get { return _fullName; }
            set
            {
                if (value.Length == 0)
                {
                    throw new Exception("Tên đầy đủ không được để trống!!");
                }

                _fullName = value;
            }
        }

        private string _fullName;
        public AccountRole Role { get; set; }
        public AccountStatus Status { get; set; }

        public override string ToString()
        {
            return $"Account Number: {AccountNumber} |Balance: {Balance} |Username: {_username} |PasswordHash: {PasswordHash} |Salt: {Salt} |Role: {Role} |Fullname: {_fullName} |Phone: {_phone} |Email: {_email} |Status: {Status}";
        }
    }
    public enum AccountRole
    {
        User = 0,
        Admin = 1
        
    }

    public enum AccountStatus
    {
        Active = 1,
        Deactive = -1,
        Lock = 0
    }
}