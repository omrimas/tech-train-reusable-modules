using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing.Patterns;

namespace Shared.Controllers.Utils;

public class ApiDescriptionValidator
{
    public string? Validate(ApiDescription apiDescription)
    {
        if (string.IsNullOrWhiteSpace(apiDescription.RelativePath))
            return "All Api methods should have a path";

        var Pattern = RoutePatternFactory.Parse(apiDescription.RelativePath);

        if (!apiDescription.RelativePath.All(char.IsAscii))
            return "urls should only contain ascii characters";

        foreach (ApiParameterDescription parameterDescription in apiDescription.ParameterDescriptions)
        {
            if (parameterDescription.Source == BindingSource.Path && !apiDescription.RelativePath.Contains("{" + parameterDescription.Name + "}"))
            {
                return $"path parameter {parameterDescription.Name} wasn't found in path {apiDescription.RelativePath}.";
            }

            if (parameterDescription.Name.Contains("id", StringComparison.OrdinalIgnoreCase) && parameterDescription.Type != typeof(string))
            {
                return $"id parameter ({parameterDescription.Name}) should be of type string";
            }
        }

        return null;
    }
}