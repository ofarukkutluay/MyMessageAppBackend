using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.Concretes;
using Core.Utilities.Security.JWT;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using DryIoc;

namespace Business.DependencyResolvers.DryIoc
{
    public class DryIocBusinessModule : Container
    {
        public static void Register(IContainer r)
        {
            r.Register<IUserService, UserManager>(Reuse.Singleton);
            r.Register<IUserRepository,MongoDbUserDal>(Reuse.Singleton);
            r.Register<IMessageService,MessageManager>(Reuse.Singleton);
            r.Register<IMessageRepository,MongoDbMessageDal>(Reuse.Singleton);
            r.Register<IAuthService,AuthManager>();
            r.Register<ITokenHelper,JwtHelper>();
        }
    }
}
