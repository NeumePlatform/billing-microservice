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
            _context.transactions.Add(transaction);

            Console.WriteLine("Database Connection: " + _context.Database.CanConnect());

            //Console.WriteLine(_context.ChangeTracker.HasChanges());
            //var response = await _context.SaveChangesAsync();
           

            await _context.SaveChangesAsync();

            //Console.WriteLine(response);
            return transaction;
        }
    }
}

