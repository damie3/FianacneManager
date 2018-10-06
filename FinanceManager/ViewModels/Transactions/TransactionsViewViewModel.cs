namespace FinanceManager.ViewModels.Transactions
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class TransactionsViewViewModel
    {
        public string GroupType { get; set; }
        public IEnumerable<TransactionGroupViewModel> TransactionGroupViewModels { get; set; }
        public IEnumerable<SelectListItem> GroupingItems { get; private set; }
        public IEnumerable<SelectListItem> Periods { get; private set; }
        public IEnumerable<SelectListItem> Categories { get; private set; }
        public IEnumerable<SelectListItem> Accounts { get; private set; }

        public string SelectedGrouping { get; set; }

        public TransactionsViewViewModel()
        {
            GroupingItems = CreateGroupingItems();
        }

        private IEnumerable<SelectListItem> CreateGroupingItems()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem() {Text = "Transactions", Value = "Transactions"},
                new SelectListItem() { Text = "Account", Value = "Account" },
                new SelectListItem() { Text = "Category", Value = "Category" },
                new SelectListItem() { Text = "Period", Value = "Period" },
            };
            
        }
    }
}