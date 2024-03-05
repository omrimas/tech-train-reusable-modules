using System.Collections;

namespace Shared.Validators
{
    public interface IApiValidators
    {
        IEnumerable<IApiValidator> GetValidators();
    }
}
