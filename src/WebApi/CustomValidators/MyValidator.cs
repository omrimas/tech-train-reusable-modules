using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Shared.Validators;

namespace Webapi.CustomValidators
{
    public class MyValidator : IApiValidator
    {
        public IEnumerable<string> Validate(IReadOnlyList<ApiDescriptionGroup> apiDescriptionGroups)
        {
            foreach (var apiDescriptionGroup in apiDescriptionGroups)
            {
                var distinctResourcesInController = apiDescriptionGroup.Items.Select(apiDescription => apiDescription.ParameterDescriptions.FirstOrDefault()?.Name).Distinct();

                if (distinctResourcesInController.Count() > 1)
                {
                    return [$"Cannot have more than 1 resource in each controller. ({JsonSerializer.Serialize(distinctResourcesInController)})"];
                }
            }

            return [];
        }
    }
}
