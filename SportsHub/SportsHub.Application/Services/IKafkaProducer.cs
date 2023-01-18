using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.AppService.Services
{
    public interface IKafkaProducer
    {
        public Task StartAsync(CreateArticleDTO article);
    }
}
