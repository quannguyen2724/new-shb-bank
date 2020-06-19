using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using shb_bank.Entity;
using shb_bank.Helper;

namespace shb_bank.Model
{
    public class AccountModel
    {
        private Account _account;
        public bool InsertAccount(Account account)
        {
            try
            {
                var cnn = ConnectionHelper.GetConnection();
                cnn.Open();
                MySqlCommand cmd = new MySqlCommand("insert into shbaccount (accountNumber, balance, username, passwordHAsh, salt, role, email , fullname, phone, status) "
                                                    + $"values( '{account.AccountNumber}',{account.Balance},'{account.Username}', '{account.PasswordHash}', '{account.Salt}', {(int) account.Role},'{account.Email}', '{account.Fullname}', '{account.Phone}', {(int) account.Status})",
                    cnn);
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public Account GetAccountByUsername(string username)
        {
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var cmd = new MySqlCommand($"select * from shbaccount where username = '{username}'", cnn);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                _account = new Account()
                {
                    AccountNumber = reader.GetString("accountNumber"),
                    Balance = reader.GetDouble("balance"),
                    Username = reader.GetString("username"),
                    PasswordHash = reader.GetString("passwordHash"),
                    Salt = reader.GetString("salt"),
                    Email = reader.GetString("email"),
                    Phone = reader.GetString("phone"),
                    Fullname = reader.GetString("fullname"),
                    Role = (AccountRole) reader.GetInt32("role"),
                    Status = (AccountStatus) reader.GetInt32("status")
                };
            }
            cnn.Close();
            return _account;
        }

        public List<Account> GetList()
        {
            List<Account> list = new List<Account>();
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var cmd = new MySqlCommand("select * from shbaccount where role = 0",cnn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Account()
                {
                    AccountNumber = reader.GetString("accountNumber"),
                    Balance = reader.GetDouble("balance"),
                    Username = reader.GetString("username"),
                    PasswordHash = reader.GetString("passwordHash"),
                    Salt = reader.GetString("salt"),
                    Email = reader.GetString("email"),
                    Phone = reader.GetString("phone"),
                    Fullname = reader.GetString("fullname"),
                    Role = (AccountRole) reader.GetInt32("role"),
                    Status = (AccountStatus) reader.GetInt32("status")
                });
            }
            cnn.Close();
            return list;
        }
    }
}