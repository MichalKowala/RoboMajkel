using Discord.Commands;
using Newtonsoft.Json.Linq;
using RoboMajkel.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RoboMajkel.Modules
{
    public class ArtosisModule : ModuleBase<SocketCommandContext>
    {
        [Command("artoclip")]
        [Summary("Plays random artosis clip")]
        public async Task ArtoClip()
        {
            var clip = await GetArtoclipUrl();
            await Context.Channel.SendMessageAsync(clip);
        }
        private async Task<string> GetArtoclipUrl()
        {
            string ArtosisStarcraftClipsChannelId = "UC6p7RWdPkmGkr-Ll5IdRbYg";
            int resultsPerPage = 50; // 0-50
            string query = $"https://youtube.googleapis.com/youtube/v3/search?part=snippet&channelId={ArtosisStarcraftClipsChannelId}&q=artosis&maxResults={resultsPerPage}&key={EnvVars.RoboMajkelYTApiKeyBackup3}";
            List<string> results = new List<string>();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            JToken nextPageToken = null;
            Task<string> stringTask;
            do
            {
                if (nextPageToken == null)
                    stringTask = client.GetStringAsync(query);
                else
                    stringTask = client.GetStringAsync(query + $"&pageToken={nextPageToken}");

                var msg = await stringTask;
                JObject json = JObject.Parse(msg);
                nextPageToken = json["nextPageToken"];
                foreach (var x in json["items"])
                {
                    string videoId = (string)x["id"]["videoId"];
                    results.Add(videoId);
                }
            } while (nextPageToken != null);
            var rand = new Random();
            return "https://www.youtube.com/watch?v=" + results.ElementAt(rand.Next(results.Count()));
        }
    }
}
