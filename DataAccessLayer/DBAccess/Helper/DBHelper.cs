using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccessLayer.DBAccess
{
    static class DBHelper
    {
        public static object Optional<T>(this T value, T optionalValue = default(T))
        {
            return Equals(value, optionalValue) ? DBNull.Value : (object)value;
        }

        public static T DBNullTo<T>(this object value, T substitutionValue = default(T))
        {
            return Convert.IsDBNull(value) ? substitutionValue : (T)value;
        }
    }
}
