using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace TestAPI.MultipartRequest
{
    public class MyFormValueProvider : FormValueProvider
    {
        public MyFormValueProvider(BindingSource bindingSource, IFormCollection values, CultureInfo culture) : base(bindingSource, values, culture)
        {
        }

        public string FileName { get; set; }
    }
}
