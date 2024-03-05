using System.Collections;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Shared.Validators
{
    public interface IApiValidator
    {
        string? Validate(ApiDescription apiDescription);
    }
}