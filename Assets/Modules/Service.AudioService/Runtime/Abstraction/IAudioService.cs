using UnityEngine.Audio;

namespace RpDev.Services.AudioService
{
    public interface IAudioService
    {
        void AddAudioMixer(AudioMixer audioMixer);
        void AddAudioClipPacks(AudioClipPack[] audioClipPacks);
        void Init();
        void EnableSound(bool state);
        void EnableUISounds(bool state);
        void EnableSfx(bool state);
        void EnableMusic(bool state);
        void EnableChannel(AudioClipType channel, bool status);
    }
}