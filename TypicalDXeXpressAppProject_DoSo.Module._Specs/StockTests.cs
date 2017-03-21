using TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects;
using Xunit;
using System;
using DevExpress.Xpo;
using static TypicalDXeXpressAppProject_DoSo.Module.BusinessObjects.StockTransactionMethods;
using Shouldly;

namespace TypicalDXeXpressAppProject_DoSo.Module._Specs
{
    public class StockTests : XpoTestsBase
    {
        /// <summary>
        /// Create StockTransactions in this test for the StockItem BEFORE the date you are trying to calculate BalanceAmount for.
        /// Simply write a method somewhere that accepts DateTime and StockItem and returns the amount of StockItems available for that Date.
        /// </summary>
        [Fact]
        public void When_calculating_StockItemBalance_for_date_BalanceAmount_should_be_sum_of_all_previous_transactions_amounts()
        {

            var customer = new Customer(Uow) { DateOfBirth = new DateTime(1990, 1, 1) };
            var item = new StockItem(Uow) { ItemName = "test_item" };

            var transaction = new StockTransaction(Uow)
            {
                DateOfTransaction = new DateTime(2016, 6, 10),
                StockItem = item,
                Customer = customer,
                Amount = 100,
                TransactionNumberInt = 1
            };

            var transaction2 = new StockTransaction(Uow)
            {
                DateOfTransaction = new DateTime(2016, 7, 10),
                StockItem = item,
                Customer = customer,
                Amount = 12,
                TransactionNumberInt = 1
            };

            var transaction3 = new StockTransaction(Uow)
            {
                DateOfTransaction = new DateTime(2016, 11, 10),
                StockItem = item,
                Customer = customer,
                Amount = 50,
                TransactionNumberInt = 1
            };
            Uow.CommitChanges();
            CalculateBalanceForStockItemBefore(Uow, item, DateTime.Now).ShouldBe(162);
        }
        /// <summary>
        /// Create StockTransactions in this test for the StockItem AFTER the date you are trying to calculate BalanceAmount for.
        /// Simply write a method somewhere that accepts DateTime and StockItem and returns the amount of StockItems available for that Date.
        /// </summary>
        [Fact] 
        public void When_calculating_StockItemBalance_for_date_BalanceAmount_should_not_consider_future_transaction_amounts()
        {
            var customer = new Customer(Uow) { DateOfBirth = new DateTime(1990, 1, 1) };
            var item = new StockItem(Uow) { ItemName = "test_item" };

            var transaction0 = new StockTransaction(Uow)
            {
                DateOfTransaction = new DateTime(2012, 6, 10),
                StockItem = item,
                Customer = customer,
                Amount = 33,
                TransactionNumberInt = 1
            }; 

            var transaction = new StockTransaction(Uow)
            {
                DateOfTransaction = new DateTime(2016, 6, 10),
                StockItem = item,
                Customer = customer,
                Amount = 100,
                TransactionNumberInt = 1
            };

            var transaction2 = new StockTransaction(Uow)
            {
                DateOfTransaction = new DateTime(2016, 7, 10),
                StockItem = item,
                Customer = customer,
                Amount = 12,
                TransactionNumberInt = 1
            };

            var transaction3 = new StockTransaction(Uow)
            {
                DateOfTransaction = new DateTime(2016, 11, 10),
                StockItem = item,
                Customer = customer,
                Amount = 50,
                TransactionNumberInt = 1
            };
            Uow.CommitChanges();
            CalculateBalanceForStockItemBefore(Uow, item, (new DateTime(2014, 9, 11))).ShouldBe(33);
            CalculateBalanceForStockItemBefore(Uow, item, (new DateTime(2016, 9, 11))).ShouldBe(145);
        }
        /// <summary>
        /// For example, if there are two customers with IDs 105 & 203, StockTransactions created should be numbered as ST-105-1-17, ST-105-2-17 and ST-203-1-17, ST-203-2-17
        /// where last part is the StockTransaction's Year, second to last is the incrementing transaction identity for that customer for the year 2017
        /// </summary>
        [Fact]
        public void StockTransaction_Number_should_be_calculated_for_the_Customer_and_Year_of_the_StockTransaction()
        {
            var customer = new Customer(Uow) { ID = 0, DateOfBirth = new DateTime(1990, 1, 1) };
            var customer2 = new Customer(Uow) { ID = 1, DateOfBirth = new DateTime(1996, 1, 1) };
            var customer3 = new Customer(Uow) { ID = 2, DateOfBirth = new DateTime(1999, 1, 1) };

            var item = new StockItem(Uow) { ItemName = "test_item" }; 

            var transaction = new StockTransaction(Uow) { DateOfTransaction = DateTime.Now, StockItem = item, Customer = customer,
                Amount = 100};

            var transaction01 = new StockTransaction(Uow)
            {
                DateOfTransaction = DateTime.Now,
                StockItem = item,
                Customer = customer,
                Amount = 22
            };
            
            var transaction2 = new StockTransaction(Uow) { DateOfTransaction = DateTime.Now, StockItem = item, Customer = customer3,
                Amount = 100};
            Uow.CommitChanges();

            CalculateTransactionNumber(customer, 2).ShouldBe("ST-1-2-17");

            transaction.TransactionNumber.ShouldBe("ST-0-1-17");
            transaction01.TransactionNumber.ShouldBe("ST-0-2-17");
            transaction2.TransactionNumber.ShouldBe("ST-2-1-17");
        }

        /// <summary>
        /// Customer should have a collection of StockItemBalances which should be updated or created based on the StockTransactions taking place.
        /// Thus, if you create a new StockTransaction, you should see if StockItemBalance exists for this StockItem for this Customer, create if it doesn't and update if it does.
        /// </summary>
        [Fact]
        public void StockItemBalance_should_be_ADDED_to_customer_if_it_doesnt_exist()
        {
            var customer = new Customer(Uow) { DateOfBirth = new DateTime(1990, 1, 1) };
            var item = new StockItem(Uow) { ItemName = "test_item" };

            var transaction0 = new StockTransaction(Uow)
            {
                DateOfTransaction = new DateTime(2012, 6, 10),
                StockItem = item,
                Customer = customer,
                Amount = 33
            };
            
            Uow.CommitChanges();

        }
       // [Fact]
        public void StockItemBalance_should_be_UPDATED_if_it_exists_for_customer()
        {
            FailMiserably();
        }
    }
}