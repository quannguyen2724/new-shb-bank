using System;
using System.Collections.Generic;
using shb_bank.Entity;
using shb_bank.Model;

namespace shb_bank.Controller
{
    public class TransactionController
    {
        private TransactionModel _transactionModel = new TransactionModel();

        public void Deposit()
        {
            Console.WriteLine("Enter the amount: ");
            var amount = double.Parse(Console.ReadLine());
            _transactionModel.Deposit(AccountController.currentAccount.AccountNumber, amount);
        }

        public void Withdraw()
        {
            Console.WriteLine("Enter the amount: ");
            var amount = double.Parse(Console.ReadLine());
            _transactionModel.Withdraw(AccountController.currentAccount.AccountNumber, amount);
        }

        public void Transfer()
        {
            Console.WriteLine("Enter receiver account number: ");
            var receiverAccount = Console.ReadLine();
            Console.WriteLine("Enter the amount: ");
            var amount = double.Parse(Console.ReadLine());
            _transactionModel.Transfer(AccountController.currentAccount.AccountNumber, receiverAccount, amount);
        }

        public void PrintListTransaction()
        {
            string accountNumber;
            if (AccountController.currentAccount.Role == AccountRole.Admin)
            {
                Console.WriteLine("Vui lòng nhập số tài khoản cần kiểm tra giao dịch:");
                accountNumber = Console.ReadLine();
            }
            else
            {
                accountNumber = AccountController.currentAccount.AccountNumber;
            }

            var listTransaction = _transactionModel.GetTransactionHistory(accountNumber);
            Console.WriteLine(
                "------------------------------------------------------------------------------------------------");
            if (listTransaction.Count > 0)
            {
                foreach (var transaction in listTransaction)
                {
                    Console.WriteLine(
                        $"{transaction.TransactionCode} | {transaction.SenderAccountNumber} | {transaction.ReceiverAccountNumber} | {transaction.Type} | {transaction.Amount} | {transaction.Fee} | {transaction.Message} | {transaction.CreatedAt} | {transaction.UpdatedAt} | {transaction.Status}");
                }
            }
            else
            {
                Console.WriteLine("Không có bản ghi nào hoặc tài khoản không tồn tại");
            }
            Console.WriteLine(
                "-------------------------------------------------------------------------------------------------");
            _transactionModel.TransactionPage(listTransaction);
        }

        public void PrintlistTransactionHistory()
        {
            var listTransaction = _transactionModel.GetAllTransactionHistory();
            if (listTransaction.Count > 0)
            {
                foreach (var transaction in listTransaction)
                {
                    Console.WriteLine(
                        $"{transaction.TransactionCode} | {transaction.SenderAccountNumber} | {transaction.ReceiverAccountNumber} | {transaction.Type} | {transaction.Amount} | {transaction.Fee} | {transaction.Message} | {transaction.CreatedAt} | {transaction.UpdatedAt} | {transaction.Status}");
                }
            }
            else
            {
                Console.WriteLine("Không có bản ghi nào");
            }
            _transactionModel.TransactionPage(listTransaction);
        }
    }
}