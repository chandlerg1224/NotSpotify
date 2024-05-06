﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NotSpotify.MVVM.Model.QuickType;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Authenticators.OAuth2;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace NotSpotify.MVVM.ViewModel
{
    internal class MainViewModel
    {
        public ObservableCollection<Item> Songs { get; set; }
        public MainViewModel()
        {
            Songs = new ObservableCollection<Item>();
            PopulateCollection();
        }

        async void PopulateCollection()
        {

            var authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(
                "BQDmYwLyG-qz6jZXQjfPc2dGAJzsNRdQ6w-3EA1V2oZ6BoPNiat2aFT0lwJi6QhJVSXnLkGcotySS2cP_TyesSgIhAFrFX21sWS31mS-QDB7RWtsNqY", "Bearer"
);
            var options = new RestClientOptions("https://api.spotify.com/v1")
            {
                Authenticator = authenticator
            };
            var client = new RestClient(options);

            var request = new RestRequest("https://api.spotify.com/v1/browse/new-releases", Method.Get);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");


            var response = await client.GetAsync(request);
            var data = JsonConvert.DeserializeObject<TrackModel>(response.Content);

            for(int i = 0; i < data.Albums.Limit; i++)
            {
                var track = data.Albums.Items[i];
                track.Duration = "3:23";
                Songs.Add(track);
            }
        }
    }
}
