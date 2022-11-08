using Microsoft.AspNetCore.Http;
using MoviePro.Services.Interfaces;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace MoviePro.Services
{
    public class BasicImageService : IImageService
    {
        private readonly IHttpClientFactory _httpClient;

        public BasicImageService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        // This method will be used to retrieve image data from the database (the image data in the database is stored as a byte array) 
        public string DecodeImage(byte[] poster, string contentType)
        {
            if (poster == null) return null;

            // The byte array data is converted to base64 string, and then concatenated into a string the src attribute of an image tag can understand.
            var posterImage = Convert.ToBase64String(poster);
            return $"data:{contentType};base64,{posterImage}";
        }

        // This method will be used to allow a user to upload or change an image for a movie that has been saved to the database.
        // The IFromFile parameter is the uploaded Image information from the form.
        public async Task<byte[]> EncodeImageAsync(IFormFile poster)
        {
            if (poster == null) return null;

            using var ms = new MemoryStream();
            await poster.CopyToAsync(ms);
            return ms.ToArray();
        }

        // This method will take in a URL from the TMDB API and convert the image to a byte array. This is done so that we can save the image to the database.
        // The parameter imageURL is the remote location of the hosted image by TMDB API. 
        public async Task<byte[]> EncodeImageURLAsync(string imageURL)
        {
            var client = _httpClient.CreateClient();
            var response = await client.GetAsync(imageURL);
            using Stream stream = await response.Content.ReadAsStreamAsync();

            var ms = new MemoryStream();
            await stream.CopyToAsync(ms);

            return ms.ToArray();
        }
    }
}
