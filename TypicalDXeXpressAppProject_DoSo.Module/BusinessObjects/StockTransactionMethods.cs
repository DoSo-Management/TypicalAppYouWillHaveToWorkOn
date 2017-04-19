using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using DevExpress.Xpo;
using System.Linq.Expressions;
#pragma warning disable 252,253

namespace TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects
{
    public class StockTransactionMethods
    {
        public static decimal CalculateBalanceForStockItemBefore(Session session, StockItem item, DateTime date)
        {
            var balance = session.Query<StockTransaction>()
               .Where(t => t.StockItem == item)
               .Where(d => DateTime.Compare(d.DateOfTransaction, date) <= 0).Sum(a => a.Amount);
            return balance;
        }

        public static Result<StockBalance> ValidateBalance(Customer customer, StockItem item, Session session)
        {
            if (customer == null) return Result.Fail<StockBalance>("Customer is null!");
            if (item == null) return Result.Fail<StockBalance>("Item is null!");

            var balanceList = session.Query<StockBalance>()
                .Where(c => c.Customer == customer)
                .Where(c => c.StockItem == item);
            return !balanceList.Any() ? Result.Fail<StockBalance>("Empty List") : Result.Ok(balanceList.Single());
        }

        public static Result<decimal> CountAmount(Customer customer, StockItem item, Session session)
        {
            if (customer == null || item == null) return Result.Fail<decimal>("Customer or item is Null");

            var count = session.Query<StockTransaction>()
                .Where(t => t.StockItem == item && t.Customer == customer)
                .Sum(tr => tr.Amount);
            return Result.Ok(count);
        }

        public static int GetTransactionNumberInt(Customer customer, Session session)
        {
  
            if (customer == null) throw new ArgumentNullException(nameof(customer));
            if (session == null) throw new ArgumentNullException(nameof(session));

            var number = session.QueryInTransaction<StockTransaction>()
                .Where(t => t.Customer == customer) // Customer.StockTransactions 
                .OrderByDescending(t => t.TransactionNumberInt)
                .FirstOrDefault();
            Debug.Assert(number != null, "number != null");
            var value = number.TransactionNumberInt;
            return value;
        }

        public static string CalculateTransactionNumber(Customer customer, int transactionNumberInt)
        {
            if (customer == null) throw new ArgumentNullException(nameof(customer));

            var year = (DateTime.Now.Year % 100).ToString();
            var id = customer.ID.ToString();
            var tnumber = "ST-" + id + "-" + transactionNumberInt + "-" + year;
            return tnumber;
        }
    }
}
