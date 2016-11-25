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
using Shared.Models;

namespace RandomJokes
{
    public class JokeAdapter : BaseAdapter<Joke>
    {
        List<Joke> _items;
        Activity _context;

        public JokeAdapter(Activity context, Joke[] items) : base()
        {
            this._context = context;
            this._items = new List<Joke>(items);
        }

        public override long GetItemId(int position)
        {
            return _items[position].id;
        }

        public override Joke this[int position]
        {
            get { return _items[position]; }
        }

        public override int Count
        {
            get { return _items.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            if (view == null)
                view = _context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);

            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = _items[position].id.ToString();
            view.FindViewById<TextView>(Android.Resource.Id.Text2).Text = _items[position].joke;

            return view;
        }

        public void RemoveJokeById(int id)
        {
            var jokeToRemove = _items.FirstOrDefault(joke => joke.id == id);
            _items.Remove(jokeToRemove);

            NotifyDataSetChanged();
        }
    }
}