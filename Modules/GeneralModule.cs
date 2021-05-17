using Discord.Commands;
using System.Threading.Tasks;
using System.IO;
using System;
using Google.Cloud.TextToSpeech.V1;
using Google.Apis.Auth.OAuth2;
using Discord;
using System.Diagnostics;
using Discord.Audio;
using System.Threading;
using RoboMajkel.Utilities;

namespace RoboMajkel.Modules
{
    public class GeneralModule : ModuleBase<SocketCommandContext>
    {
        [Command("tts", RunMode=RunMode.Async)]
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
            using (var output=File.Create(EnvVars.RoboMajkelTtsAudioFileLocation))
            {
                response.AudioContent.WriteTo(output);
            }

            IVoiceChannel channel = (Context.User as IGuildUser)?.VoiceChannel;
            var audioClient = await channel.ConnectAsync();
            await SendAsync(audioClient, EnvVars.RoboMajkelTtsAudioFileLocation);
        }

        private Process CreateStream(string path)
        {
            return Process.Start(new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $"-hide_banner -loglevel panic -i \"{path}\" -ac 2 -f s16le -ar 48000 pipe:1",
                UseShellExecute = false,
                RedirectStandardOutput = true,
            }); ;
        }

        private async Task SendAsync(IAudioClient client, string path)
        {
            using (var ffmpeg = CreateStream(path))
            using (var output = ffmpeg.StandardOutput.BaseStream)
            using (var discord = client.CreatePCMStream(AudioApplication.Mixed))
            {
                try { await output.CopyToAsync(discord); }
                finally { await discord.FlushAsync(); }
            }
        }
       
        
    }
}
