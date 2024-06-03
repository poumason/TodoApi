using TodoApi.Models;
using System.Linq;
using System.Collections.Generic;

namespace TodoApi.Libs
{
    public static class ObjectExtensions
    {
        public static void CopyProperiesFromBaseClass<T, Y>(this T self, Y from)
        where T : BaseInfo
        where Y : BaseInfo
        {
            var selfProperties = self.GetType().GetProperties();
            var fromProperties = from.GetType().GetProperties();

            // foreach (var parentProperty in parentProperties)
            // {
            //     foreach (var childProperty in childProperties)
            //     {
            //         if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
            //         {
            //             childProperty.SetValue(child, parentProperty.GetValue(parent));
            //             break;
            //         }
            //     }
            // }
            foreach (var item in fromProperties)
            {
                Console.WriteLine($"feed proprerty {item.Name}={item.GetValue(from)}");
                selfProperties.Where(x => x.Name == item.Name).FirstOrDefault()?.SetValue(self, item.GetValue(from));
            }

        }
    }
}