using System;
namespace service_billing.services.TransactionService
{
	public interface ITransactionService
	{
		Task<Transaction> handleTransaction(Transaction transaction);
	}
}

