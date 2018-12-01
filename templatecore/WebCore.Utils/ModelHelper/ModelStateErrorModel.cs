using System;
using System.Collections.Generic;
using System.Text;

namespace WebCore.Utils.ModelHelper
{
    public class ModelStateErrorModel
    {
        public string PropertyName { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}
