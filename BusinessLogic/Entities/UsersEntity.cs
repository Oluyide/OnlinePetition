using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
   public class UsersEntity : BaseEntity<int>
    {
        public virtual int UserId { get; set; }
        public virtual string Email { get; set; }
        public virtual string Surname { get; set; }
        public virtual string   FirstName { get; set; }
        public virtual string MiddleName { get; set; }


    }
   public class UsersEnitityMap : ClassMapping<UsersEntity>
   {
       public UsersEnitityMap()
       {
           Table("UsersInfo");
           Id<int>(x => x.UserId, m => { m.Column("UserId"); m.Generator(Generators.Native); });
           Property<string>(x => x.Email, m => { m.Column("Email"); });
           Property<string>(x => x.Surname, m => { m.Column("Surname"); });
           Property<string>(x => x.FirstName, m => { m.Column("FirstName"); });
           Property<string>(x => x.MiddleName, m => { m.Column("MiddleName"); });

          
       }
   }
}
