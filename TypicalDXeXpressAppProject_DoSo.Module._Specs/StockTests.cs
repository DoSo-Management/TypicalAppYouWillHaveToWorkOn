using Xunit;

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
            //StockTransactionMethods.CalculateBalanceForStockItem(StockItem item, DateTime date)..
            FailMiserably();
        }
        /// <summary>
        /// Create StockTransactions in this test for the StockItem AFTER the date you are trying to calculate BalanceAmount for.
        /// Simply write a method somewhere that accepts DateTime and StockItem and returns the amount of StockItems available for that Date.
        /// </summary>
        [Fact]
        public void When_calculating_StockItemBalance_for_date_BalanceAmount_should_not_consider_future_transaction_amounts()
        {
            FailMiserably();
        }
        /// <summary>
        /// For example, if there are two customers with IDs 105 & 203, StockTransactions created should be numbered as ST-105-1-17, ST-105-2-17 and ST-203-1-17, ST-203-2-17
        /// where last part is the StockTransaction's Year, second to last is the incrementing transaction identity for that customer for the year 2017
        /// </summary>
        [Fact]
        public void StockTransaction_Number_should_be_calculated_for_the_Customer_and_Year_of_the_StockTransaction()
        {
            FailMiserably();
        }

        /// <summary>
        /// Customer should have a collection of StockItemBalances which should be updated or created based on the StockTransactions taking place.
        /// Thus, if you create a new StockTransaction, you should see if StockItemBalance exists for this StockItem for this Customer, create if it doesn't and update if it does.
        /// </summary>
        [Fact]
        public void StockItemBalance_should_be_ADDED_to_customer_if_it_doesnt_exist()
        {
            FailMiserably();
        }
        [Fact]
        public void StockItemBalance_should_be_UPDATED_if_it_exists_for_customer()
        {
            FailMiserably();
        }
    }
}