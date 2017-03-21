using System;
using System.Linq;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using CSharpFunctionalExtensions;
using static TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects.StockTransactionMethods;
using static TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects.TransactionMethods;


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

        public string TransactionNumber { get; private set; }

        protected override void OnSaving()
        {
            base.OnSaving();
            if (Customer?.StockBalances == null)
                return;

            TransactionNumber = CalculateTransactionNumber(Customer, TransactionNumberInt);

            // if Customer is valid, Check StockItem and StockBalance for this customer
            ValidateCustomer(Customer)
               .OnSuccess(() => 
                    ValidateBalance(Customer, StockItem, Session)
                        .OnSuccess(b => b.Amount += Amount)
                        .OnFailure(() =>
                        {
                            var newItem = new StockBalance(Session)
                            {
                                StockItem = StockItem,
                                Customer = Customer,
                                Amount = Amount
                            };
                        }));
        }
    }
}