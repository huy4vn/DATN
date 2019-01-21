using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Utils
{
    class PropertyHelper<T>
    {
        public static List<Object> getProperties(T item)
        {
            List<Object> result = new List<object>();
            foreach (var prop in item.GetType().GetProperties())
            {
                if (!prop.Name.Equals("id"))
                {
                    result.Add(prop.GetValue(item, null));
                }
            }
            return result;
        }
    }
}
