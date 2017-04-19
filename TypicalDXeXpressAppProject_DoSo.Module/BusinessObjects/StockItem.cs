using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class StockItem : XPLiteObjectBase
    {
        // now update line 9

        //hotfix N1
        public StockItem(Session session) : base(session)
        { }
        public string ItemName { get; set; }

        [Association]
        public XPCollection<StockTransaction> StockTransactions => GetCollection<StockTransaction>(nameof(StockTransactions));

        [Association]
        public XPCollection<StockBalance> StockBalances => GetCollection<StockBalance>(nameof(StockBalances));
    }
    test1
     test2
}