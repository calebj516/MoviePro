using MoviePro.Models.Database;
using MoviePro.Models.TMDB;
using System.Threading.Tasks;

namespace MoviePro.Services.Interfaces
{
    public interface IDataMappingService
    {
        // When querying a movie in the TMDB API this method maps the properties coming from the TMDB API JSON to our Movie Model.
        Task<Movie> MapMovieDetailAsync(MovieDetail movie);

        // When querying an actor in the TMDB API this method maps the properties coming from the TMDB API JSON to our ActorDetail Model.
        ActorDetail MapActorDetail(ActorDetail actor);

        // Constructs a proper path to display cast Images.
        string BuildCastImage(string profilePath);
    }
}
