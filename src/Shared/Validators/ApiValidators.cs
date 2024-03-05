
using System.Collections;

namespace Shared.Validators
{
    public class ApiValidators : IApiValidators
    {
        public IEnumerable<IApiValidator> Validators { get; init; }

        public ApiValidators(IEnumerable<IApiValidator> validators)
        {
            Validators = validators;
        }

        public IEnumerable<IApiValidator> GetValidators()
        {
            return Validators;
        }
    }
}
