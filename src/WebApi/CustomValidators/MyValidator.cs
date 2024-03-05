using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Shared.Validators;

namespace Webapi.CustomValidators
{
    public class MyValidator : IApiValidator
    {
        public string? Validate(ApiDescription apiDescription)
        {
            return $"{nameof(MyValidator)}.{nameof(Validate)}";
        }
    }
}
