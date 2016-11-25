using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Shared.Business;

namespace RandomJokes
{
    [Activity(Label = "FavoritesActivity")]
    public class FavoritesActivity : Activity
    {
        FavoriteService _favoriteService;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _favoriteService = new FavoriteService();

            SetContentView(Resource.Layout.Favorites);

            var jokesList = FindViewById<ListView>(Resource.Id.jokesListView);
            JokeAdapter adapter = new JokeAdapter(this, _favoriteService.GetAll().ToArray());
            jokesList.Adapter = adapter;
            jokesList.ItemClick += FavoriteClicked;
        }

        private void FavoriteClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            var jokeAdapter = (sender as ListView).Adapter as JokeAdapter;
            var jokeId = (int)jokeAdapter.GetItemId(e.Position);
            _favoriteService.RemoveJoke(jokeId);
            jokeAdapter.RemoveJokeById(jokeId);
        }
    }
}