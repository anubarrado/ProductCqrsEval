using Application.Common.Interfaces;
using LazyCache;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CacheService : ICacheService
    {
        private readonly IConfiguration _configuration;
        private readonly IAppCache _appCache;

        public CacheService(IConfiguration configuration, IAppCache appCache)
        {
            _configuration = configuration;
            _appCache = appCache;
        }

        public string GetCacheByKey(bool key)
        {
            try
            {
                if (key)
                    return _appCache.GetOrAdd("Status.Active",
                        () => "Active",
                        DateTime.Now.AddMinutes(1)
                        );
                else
                    return _appCache.GetOrAdd("Status.Inactive",
                        () => "Inactive",
                        DateTime.Now.AddMinutes(1)
                        );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }
    }
}
