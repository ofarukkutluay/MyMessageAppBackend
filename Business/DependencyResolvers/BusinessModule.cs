using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.Concretes;
using Core.Utilities.IoC;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace Business.DependencyResolvers
{
    public class BusinessModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            //serviceCollection.AddSingleton<IUserOperationClaimService, UserOperationClaimManager>();
            //serviceCollection.AddSingleton<IUserOperationClaimRepository, MongoDbUserOperationClaimDal>();
            //serviceCollection.AddSingleton<IOperationClaimService, OperationClaimManager>();
            //serviceCollection.AddSingleton<IOperationClaimRepository, MongoDbOperationClaimDal>();
        }
    }
}
