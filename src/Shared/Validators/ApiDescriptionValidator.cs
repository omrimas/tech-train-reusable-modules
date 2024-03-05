using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing.Patterns;

namespace Shared.Validators;

public class ApiDescriptionValidator : IApiValidator
{
    public string? Validate(ApiDescription apiDescription)
    {
        if (string.IsNullOrWhiteSpace(apiDescription.RelativePath))
            return "All Api methods should have a path";

        var Pattern = RoutePatternFactory.Parse(apiDescription.RelativePath);

        if (!apiDescription.RelativePath.All(char.IsAscii))
            return "urls should only contain ascii characters";

        ControllerActionDescriptor? controllerActionDescriptor = apiDescription.ActionDescriptor as ControllerActionDescriptor;
        if (controllerActionDescriptor == null)
        {
            return null;
        }

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

        if (apiDescription.HttpMethod == "GET" &&
            !(controllerActionDescriptor.ActionName.StartsWith("Get") || controllerActionDescriptor.ActionName.StartsWith("List")))
        {
            return $"Illegal action name {controllerActionDescriptor.ActionName} - GET actions should start with Get or List";
        }

        if (apiDescription.HttpMethod == "PUT" &&
            !(controllerActionDescriptor.ActionName.StartsWith("Add") || controllerActionDescriptor.ActionName.StartsWith("Update")))
        {
            return $"Illegal action name {controllerActionDescriptor.ActionName} - PUT actions should start with Add or Update";
        }

        return null;
    }
}