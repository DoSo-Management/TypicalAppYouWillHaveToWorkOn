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
        
        public Customer Customer { get; set; }

        public StockItem StockItem { get; set; }

        public decimal Amount { get; set; }

        public string TransactionNumber { set; get; }

    }
}