using System;

namespace shb_bank.Entity
{
    public class Transaction
    {
        public string TransactionCode { get; set; }
        public string SenderAccountNumber { get; set; }

        public string ReceiverAccountNumber
        {
            get
            {
                return _receiverAccountNumber;
            }
            set
            {
                if (value.Length < 8)
                {
                    throw new Exception("Tài khoản người nhận phải có ít nhất 8 kí tự!!");
                }

                _receiverAccountNumber = value;
            }
        }

        private string _receiverAccountNumber;
        public string Message { get; set; }

        public double Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Số tiền phải lớn 0!!");
                }

                _amount = value;
            }
        }

        private double _amount;
        public double Fee { get; set; }
        public TransactionType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TransactionStatus Status { get; set; }
    }
    
    public enum TransactionType
    {
        Withdraw = 1, Deposit = 2, Tranfer = 3 
    }
    public enum TransactionStatus
    {
        Pending = 0, Done = 1, Fails = 0
    }
}