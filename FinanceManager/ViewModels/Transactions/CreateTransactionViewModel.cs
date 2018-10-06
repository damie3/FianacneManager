namespace FinanceManager.ViewModels.Transactions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;
    using Models.Account;
    using Models.Category;

    public class CreateTransactionViewModel
    {
        private IEnumerable<SelectListItem> _accountItems;
        private IEnumerable<SelectListItem> _categoryItems;
        public IEnumerable<Category> Categories { get; set; }
        public string SelectedCategory { get; set; }
        public IEnumerable<SelectListItem> CategoryItems => _categoryItems ?? (_categoryItems = Categories.Select(c => CreateSelectItem(c.Name, c.Name)));

        public IEnumerable<Account> Accounts { get; set; }

        public IEnumerable<SelectListItem> AccountItems => _accountItems ?? (_accountItems = Accounts.Select(c => CreateSelectItem(c.Name, c.Name)));
        public string SelectedAccount { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double Amount { get; set; }
        public string Description { get; set; }

        private SelectListItem CreateSelectItem(string value, string text)
        {
            return new SelectListItem() { Text = text, Value = value };
        }


    }
}