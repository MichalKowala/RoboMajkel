using Discord.Commands;
using System.Threading.Tasks;
using System.IO;
using System;
using Google.Cloud.TextToSpeech.V1;
using Google.Apis.Auth.OAuth2;
using Discord;

namespace RoboMajkel.Modules
{
    public class GeneralModule : ModuleBase<SocketCommandContext>
    {
        [Command("tts")]
        [Summary("Use text to speech to read a message")]

        public async Task Tts(string message)
        {
            var client = TextToSpeechClient.Create();
            
            var input = new SynthesisInput
            {
                Text = message.ToLower()
            };
            var voiceSelection = new VoiceSelectionParams
            {
                LanguageCode = "pl-PL",
                Name= "pl-PL-Standard-C",
                SsmlGender = SsmlVoiceGender.Female
            };
            var audioConfig = new AudioConfig
            {
                AudioEncoding = AudioEncoding.Mp3,
                SpeakingRate=0.85
                
            };
            var response = client.SynthesizeSpeech(input, voiceSelection, audioConfig);
            using (var output=File.Create(@"d:\mmmmm.mp3"))
            {
                response.AudioContent.WriteTo(output);
            }
        }

        [Command("join", RunMode = RunMode.Async)]
        public async Task JoinChannel(IVoiceChannel channel = null)
        {
            // Get the audio channel
            
            channel = channel ?? (Context.User as IGuildUser)?.VoiceChannel;
            if (channel == null) { await Context.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument."); return; }

            var audioClient = await channel.ConnectAsync();
            Console.WriteLine(channel.Name);
        }
      
    }
}
