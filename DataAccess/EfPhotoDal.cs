using Core.DataAccess.EntityFramework;
using Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
   public class EfPhotoDal : EfEntityRepositoryBase<Photo, ProjectDbContext>, IPhotoDal
    {
    }
}
