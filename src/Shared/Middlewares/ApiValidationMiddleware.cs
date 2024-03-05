using System.Threading.Channels;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Routing.Template;
using Shared.Controllers.Utils;

namespace Shared.Middlewares
{
    public class ApiValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TemplateMatcher _requestMatcher;
        IApiDescriptionGroupCollectionProvider _apiDescriptionGroupCollectionProvider;
        ChannelWriter<string> _channelWriter;

        public ApiValidationMiddleware(
            RequestDelegate next,
            IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider,
            ChannelWriter<string> channelWriter)
        {
            _apiDescriptionGroupCollectionProvider = apiDescriptionGroupCollectionProvider;
            _next = next;
            _requestMatcher = new TemplateMatcher(TemplateParser.Parse("validateApi"), new RouteValueDictionary());
            _channelWriter = channelWriter;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var routeValues = new RouteValueDictionary();
            if (!_requestMatcher.TryMatch(httpContext.Request.Path, routeValues))
            {
                await _next(httpContext);
                return;
            }

            var validator = new ApiDescriptionValidator();
            foreach (var group in _apiDescriptionGroupCollectionProvider.ApiDescriptionGroups.Items)
            {
                foreach (var item in group.Items)
                {
                    var result = validator.Validate(item);
                    if (result != null)
                        await _channelWriter.WriteAsync(result);
                }
            }
        }
    }
}
