using SportsHub.AppService.Authentication.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.AppService.BackgroundServices
{
    public interface IKafkaServiceChannel
    {
        ValueTask AddArticleToChannel (CreateArticleDTO articleDTO);
        ValueTask<bool> WaitToReadAsync(CancellationToken cancellationToken);
        bool TryRead([MaybeNullWhen(false)] out CreateArticleDTO article, CancellationToken cancellationToken);
    }
}
