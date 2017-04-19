using System;
using System.Linq;
using CSharpFunctionalExtensions;
using DevExpress.ExpressApp.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.ComponentModel;

namespace TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Customer : XPLiteObjectBase
    {
        public Customer(Session session) : base(session) { }

        public DateTime DateOfBirth { get; set; }
        [Browsable(false)]
        public Result<int> AgeCalculationResult => DateOfBirth.GetAge(DateTime.Now);
        public string AgeCalculationResultString => AgeCalculationResult.ResultString();
        public int Age => AgeCalculationResult.IsSuccess ? AgeCalculationResult.Value : -1;

        public bool IsDeactivated { get; set; }

        public enum CustomerStatusEnum
        {
            Regular,
            Forwarder,
            Reinsurer
        }

        [Size(100)]
        [SearchMemberOptions(SearchMemberMode.Include), RuleRequiredField]
        public string Name { get; set; }

        public string Email { get; set; }

        public CustomerStatusEnum? CustomerStatus { get; set; }

        [Association]
        public XPCollection<Contract> Contracts => GetCollection<Contract>(nameof(Contracts));
        [Association]
        public XPCollection<Transaction> Transactions => GetCollection<Transaction>(nameof(Transactions));
        [Association]
        public XPCollection<Invoice> Invoices => GetCollection<Invoice>(nameof(Invoices));
        [Association]
        public XPCollection<ServiceRateDiscount> ServiceRateDiscounts => GetCollection<ServiceRateDiscount>(nameof(ServiceRateDiscounts));
        [Association]
        public XPCollection<StockTransaction> StockTransactions => GetCollection<StockTransaction>(nameof(StockTransactions));
        [Association]
        public XPCollection<StockBalance> StockBalances => GetCollection<StockBalance>(nameof(StockBalances));

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!Contracts.Any())
                Contracts.Add(new Contract(Session));
        }
    }
}
