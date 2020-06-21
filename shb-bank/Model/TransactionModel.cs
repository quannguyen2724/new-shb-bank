using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using shb_bank.Entity;
using shb_bank.Helper;

namespace shb_bank.Model
{
    public class TransactionModel
    {
        public bool Deposit(string accountNumber, double amount)
        {
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            
            //tạo transaction
            var transaction = cnn.BeginTransaction();
            try
            {
                //kiểm tra tài khoản
                var strGetAccount =
                    $"select balance from shbaccount where accountNumber = {accountNumber} and status = {(int) AccountStatus.Active}";
                var cmdGetAccount = new MySqlCommand(strGetAccount, cnn);
                var accountReader = cmdGetAccount.ExecuteReader();
                if (!accountReader.Read())
                {
                    throw new Exception("Account is not found or has been deleted!");
                }

                // lấy ra số dư hiện tại
                var currentBalance = accountReader.GetDouble("balance");
                accountReader.Close();

                //Update số dư tài khoản
                currentBalance += amount;
                var strUpdateAccount =
                    $"update shbaccount set balance = {currentBalance} where accountNumber = {accountNumber} and status = {(int) AccountStatus.Active}";
                var cmdUpdateAccount = new MySqlCommand(strUpdateAccount, cnn);
                cmdUpdateAccount.ExecuteNonQuery();

                //Lưu transaction history
                var shbTransaction = new Transaction()
                {
                    TransactionCode = Guid.NewGuid().ToString(),
                    SenderAccountNumber = accountNumber,
                    ReceiverAccountNumber = accountNumber,
                    Type = TransactionType.Deposit,
                    Amount = amount,
                    Fee = 0,
                    Message = "Deposit " + amount,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Status = TransactionStatus.Done
                };
                var strInsertTransaction =
                    $"INSERT INTO `shbtransaction`(`transactionCode`, `senderAccountNumber`, `receiverAccountNumber`, `type`, `amount`, `fee`, `message`, `createdAt`, `updateAt`, `status`) " +
                    $"VALUES ('{shbTransaction.TransactionCode}', '{shbTransaction.SenderAccountNumber}', '{shbTransaction.ReceiverAccountNumber}', {(int) shbTransaction.Type}, {shbTransaction.Amount}, {shbTransaction.Fee}, '{shbTransaction.Message}', '{shbTransaction.CreatedAt:yy-MM-dd hh:mm:ss}', '{shbTransaction.UpdatedAt:yy-MM-dd hh:mm:ss}', {(int) shbTransaction.Status})";
                var cmdInsertTransaction = new MySqlCommand(strInsertTransaction, cnn);
                cmdInsertTransaction.ExecuteNonQuery();

                transaction.Commit();
                Console.WriteLine("Deposit success!");
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                transaction.Rollback();
            }
            finally
            {
                cnn.Close();
            }
            return false;
        }
        
