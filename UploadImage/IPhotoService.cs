using Entitites.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IPhotoService
    {
        Task<PhotoForReturn> AddPhotoForUser(int userId, PhotoForCreation photoDto);
        Task<bool> SaveAll();
    }
}
