using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Newtonsoft.Json;
using System.Net;
using Shared.Models;
using Shared.Business;

namespace RandomJokes
{
    [Activity(Label = "RandomJokes", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        ListView _jokesListView;
        FavoriteService _favoriteService;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Initializing services
            _favoriteService = new FavoriteService();

            // Load the Main.axml into this Activity
            SetContentView(Resource.Layout.Main);

            // Store the ListView inside a class attribute for reuse
            _jokesListView = this.FindViewById<ListView>(Resource.Id.jokesListView);
            // Subscribe to the Item Click event to handle adding a joke to favorites
            _jokesListView.ItemClick += JokeItemClicked;

            // Get the refresh button to subscribe to the click event and refresh the jokes
            var refreshButton = this.FindViewById<Button>(Resource.Id.refreshButton);
            refreshButton.Click += Refresh;
            refreshButton.Text = "Loaded";

            // Get the favorites button to subscribe to the click event and go to the favorites activity
            var favoritesButton = this.FindViewById<Button>(Resource.Id.favoritesButton);
            favoritesButton.Click += FavoritesButton_Click;
        }

        /// <summary>
        /// Method called when the favorite button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FavoritesButton_Click(object sender, EventArgs e)
        {
            // Create the intent that will load the FavoritesActivity
            Intent myIntent = new Intent(this, typeof(FavoritesActivity));

            // Start the favorites activity
            this.StartActivity(myIntent);
        }

        /// <summary>
        /// Method called when an item is clicked in the joke ListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JokeItemClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            // Get the clicked joke from the Adapter
            var jokeAdapter = _jokesListView.Adapter as BaseAdapter<Joke>;
            var clickedJoke = jokeAdapter[e.Position] as Joke;

            // Add the joke through the service (may be a database)
            _favoriteService.AddJoke(clickedJoke);

            // Refresh the jokes from the web service
            DownloadJokesAndUpdateListView();
        }

        /// <summary>
        /// Update the joke list fetching data from the service
        /// </summary>
        public void DownloadJokesAndUpdateListView()
        {
            // Get the JSON from the service
            var wc = new WebClient();
            var jsonResponse = wc.DownloadString("http://randomjoke.azurewebsites.net/api/jokes");
            wc.Dispose();

            // Deserialize the JSON response to C# objects
            ApiResponse response = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse);

            // Create the adapter with new jokes and affect it to the ListView
            JokeAdapter adapter = new JokeAdapter(this, response.value);
            _jokesListView.Adapter = adapter;

            // Ensure the visual list is updated by notifying the interface
            adapter.NotifyDataSetChanged();
        }

        /// <summary>
        /// Method called when 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refresh(object sender, EventArgs e)
        {
            DownloadJokesAndUpdateListView();
        }
    }
}

