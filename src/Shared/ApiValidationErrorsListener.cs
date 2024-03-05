using System.Diagnostics;
using System.Threading.Channels;

namespace Shared
{
    public class ApiValidationErrorsListener : BackgroundService
    {
        ChannelReader<string> _channelReader;

        public ApiValidationErrorsListener(ChannelReader<string> channelReader)
        {
            _channelReader = channelReader;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (await _channelReader.WaitToReadAsync(CancellationToken.None).ConfigureAwait(false))
            {
                while (_channelReader.TryRead(out string? incomingChannelMessage))
                {
                    Debug.WriteLine(incomingChannelMessage);
                }
            }
        }
    }
}
