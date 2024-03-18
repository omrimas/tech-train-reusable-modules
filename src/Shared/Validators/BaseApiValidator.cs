using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Shared.Validators
{
    public abstract class BaseApiValidator : IApiValidator
    {
        public IEnumerable<string> Validate(IReadOnlyList<ApiDescriptionGroup> apiDescriptionGroups)
        {
            foreach (var group in apiDescriptionGroups)
            {
                foreach (var item in group.Items)
                {
                    var result = this.Validate(item);
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        yield return result;
                    }
                }
            }
        }

        public virtual string? Validate(ApiDescription apiDescription)
        {
            throw new NotImplementedException();
        }
    }
}
