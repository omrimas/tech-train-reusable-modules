using System.Threading.Channels;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Routing.Template;
using Shared.Validators;

namespace Shared.Middlewares
{
    public class ApiValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TemplateMatcher _requestMatcher;
        IApiDescriptionGroupCollectionProvider _apiDescriptionGroupCollectionProvider;
        ChannelWriter<string> _channelWriter;
        IApiValidators _apiValidators;

        public ApiValidationMiddleware(
            RequestDelegate next,
            IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider,
            ChannelWriter<string> channelWriter,
            IApiValidators apiValidators)
        {
            _apiDescriptionGroupCollectionProvider = apiDescriptionGroupCollectionProvider;
            _next = next;
            _requestMatcher = new TemplateMatcher(TemplateParser.Parse("validateApi"), new RouteValueDictionary());
            _channelWriter = channelWriter;
            _apiValidators = apiValidators;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var routeValues = new RouteValueDictionary();
            if (!_requestMatcher.TryMatch(httpContext.Request.Path, routeValues))
            {
                await _next(httpContext);
                return;
            }

            foreach (var apiValidator in _apiValidators.GetValidators())
            {
                foreach (string validationError in apiValidator.Validate(_apiDescriptionGroupCollectionProvider.ApiDescriptionGroups.Items))
                {
                    await _channelWriter.WriteAsync(validationError);
                }
            }
        }
    }
}
