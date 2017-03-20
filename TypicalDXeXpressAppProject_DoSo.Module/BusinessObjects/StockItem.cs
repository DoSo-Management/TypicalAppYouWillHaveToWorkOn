using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class StockItem : XPLiteObjectBase
    {

        public StockItem(Session session) : base(session)
        { }
        public string ItemName { get; set; }
    }

}