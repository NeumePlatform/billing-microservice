using System;
using service_billing.Data;

namespace service_billing.services.TransactionService
{
    public class TransactionService : ITransactionService
    {
        private readonly DataContext _context;

        public TransactionService(DataContext context)
        {
            _context = context;
        }

        //public async Task<Transaction> handleTransaction(Transaction transaction)
        //{
        //    _context.transactions.Add(transaction);
        //    await _context.SaveChangesAsync();

        //    return transaction;
        //}

        public async Task<TransactionModel.Transaction> handleTransaction(TransactionModel.Transaction transaction)
        {
            Console.WriteLine(_context.transactions.Add(transaction));
            
            //var response = await _context.SaveChangesAsync();
            Console.WriteLine(_context.SaveChanges());

            //Console.WriteLine(response);
            return transaction;
        }
    }
}

