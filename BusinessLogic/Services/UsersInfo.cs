using BusinessLogic.Entities;
using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{

    public class UsersInfo : IUsersInfo
    {
        private readonly IRepository<UsersEntity, int> _UsersEntity;
         public UsersInfo ( IRepository<UsersEntity, int> usersEntity)
         {
           
            this._UsersEntity = usersEntity;
          
         }

         public UsersEntity SaveUsers(UsersEntity entity)
         {
             this._UsersEntity.SaveOrUpdate(entity);
             return entity;
         }
    }
}
