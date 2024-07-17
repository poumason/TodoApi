using System;
using System.Reflection;

namespace TodoApi.ServiceModel.Flow
{
    internal class PropertiesCheckFlow<T> : IPrechekFlow
    {
        internal T Content { get; }

        public PropertiesCheckFlow(T _object)
        {
            Content = _object;
        }

        public async Task<Tuple<bool, ErrorData?>> Validate()
        {
            // foreach (PropertyInfo pi in Content.GetType().GetProperties())
            // {
            //     if (pi.PropertyType == typeof(string))
            //     {
            //         string value = (string)pi.GetValue(Content);
            //         if (string.IsNullOrEmpty(value))
            //         {
            //             return true;
            //         }
            //     }
            // }
            return await Task.FromResult(Tuple.Create<bool, ErrorData?>(false, null));
        }
    }
}