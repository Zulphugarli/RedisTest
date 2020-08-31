using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Funq;
using ServiceStack;
using ServiceStack.CacheAccess;

using ServiceStack.Configuration;
using ServiceStack.Redis;
using ServiceStack.ServiceInterface;
using ServiceStack.WebHost.Endpoints;

namespace NotesServer.AppHost
{
    public class NotesAppHost : AppHostBase
    {
        public NotesAppHost() : base("Notes Service", typeof(NotesService).Assembly)
        {

        }

        public override void Configure(Container container)
        {
            var appSettings = new AppSettings();


            // var redisServerAddress = ServiceAddressSettings.GetServiceAddress(Models.ServiceType.RedisServer);
            container.Register<IRedisClientsManager>(c => new PooledRedisClientManager("myadress:6379"));
            container.Register(c => c.Resolve<IRedisClientsManager>().GetCacheClient());

            //container.Register<ISessionFactory>(c => new SessionFactory(c.Resolve<ICacheClient>()));
            container.Register<ICacheClient>(c =>
                        (ICacheClient)c.Resolve<IRedisClientsManager>()
                        .GetCacheClient())
                        .ReusedWithin(Funq.ReuseScope.None);
            container.Register<IRedisClientFactory>(c => new RedisClientFactory());

            Plugins.Add(new SessionFeature());
        }
    }
}
