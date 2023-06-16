using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Audio;

namespace Managers
{
    class SoundManager : MonoSingleton<SoundManager>
    {
        public AudioMixer audioMixer;
        public AudioSource musicAudioSource;
        public AudioSource soundAudioSource;

        const string MusicPath = "Music/";
        const string SoundPath = "Sound/";

        private bool musicOn;
        public bool MusicOn
        {
            get { return musicOn; }
            set
            {
                musicOn = value;
                this.MusicMute(!musicOn);
            }
        }

        private bool soundOn;
        public bool SoundOn
        {
            get { return soundOn; }
            set
            {
                soundOn = value;
                this.SoundMute(!soundOn);
            }
        }

        private int musicVolume;
        public int MusicVolume
        {
            get { return musicVolume; }
            set
            {
                if (musicVolume != value)
                {
                    musicVolume = value;
                    if (musicOn) this.SetVolume("MusicVolume", musicVolume);
                }
            }
        }

        private int soundVolume;
        public int SoundVolume
        {
            get { return soundVolume; }
            set
            {
                if (soundVolume != value)
                {
                    soundVolume = value;
                    if (soundOn) this.SetVolume("SoundVolume", soundVolume);
                }
            }
        }

        void Start()
        {
            this.MusicVolume = Config.MusicVolume;
            this.soundVolume = Config.SoundVolume;
            this.MusicOn = Config.MusicOn;
            this.soundOn = Config.SoundOn;
        }

        public void MusicMute(bool mute)
        {
            this.SetVolume("MusicVolume", mute ? 0 : musicVolume);
        }
        public void SoundMute(bool mute)
        {
            this.SetVolume("SoundVolume", mute ? 0 : soundVolume);
        }

        private void SetVolume(string name, int value)
        {
            float volume = value * 0.5f - 50f; //分贝计算
            this.audioMixer.SetFloat(name, volume);
        }

        public void PlayMusic(string name)
        {
            AudioClip clip = Resloader.Load<AudioClip>(MusicPath + name);
            if (clip == null)
            {
                Debug.LogWarningFormat("PlayMusic:{0} not existed.", name);
                return;
            }

            if (musicAudioSource.isPlaying)
            {
                musicAudioSource.Stop();
            }
            Debug.Log("Start Play Music:" + name);
            musicAudioSource.clip = clip;
            musicAudioSource.Play();
        }

        public void PlaySound(string name)
        {
            AudioClip clip = Resloader.Load<AudioClip>(SoundPath + name);
            if (clip == null)
            {
                Debug.LogWarningFormat("PlaySound:{0} not existed.", name);
                return;
            }
            Debug.Log("Start Play Sound:"+ name);
            soundAudioSource.PlayOneShot(clip);
        }

        protected void PlayClipOnAudioSource(AudioSource source, AudioClip clip, bool isLoop)
        {

        }
    }
}
