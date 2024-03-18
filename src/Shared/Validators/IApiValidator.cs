using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Shared.Validators
{
    public interface IApiValidator
    {
        IEnumerable<string> Validate(IReadOnlyList<ApiDescriptionGroup> apiDescriptionGroups);
    }
}