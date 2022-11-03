using Microsoft.AspNetCore.Mvc.RazorPages;
using MoviePro.Models.Database;
using MoviePro.Models.TMDB;
using System.Collections.Generic;

namespace MoviePro.Models.ViewModels

    // Landing Page VM is an aggregate of the MovieSearch and Collection Models, and will not be represented as a table in the database
{
    public class LandingPageVM
    {
        public List<Collection> CustomCollections { get; set; }

        // The app landing page will generate four queries to the TMDBApi. Our MovieSearch model can hold each instance of these searches.
        // The landing page will display a list of movies in four categories: Now Playing, Popular, Top Rated, and Upcoming.
        public MovieSearch NowPlaying { get; set; }
        public MovieSearch Popular { get; set; }
        public MovieSearch TopRated { get; set; }
        public MovieSearch Upcoming { get; set; }
    }
}
