using Core.DataAccess;
using Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
  public interface IPhotoDal: IEntityRepository<Photo>
    {
    }
}
