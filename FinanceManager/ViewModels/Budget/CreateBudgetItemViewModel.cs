using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceManager.ViewModels.Budget
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Models.Category;
    using Models.Period;

    public class CreateBudgetItemViewModel
    {
        private IEnumerable<SelectListItem> _categoryItems;
        public IEnumerable<Category> Categories { get; set; }
        public string SelectedCategory { get; set; }
        public IEnumerable<SelectListItem> CategoryItems => _categoryItems ?? (_categoryItems = Categories.Select(c => CreateSelectItem(c.Name, c.Name)));


        private IEnumerable<SelectListItem> _periodItems;
        public IEnumerable<Period> Periods { get; set; }
        public string SelectedPeriod { get; set; }
        public IEnumerable<SelectListItem> PeriodItems => _periodItems ?? (_periodItems = Periods.Select(p => CreateSelectItem(p.Name, p.Name)));

        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public double Amount { get; set; }
        public string Description { get; set; }

        private SelectListItem CreateSelectItem(string value, string text)
        {
            return new SelectListItem() { Text = text, Value = value };
        }

    }
}