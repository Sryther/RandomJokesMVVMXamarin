using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Shared.Business
{
    public class FavoriteService
    {
        private static List<Joke> _jokes;

        static FavoriteService()
        {
            _jokes = new List<Joke>();
        }

        public void AddJoke(Joke jokeToFavorite)
        {            
            if (_jokes.All(j => j.id != jokeToFavorite.id))
            {
                _jokes.Add(jokeToFavorite);
            }
        }

        public void RemoveJoke(int idOfJokeToRemove)
        {
            var jokeToRemove = _jokes.FirstOrDefault(j => j.id == idOfJokeToRemove);
            if (jokeToRemove != null)
            {
                _jokes.Remove(jokeToRemove);
            }
        }

        public Joke[] GetAll()
        {
            return _jokes.ToArray();
        }
    }
}