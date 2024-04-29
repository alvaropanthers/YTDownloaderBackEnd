using CSCore;
using CSCore.Codecs;
using CSCore.SoundOut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP4Player
{
    public class AudioPlayer
    {
        ISoundOut _soundOut;

        public IWaveSource? GetWave() 
        {
            if (_soundOut == null) return null;
            return _soundOut.WaveSource;
        }

        public PlaybackState? GetCurrentState()
        {
            if (_soundOut == null) return null;
            
            return _soundOut.PlaybackState;
        }

        public void PlayAudioFile(string path)
        {
            if (_soundOut != null) _soundOut.Stop();

            var _waveSource =
                CodecFactory.Instance.GetCodec(path)
                    .ToSampleSource()
                    .ToMono()
                    .ToWaveSource();

            _soundOut = new WasapiOut() { Latency = 100 };
            _soundOut.Initialize(_waveSource);

            if (_soundOut != null) _soundOut.Play();
        }

        public void StopAudioFile()
        {
            _soundOut?.Stop();
        }

        public void ResumeAudioFile()
        {
            _soundOut?.Resume();
        }

        public void PauseAudioFile()
        {
            _soundOut?.Pause();
        }
    }
}
