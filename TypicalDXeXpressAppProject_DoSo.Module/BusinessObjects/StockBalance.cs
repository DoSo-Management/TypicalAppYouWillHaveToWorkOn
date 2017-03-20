using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

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

        private decimal CountAmount() {

            return 0;
        }
    }
}