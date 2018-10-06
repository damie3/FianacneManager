namespace FinanceManager.ViewModels.Reports
{
    using System.ComponentModel.DataAnnotations;

    public class ExpenditurePerCategoryPerPeriodReportItem
    {
        public string Category { get; set; }
        public string Period { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}")]
        public double Amount { get; set; } = 0;


    }
}