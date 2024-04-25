using System.Collections.Generic;
using Lidgren.Network;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Chat_video_app.Classes.Voice
{
    internal class Audio
    {
        public static Dictionary<int, BufferedWaveProvider> Providers = new Dictionary<int, BufferedWaveProvider>();
        public static Dictionary<NetConnection, int> IDs = new Dictionary<NetConnection, int>();
        public static WaveOutEvent Output;
        public static WaveInEvent Input;
        public static MixingSampleProvider Mixer;
        public static bool Initialized = false;

        private static int maxID;
        const int SAMPLE_RATE = 44100;
        const int CHANNELS = 1;

        public static WaveFormat GetFormat()
        {
            return WaveFormat.CreateIeeeFloatWaveFormat(SAMPLE_RATE, CHANNELS);
        }

        public static void InitAll()
        {
            if (Initialized)
            {
                Forms.Room2_host.Log("[AUDIO] Already initialized!");
                return;
            }

            Input = new WaveInEvent();
            Input.WaveFormat = GetFormat();
            Input.BufferMilliseconds = 25;
            Input.DataAvailable += NewInputData;
            Input.StartRecording();

            Mixer = new MixingSampleProvider(GetFormat());
            Mixer.ReadFully = true;

            Output = new WaveOutEvent();
            Output.DesiredLatency = 60;
            Output.Init(Mixer);
            Output.Play();

            Initialized = true;
            Forms.Room2_host.Log("[AUDIO] Initialized.");
        }

        public static int GetOrCreateID(NetConnection n)
        {
            if (n == null)
                return -1;

            if (IDs.ContainsKey(n))
            {
                return IDs[n];
            }
            else
            {
                IDs.Add(n, maxID++);
                return maxID - 1;
            }
        }

        public static void QueuePlay(int key, byte[] bytes, int count)
        {
            if (!Providers.ContainsKey(key))
            {
                Forms.Room2_host.Log("Creating new Buffered Wave Provider for key '" + key + "'");
                BufferedWaveProvider bufferedWave = new BufferedWaveProvider(GetFormat());
                bufferedWave.BufferLength = (32 / 8) * SAMPLE_RATE * CHANNELS * 5; // 5 seconds of buffer.
                Mixer.AddMixerInput(bufferedWave);
                Providers[key] = bufferedWave;

            }
            Providers[key].AddSamples(bytes, 0, count);
            //Forms.Room2_host.Log("Buffer @ " + Math.Round((float)Providers[key].BufferedBytes / Providers[key].BufferLength * 100f) + "%");
        }

        public static void ClearAll()
        {
            if (!Initialized)
            {
                Forms.Room2_host.Log("[AUDIO] Not initialized, cannot clear all!");
                return;
            }

            foreach (var item in Providers)
            {
                item.Value.ClearBuffer();
            }
            Providers.Clear();
            IDs.Clear();
            Mixer.RemoveAllMixerInputs();
            Output.Stop();
            Input.StopRecording();

            Mixer = null;
            Output = null;
            Input = null;
            maxID = 0;

            Initialized = false;
            Forms.Room2_host.Log("[AUDIO] Cleared all data, reset.");
        }

        private static void NewInputData(object sender, WaveInEventArgs e)
        {
            if (Net.IsConnected)
            {
                // Client...
                Net.Client.SendAudio(e.Buffer, e.BytesRecorded);
            }
            else if (Net.IsServer)
            {
                // Server...
                Net.Server.SendAudio(e.Buffer, e.BytesRecorded);
            }
        }
    }
}
