﻿using BankTransfer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransfer.Interfaces
{
    public interface IAccount
    {
        string AccountNumber { get; }
        double Balance { get; }



        void AddTransaction(Transaction transaction);

        IEnumerable<Transaction> GetTransactions();
        IEnumerable<Transaction> FilterTransactions(TransactionType? type, IAccount account);

        Task TransferAsync(IAccount toAccount, double amount, ITransactionProvider provider);

    }
}