        public bool Withdraw(string accountNumber, double amount)
        {
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            
            //tạo transaction
            var transaction = cnn.BeginTransaction();
            try
            {
                if (amount <= 0)
                {
                    throw new Exception("Invalid amount!");
                }
                
                //kiểm tra tài khoản
                var strGetAccount = $"select balance from shbaccount where accountNumber = '{accountNumber}' and status = {(int) AccountStatus.Active}";
                var cmdGetAccount = new MySqlCommand(strGetAccount, cnn);
                var accountReader = cmdGetAccount.ExecuteReader();
                if (!accountReader.Read())
                {
                    throw new Exception("Account is not found or has been deleted!");
                }
                // lấy ra số dư hiện tại
                var currentBalance = accountReader.GetDouble("balance");
                accountReader.Close();
                
                //update số dư tài khoản sau khi rút.
                currentBalance -= amount;
                if (currentBalance <= 0)
                {
                    throw new Exception("Invalid amount");
                }
                var strUpdateAccount =
                    $"update shbaccount set balance = {currentBalance} where accountNumber = {accountNumber} and status = {(int) AccountStatus.Active}";
                var cmdUpdateAccount = new MySqlCommand(strUpdateAccount, cnn);
                cmdUpdateAccount.ExecuteNonQuery();
                
                //lưu transaction history.
                var shbTransaction = new Transaction()
                {
                    TransactionCode = Guid.NewGuid().ToString(),
                    SenderAccountNumber = accountNumber,
                    ReceiverAccountNumber = accountNumber,
                    Type = TransactionType.Withdraw,
                    Amount = amount,
                    Fee = 0,
                    Message = "Withdraw " + amount,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Status = TransactionStatus.Done
                };
                var strInsertTransaction =
                    $"INSERT INTO `shbtransaction`(`transactionCode`, `senderAccountNumber`, `receiverAccountNumber`, `type`, `amount`, `fee`, `message`, `createdAt`, `updateAt`, `status`) " +
                    $"VALUES ('{shbTransaction.TransactionCode}', '{shbTransaction.SenderAccountNumber}', '{shbTransaction.ReceiverAccountNumber}', {(int) shbTransaction.Type}, {shbTransaction.Amount}, {shbTransaction.Fee}, '{shbTransaction.Message}', '{shbTransaction.CreatedAt:yy-MM-dd hh:mm:ss}', '{shbTransaction.UpdatedAt:yy-MM-dd hh:mm:ss}', {(int) shbTransaction.Status})";
                var cmdInsertTransaction = new MySqlCommand(strInsertTransaction, cnn);
                cmdInsertTransaction.ExecuteNonQuery();
                
                transaction.Commit();
                Console.WriteLine("Withdraw success!");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                transaction.Rollback();
            }
            finally
            {
                cnn.Close();
            }
            return false;
        }
        
        public bool Transfer(string senderAccountNumber, string receiverAccountNumber, double amount)
        {
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            
            //Tạo transaction.
            var transaction = cnn.BeginTransaction();
            try
            {
                Console.WriteLine("Nhập vào lời nhắn: ");
                var message = Console.ReadLine();
                if (amount <= 0)
                {
                    throw new Exception("Invalid amount!");
                }
                
                //Kiểm tra tài khoản chuyển tiền.
                var strGetSenderAccount = $"select balance from shbaccount where accountNumber = '{senderAccountNumber}' and status = {(int) AccountStatus.Active}";
                var cmdGetSenderAccount = new MySqlCommand(strGetSenderAccount, cnn);
                var senderAccountReader = cmdGetSenderAccount.ExecuteReader();
                if (!senderAccountReader.Read())
                {
                    throw new Exception("Account is not found or has been deleted!");
                }
                // lấy ra số dư hiện tại của tài khoản chuyển tiền.
                var currentSenderBalance = senderAccountReader.GetDouble("balance");
                senderAccountReader.Close();
                
                //Kiểm tra tài khoản nhận tiền.
                var strGetReceiverAccount = $"select balance from shbaccount where accountNumber = '{receiverAccountNumber}' and status = {(int) AccountStatus.Active}";
                var cmdGetReceiverAccount = new MySqlCommand(strGetReceiverAccount, cnn);
                var receiverAccountReader = cmdGetReceiverAccount.ExecuteReader();
                if (!receiverAccountReader.Read())
                { 
                  throw  new Exception("Account is not found or has been deleted!");
                }
                // lấy ra số dư hiện tại của tài khoản nhận tiền.
                var currentReceiverBalance = receiverAccountReader.GetDouble("Balance");
                receiverAccountReader.Close();
                
                //Update số dư tài khoản chuyển và tài khoản nhận sau khi chuyển tiền.
                currentSenderBalance -= amount;
                if ( currentSenderBalance <= 0)
                {
                    throw new Exception("Invalid amount");
                }
                var strUpdateSenderAccount =
                    $"update shbaccount set balance = {currentSenderBalance} where accountNumber = {senderAccountNumber} and status = {(int) AccountStatus.Active}";
                var cmdUpdateSenderAccount = new MySqlCommand(strUpdateSenderAccount, cnn);
                cmdUpdateSenderAccount.ExecuteNonQuery();

                currentReceiverBalance += amount;
                var strUpdateReceiverAccount = 
                    $"update shbaccount set balance = {currentReceiverBalance} where accountNumber = {receiverAccountNumber} and status = {(int) AccountStatus.Active}";
                var cmdUpdateReceiverAccount = new MySqlCommand(strUpdateReceiverAccount, cnn);
                cmdUpdateReceiverAccount.ExecuteNonQuery();
                
                //Lưu transaction history.
                var shbTransaction = new Transaction()
                {
                    TransactionCode = Guid.NewGuid().ToString(),
                    SenderAccountNumber = senderAccountNumber,
                    ReceiverAccountNumber = receiverAccountNumber,
                    Type = TransactionType.Tranfer,
                    Amount = amount,
                    Fee = 0,
                    Message = message,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Status = TransactionStatus.Done
                };
                var strInsertTransaction =
                    $"INSERT INTO `shbtransaction`(`transactionCode`, `senderAccountNumber`, `receiverAccountNumber`, `type`, `amount`, `fee`, `message`, `createdAt`, `updateAt`, `status`) " +
                    $"VALUES ('{shbTransaction.TransactionCode}', '{shbTransaction.SenderAccountNumber}', '{shbTransaction.ReceiverAccountNumber}', {(int) shbTransaction.Type}, {shbTransaction.Amount}, {shbTransaction.Fee}, '{shbTransaction.Message}', '{shbTransaction.CreatedAt:yy-MM-dd hh:mm:ss}', '{shbTransaction.UpdatedAt:yy-MM-dd hh:mm:ss}', {(int) shbTransaction.Status})";
                var cmdInsertTransaction = new MySqlCommand(strInsertTransaction, cnn);
                cmdInsertTransaction.ExecuteNonQuery();
                
                transaction.Commit();
                Console.WriteLine("Transfer success!");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                transaction.Rollback();
            }
            finally
            {
                cnn.Close();
            }
            return false;
        }

