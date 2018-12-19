using Bit.Application.CacheManager;
using Bit.Application.Interfaces;
using Bit.Application.Manager;
using Bit.Application.Managers;
using Bit.Application.PersonManager;
using Bit.Application.Repository;
using Bit.Persistence.Repository;
using Ninject.Modules;

namespace Bit.WebApi.Modules
{
    public class MainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPersonRepository>().To<PersonRepository>();
            Bind<ICacheRepository>().To<CacheRepository>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IPersonManager>().To<PersonManager>();
            Bind<ICacheManager>().To<CacheManager>();
            Bind<IUserManager>().To<UserManager>();
            Bind<IEncryption>().To<Encryption>();
        }
    }
}