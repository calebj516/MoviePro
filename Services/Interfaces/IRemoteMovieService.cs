using MoviePro.Enums;
using MoviePro.Models.TMDB;
using System.Threading.Tasks;

namespace MoviePro.Services.Interfaces
{
    public interface IRemoteMovieService
    {
        // A query to the TMDB API. the ID is the TMDB movie api. Returns the MovieDetail object for the Movie. 
        Task<MovieDetail> MovieDetailAsync(int id);

        // A search to the TMDB API returns a MovieSearch object.
        Task<MovieSearch> SearchMoviesAsync(MovieCategory category, int count);

        // A query to the TMDB API. The Id is the TMDB actor Id. Returns the ActorDetail object for the Actor.  
        Task<ActorDetail> ActorDetailAsync(int id);

    }
}
