using System;

namespace WebCore.Utils.FilterHelper
{
    public class FilterAttribute : Attribute
    {
        public string FilterProperty { get; set; }
        public FilterComparison FilterComparison { get; set; }

        public FilterAttribute(FilterComparison filterComparison, string filterProperty = null)
        {
            this.FilterComparison = filterComparison;
            FilterProperty = filterProperty;
        }
    }
}
