using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using WebCore.Utils.ModelHelper;

namespace WebCore.Utils.Commons
{
    public static class ExtensionMethods
    {
        // Deep clone
        public static T DeepClone<T>(this T a)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, a);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }

        public static SelectList ToSelectList<TKey, TValue>(this List<ComboboxResult<TKey, TValue>> comboboxResult)
        {
            return new SelectList(comboboxResult, nameof(ComboboxResult<TKey, TValue>.Value), nameof(ComboboxResult<TKey, TValue>.Display));
        }
    }
}
