using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using DevExpress.Xpo;

namespace TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects
{
    class StockTransactionMethods
    {

        public decimal CalculateBalanceForStockItemBefore(Session session, StockItem item, DateTime date)
        {
            var balance = session.Query<StockTransaction>()
               .Where(t => t.StockItem == item)
               .Where(d => DateTime.Compare(d.DateOfTransaction, date) <= 0).Sum(a => a.Amount);
            return balance;
        }

        public decimal CalculateBalanceForStockItemAfter(Session session, StockItem item, DateTime date)
        {
            var balance = session.Query<StockTransaction>()
               .Where(t => t.StockItem == item)
               .Where(d => DateTime.Compare(d.DateOfTransaction, date) > 0).Sum(a => a.Amount);
            return balance;
        }


    }
}
