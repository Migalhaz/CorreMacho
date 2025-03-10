using MigalhaSystem.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystem
{
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] List<AudioSource> m_musics;
        int m_currentMusicIndex;

        private void OnEnable()
        {
            GameManagerStaticObserver.OnSetAudioValue += SetVolume;
        }

        private void OnDisable()
        {
            GameManagerStaticObserver.OnSetAudioValue -= SetVolume;

        }

        private void Start()
        {
            m_currentMusicIndex = Random.Range(0, m_musics.Count);
            GetCurrentMusic().Play();
            StartCoroutine(MusicUpdate());
        }

        void SetVolume(bool enable)
        {
            float volume = enable ? 1.0f : 0.0f;

            foreach (var music in m_musics)
            {
                music.volume = volume;
            }
        }

        IEnumerator MusicUpdate()
        {
            yield return new WaitForSecondsRealtime(GetCurrentMusic().clip.length);
            GetCurrentMusic().Stop();
            NextSong();
            StartCoroutine(MusicUpdate());
        }

        public AudioSource GetCurrentMusic()
        {
            return m_musics[m_currentMusicIndex];
        }

        void NextSong()
        {
            GetCurrentMusic().Stop();
            m_currentMusicIndex++;
            if (m_currentMusicIndex >= m_musics.Count)
            {
                m_currentMusicIndex = 0;
            }
            GetCurrentMusic().Play();
        }
    }
}
