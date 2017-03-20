using System;
using System.Linq;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class StockTransaction : XPLiteObjectBase
    {
        public StockTransaction(Session session) : base(session)
        { }

        public DateTime DateOfTransaction { get; set; }

        [Association]
        public Customer Customer { get; set; }

        [Association]
        public StockItem StockItem { get; set; }

        public decimal Amount { get; set; }

        public int TransactionNumberInt { get; set; }
        public string TransactionNumber { get { return CalculateTransactionNumber(); } }

        private string CalculateTransactionNumber()
        {
            var TNumber = Session.Query<StockTransaction>()
               .Where(t => t.Customer == Customer)
               .OrderByDescending(t => t.TransactionNumberInt)
               .Take(1);
            
            /*
             * var lastTransactionV2 = Customer
               .StockTransactions
               .OrderByDescending(t => t.TransactionNumberInt)
               .Take(1); 
               */

            string year = DateTime.Now.Year.ToString();
            string TransactionNumber = string.Concat("ST-", Customer.ID.ToString(), TNumber.ToString(), year);
            return TransactionNumber;
        }
    }
}