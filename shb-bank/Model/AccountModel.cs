using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using shb_bank.Controller;
using shb_bank.Entity;
using shb_bank.Helper;

namespace shb_bank.Model
{
    public class AccountModel
    {
        private Account _account;

        public bool InsertAccount(Account account)
        {
            var cnn = ConnectionHelper.GetConnection();
            try
            {
                cnn.Open();
                MySqlCommand cmd =
                    new MySqlCommand
                    ("insert into shbaccount (accountNumber, balance, username, passwordHAsh, salt, role, email , fullname, phone, status) "
                     + $"values( '{account.AccountNumber}',{account.Balance},'{account.Username}', '{account.PasswordHash}', '{account.Salt}', {(int) account.Role},'{account.Email}', '{account.Fullname}', '{account.Phone}', {(int) account.Status})",
                        cnn);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                cnn.Close();
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

        public List<Account> GetList(string key, string str)
        {
            List<Account> list = new List<Account>();
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            MySqlCommand cmd;
            switch (key)
            {
                case null:
                    cmd = new MySqlCommand($"select * from shbaccount Where Role = 0", cnn);
                    break;
                // case "username":
                //     cmd = new MySqlCommand($"select * from shbaccount where {key} = '{str}'", cnn);
                //     break;
                // case "AccountNumber":
                //     cmd = new MySqlCommand($"select * from shbaccount where {key} = '{str}'", cnn);
                //     break;
                default:
                    cmd = new MySqlCommand($"select * from shbaccount where {key} = '{str}'", cnn);
                    break;
            }
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(_account = new Account()
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
            Console.WriteLine(_account.ToString());
            
            cnn.Close();
            return list;
        }

        public Boolean CheckExistAccount(String accountNumber)
        {
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var cmd = new MySqlCommand($"select * from shbaccount where accountNumber = '{accountNumber}'", cnn);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var account = new Account()
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
                Console.WriteLine("Account Information: ");
                Console.WriteLine(account.ToString());
            }
            else
            {
                Console.WriteLine("Not Found");
                cnn.Close();
                return false;
            }

            cnn.Close();
            return true;
        }

        public void UpdatetAccount(string accountNumber, string str)
        {
            var cnn = ConnectionHelper.GetConnection();
            try
            {
                if (CheckExistAccount(accountNumber))
                {
                    cnn.Open();
                    Account account;
                    switch (str)
                    {
                        case "updateInfor":
                            account = new Account();
                            Console.WriteLine("Enter new Full Name: ");
                            account.Fullname = Console.ReadLine();
                            Console.WriteLine("Enter new Email: ");
                            account.Email = Console.ReadLine();
                            Console.WriteLine("Enter new Phone: ");
                            account.Phone = Console.ReadLine();
                            var cmdInfor =
                                new MySqlCommand
                                ($"update shbaccount set fullName = '{account.Fullname}', email = '{account.Email}', phone = '{account.Phone}' where accountNumber = '{accountNumber}'",
                                    cnn);
                            cmdInfor.ExecuteNonQuery();
                            Console.WriteLine("Update Success");
                            break;

                        case "activeAccount":
                            Console.WriteLine("Enter Status (-1, 0, 1):");
                            var status = int.Parse(Console.ReadLine());
                            var cmdActive =
                                new MySqlCommand
                                ($"update shbaccount set status = '{status}' where accountNumber = '{accountNumber}'",
                                    cnn);
                            cmdActive.ExecuteNonQuery();
                            Console.WriteLine("Update Success");
                            break;

                        case "updatePassword":
                            Console.WriteLine("Enter new password: ");
                            string password = Console.ReadLine();
                            if (password.Length <= 0)
                            {
                                throw new Exception("Password không được để trống");
                            }

                            account = new Account();
                            account.Salt = PasswordHelper.GenerateSalt();
                            account.PasswordHash = PasswordHelper.MD5Hash(password + account.Salt);
                            var cmdPassword =
                                new MySqlCommand
                                ($"update shbaccount set passwordHash = '{account.PasswordHash}', salt = '{account.Salt}' where accountNumber = '{accountNumber}'",
                                    cnn);
                            cmdPassword.ExecuteNonQuery();
                            Console.WriteLine("Update Success");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Update fail");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                cnn.Close();
            }
        }
    }
}