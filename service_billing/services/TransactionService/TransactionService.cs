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
            Console.WriteLine("Add Transaction: " + _context.transactions.Add(transaction));
            Console.WriteLine("Transaction: " + transaction);

            Console.WriteLine(_context.ChangeTracker.HasChanges());
            //var response = await _context.SaveChangesAsync();
            Console.WriteLine("Save Changes: " + _context.SaveChanges());
            Console.WriteLine("Changes Saved!");

            //Console.WriteLine(response);
            return transaction;
        }
    }
}

