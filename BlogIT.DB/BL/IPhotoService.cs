using Microsoft.AspNetCore.Http;
using BlogIT.DB.Models;
using System.Threading.Tasks;

namespace BlogIT.DB.BL
{
    public interface IPhotoService
    {
        void UpdatePhoto(int fileID, IFormFile file);
        FileModel AddPhoto(IFormFile file);
        FileModel GetFileByID(int fileID);
        Task<string> GetPathAvatarByUserId(string userId);
        void DeletePhotoFromUser(User user);
    }
}
