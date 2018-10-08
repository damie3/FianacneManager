using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceManager.ViewModels.Reports.SavingsReport
{
    using System.ComponentModel.DataAnnotations;

    public class SavingsReportViewModel
    {
        public IEnumerable<SavingsReportViewModelItem> ViewModelItems { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public double ActualSavingsToDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public double ExpectedSavingsToDate { get; set; }
    }
}