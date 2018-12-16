using System;
using System.ComponentModel.DataAnnotations;
using WebCore.Entities;
using WebCore.Utils.Config;
using WebCore.Utils.FilterHelper;
using WebCore.Utils.ModelHelper;

namespace WebCore.Services.Share.Admins.Featureds.Dto
{
    // Featured
    public class FeaturedFilterInput : IPagingAndSortingFilterDto
    {
        public FeaturedFilterInput()
        {
            RecordStatus = ConstantConfig.RecordStatusConfig.Active;
        }
        [Filter(FilterComparison.Contains, nameof(Featured.Text))]
        public string TextContains { get; set; }

        [Filter(FilterComparison.Equal, nameof(Featured.Text))]
        public string TextEqual { get; set; }

        [Filter(FilterComparison.Equal, nameof(Featured.Number))]
        public int? NumberEqual { get; set; }

        [Filter(FilterComparison.LessThan, nameof(Featured.Number))]
        public int? NumberLessThan { get; set; }

        [Filter(FilterComparison.GreaterThan, nameof(Featured.Number))]
        public int? NumbeGreaterThan { get; set; }

        [Filter(FilterComparison.Equal, nameof(Featured.Date))]
        public DateTime? DateEqual { get; set; }

        [Filter(FilterComparison.LessThan, nameof(Featured.Date))]
        public DateTime? DateLessThan { get; set; }

        [Filter(FilterComparison.GreaterThan, nameof(Featured.Date))]
        public DateTime? DateGreaterThan { get; set; }

        [Filter(FilterComparison.Equal, nameof(Featured.Money))]
        public decimal? MoneyEqual { get; set; }

        [Filter(FilterComparison.LessThan, nameof(Featured.Money))]
        public decimal? MoneyLessThan { get; set; }

        [Filter(FilterComparison.GreaterThan, nameof(Featured.Money))]
        public decimal? MoneyGreaterThan { get; set; }

        [Filter(FilterComparison.Equal)]
        public string Combobox { get; set; }

        [Filter(FilterComparison.Contains)]
        public string MultiSelect { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Sorting { get; set; }
        public long RecordStatus { get; set; }
    }
}
