using System;
using TransactionModel;
namespace service_billing.services.TransactionService
{
	public interface ITransactionService
	{
		Task<TransactionModel.Transaction> handleTransaction(TransactionModel.Transaction transaction);
	}
}

