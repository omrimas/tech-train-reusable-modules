using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Shared.Controllers.Utils;

namespace Shared.Controllers
{
    [ApiController]
    public class ApiValidationController : ControllerBase
    {
        private readonly IApiDescriptionGroupCollectionProvider _apiDescriptionGroupCollectionProvider;

        public ApiValidationController(IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider)
        {
            _apiDescriptionGroupCollectionProvider = apiDescriptionGroupCollectionProvider;
        }

        [HttpGet]
        [Route("/validateApi")]
        public IEnumerable<string> ValidateApi()
        {
            var validator = new ApiDescriptionValidator();
            foreach(var group in _apiDescriptionGroupCollectionProvider.ApiDescriptionGroups.Items)
            {
                foreach(var item in group.Items)
                {
                     var result = validator.Validate(item);
                     if(result != null)
                        yield return result;       
                }
            }
        }   
    }
}
