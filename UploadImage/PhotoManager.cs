using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core;
using DataAccess;
using Entitites;
using Entitites.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class PhotoManager:IPhotoService
    {
        //public IConfiguration Configuration { get; }
       // private CloudinarySettings _cloudinarySettings;
        private readonly IUserService _userService;
        //private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;
        private readonly ProjectDbContext _context;

        //public PhotoManager(IUserService userService,
        //    IOptions<CloudinarySettings> cloudinaryConfig,
        //  ProjectDbContext context)
        //{
        //    _context = context;
        //    _userService = userService;
        //    _cloudinaryConfig = cloudinaryConfig;

        //    Account account = new Account(
        //        _cloudinaryConfig.Value.CloudName,
        //        _cloudinaryConfig.Value.ApiKey,
        //        _cloudinaryConfig.Value.ApiSecret
        //    );

        //    _cloudinary = new Cloudinary(account);
        //}

        public PhotoManager(IConfiguration configuration,IUserService userService,ProjectDbContext context)
        {
            //Configuration = configuration;
            //_cloudinarySettings = Configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
            _context = context;
            _userService = userService;
         

            Account account = new Account("deno",
                "915681628234541",
                "Piv97Lh-yPb7HOXyGsDxKhX546g"
                );

            _cloudinary = new Cloudinary(account);
        }
        public async Task<PhotoForReturn> AddPhotoForUser(int userId, PhotoForCreation photoDto)
        {
            var user = await _context                                   //  1.
                        .Users
                        .Where(u => u.Id == userId)
                        .FirstOrDefaultAsync();

            var file = photoDto.File;                                   //  2.

            var uploadResult = new ImageUploadResult();                 //  3.

            if (file.Length > 0)                                        //  4.
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream)
                        //Transformation = new Transformation()           //  *
                        //                .Width(500).Height(500)
                        //                .Crop("fill")
                        //                .Gravity("face")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);    //  5.
                }
            }

            photoDto.Url = uploadResult.Uri.ToString();                 //  4. (cont'd)
            photoDto.PublicId = uploadResult.PublicId;                  //  4. (cont'd)

            var photo = new Photo                                       //  6.
            {
                Url = photoDto.Url,
                Description = "",
                DateAdded = photoDto.DateAdded,
                PublicId = photoDto.PublicId,
                User = user
            };

            //if (!photo.User.Photos.Any(m => m.IsMain))                  //  7.
                photo.IsMain = true;

            user.Photos.Add(photo);
            await SaveAll();                                            //  8.

            return new PhotoForReturn                                   //  9.
            {
                Id = photo.Id,
                Url = photo.Url,
                Description = photo.Description,
                DateAdded = photo.DateAdded,
                IsMain = photo.IsMain,
                PublicId = photo.PublicId,
            };
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
