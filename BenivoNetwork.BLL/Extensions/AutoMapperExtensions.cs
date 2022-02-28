using BenivoNetwork.BLL.Configuration;
using System.Collections.Generic;

namespace BenivoNetwork.BLL.Extensions
{
    public static class AutoMapperExtensions
    {
        public static T MapTo<T>(this object ob)
        {
            return AutoMapperConfig.Instance.Map<T>(ob);
        }

        public static IEnumerable<T> MapTo<T>(this IEnumerable<object> collection)
        {
            foreach (var ob in collection)
            {
                yield return AutoMapperConfig.Instance.Map<T>(ob);
            }
        }
    }
}
