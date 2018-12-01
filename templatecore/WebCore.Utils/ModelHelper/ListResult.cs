using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WebCore.Utils.ModelHelper
{
    public class ListResult<TModel>
    {
        public List<TModel> DataList { get; set; }
        public List<TModel> GetByCondition(Func<TModel, bool> predicate)
        {
            return DataList.Where(predicate).ToList();
        }
        public TModel GetFirstByCondition(Func<TModel, bool> predicate)
        {
            return DataList.Where(predicate).FirstOrDefault();
        }
    }
}