        public List<Transaction> GetTransactionHistory(string accountNumber)
        {
            var listTransaction = new List<Transaction>();
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            var strGetListTransaction = $"select * from shbtransaction where senderAccountNumber = '{accountNumber}' or receiverAccountNumber = '{accountNumber}'";
            var cmdGetListTransaction = new MySqlCommand(strGetListTransaction, cnn);
            var reader = cmdGetListTransaction.ExecuteReader();
            while (reader.Read())
            {
                listTransaction.Add(new Transaction()
                {
                    TransactionCode = reader.GetString("transactionCode"),
                    SenderAccountNumber = reader.GetString("senderAccountNumber"),
                    ReceiverAccountNumber = reader.GetString("receiverAccountNumber"),
                    Type = (TransactionType) reader.GetInt32("type"),
                    Amount = reader.GetDouble("amount"),
                    Fee = reader.GetDouble("fee"),
                    Message = reader.GetString("message"),
                    CreatedAt = reader.GetDateTime("createdAt"),
                    UpdatedAt = reader.GetDateTime("updateAt"),
                    Status = (TransactionStatus) reader.GetInt32("status")
                });
            }
            cnn.Close();
            return listTransaction;
        }

        public List<Transaction> GetAllTransactionHistory()
        {
            var listTransaction = new List<Transaction>();
            var cnn = ConnectionHelper.GetConnection();
            cnn.Open();
            try
            {
                var strGetListTransaction = "select * from shbtransaction";
                var cmdGetListTransaction = new MySqlCommand(strGetListTransaction, cnn);
                var reader = cmdGetListTransaction.ExecuteReader();
                while (reader.Read())
                {
                    listTransaction.Add(new Transaction()
                    {
                        TransactionCode = reader.GetString("transactionCode"),
                        SenderAccountNumber = reader.GetString("senderAccountNumber"),
                        ReceiverAccountNumber = reader.GetString("receiverAccountNumber"),
                        Type = (TransactionType) reader.GetInt32("type"),
                        Amount = reader.GetDouble("amount"),
                        Fee = reader.GetDouble("fee"),
                        Message = reader.GetString("message"),
                        CreatedAt = reader.GetDateTime("createdAt"),
                        UpdatedAt = reader.GetDateTime("updateAt"),
                        Status = (TransactionStatus) reader.GetInt32("status")
                    });
                }

                return listTransaction;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            finally
            {
                cnn.Close();
            }
        }
    }
}