using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gamespace_api.Services
{
    public class GameSpaceService
    {
        private readonly ILogger<GameSpaceService> _logger;

        public GameSpaceService(
            ILogger<GameSpaceService> logger,
            IConfiguration config
        )
        {
            _logger = logger;
        }
    }
}
