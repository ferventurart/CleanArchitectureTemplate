using ApplicationCore.Interfaces;

namespace ApplicationCore.Services
{
    public class UriComposer : IUriComposer
    {
        private readonly ApplicationSettings _applicationSettings;

        public UriComposer(ApplicationSettings applicationSettings) => _applicationSettings = applicationSettings;

        public string ComposePicUri(string uriTemplate)
        {
            return uriTemplate.Replace("http://applicationurltobereplaced", _applicationSettings.AppBaseUrl);
        }
    }
}
