using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using CSharpFunctionalExtensions;
using DevExpress.Data.ODataLinq.Helpers;


namespace TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class StockBalance : XPLiteObjectBase
    { 
        public StockBalance(Session session) : base(session) {}

        [Association]
        public StockItem StockItem { get; set; }

        [Association]
        public Customer Customer { get; set; }

        public decimal Amount { get; set; }

    }
}