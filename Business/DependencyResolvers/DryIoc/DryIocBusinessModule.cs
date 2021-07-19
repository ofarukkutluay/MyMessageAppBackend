using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.Concretes;
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
            r.Register<IUserRepository,UserDal>(Reuse.Singleton);
            r.Register<IMessageService,MessageManager>(Reuse.Singleton);
            r.Register<IMessageRepository,MessageDal>(Reuse.Singleton);
            
        }
    }
}
