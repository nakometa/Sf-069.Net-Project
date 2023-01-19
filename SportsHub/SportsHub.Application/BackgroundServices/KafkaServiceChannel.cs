using SportsHub.AppService.Authentication.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SportsHub.AppService.BackgroundServices
{
    public class KafkaServiceChannel : IKafkaServiceChannel
    {
        private readonly Channel<CreateArticleDTO> _channel;

        public KafkaServiceChannel()
        {
            _channel = Channel.CreateUnbounded<CreateArticleDTO>();
        }

        public ValueTask AddArticleToChannel(CreateArticleDTO articleDTO) =>
         _channel.Writer.WriteAsync(articleDTO);

        public bool TryRead([MaybeNullWhen(false)] out CreateArticleDTO article, CancellationToken cancellationToken) =>
          _channel.Reader.TryRead(out article);
        

        public ValueTask<bool> WaitToReadAsync(CancellationToken cancellationToken) =>
            _channel.Reader.WaitToReadAsync(cancellationToken);
        
    }
}
