using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MoviePro.Services.Interfaces
{
    // The IImageService will used for storing and retrieving images from the database.  Our app will store images as a byte array in the database and not on disk. 
    public interface IImageService
    {
        // This method will take an image uploaded from a user, convert it to a byte array and save it to the database.
        Task<byte[]> EncodeImageAsync(IFormFile poster);

        // This method will take an image from a URL, convert it to a byte array and save it to the database.
        Task<byte[]> EncodeImageURLAsync(string imageURL);

        // This method will take a byte array from the database and construct a string that an image tag (<Img>) can use. 
        string DecodeImage(byte[] poster, string contentType);
    }
}
