using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitites
{
   public  class User:IEntity
    {
        public int Id { get; set; }
        public string Username{ get; set; }

        public ICollection<Photo> Photos { get; set; }

        public User()
        {
            Photos = new List<Photo>();
        }
    }
}
