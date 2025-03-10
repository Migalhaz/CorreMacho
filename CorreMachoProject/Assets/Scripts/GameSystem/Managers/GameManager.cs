using MigalhaSystem.Singleton;
using UnityEngine;

namespace Game
{
	public class GameManager : Singleton<GameManager>
	{
        public bool m_GamePaused { get; private set; }
        public bool m_AudioEnabled { get; private set; }
        const string AUDIOKEY = "Audio";

        private void OnEnable()
        {
            GameSettingsObserver.OnAudioButtonClick += SwitchAudioSettings;
            GameSettingsObserver.OnPauseButtonClick += SwitchPause;
        }

        private void OnDisable()
        {
            GameSettingsObserver.OnAudioButtonClick -= SwitchAudioSettings;
            GameSettingsObserver.OnPauseButtonClick -= SwitchPause;
        }

        private void Start()
        {
            EnableAudio(GetAudioEnableFromPlayerPrefs());
            PauseGame(false);
        }

        bool GetAudioEnableFromPlayerPrefs()
        {
            int trueValue = 1;
            int defaultValue = trueValue;
            int value = PlayerPrefs.GetInt(AUDIOKEY, defaultValue);
            return value == trueValue;
        }

        public void PauseGame(bool pause)
        {
            m_GamePaused = pause;
            float timeScale = pause ? 0f : 1f;
            Time.timeScale = timeScale;
        }

        public void EnableAudio(bool enabled)
        {
            m_AudioEnabled = enabled;
            int value = enabled ? 1 : 0;
            PlayerPrefs.SetInt(AUDIOKEY, value);
            GameManagerStaticObserver.SetAudioValue(m_AudioEnabled);
        }

        void SwitchPause()
        {
            PauseGame(!m_GamePaused);
        }

        void SwitchAudioSettings()
        {
            EnableAudio(!m_AudioEnabled);
        }
    }

    public static class GameManagerStaticObserver
    {
        public static event System.Action<bool> OnSetAudioValue;
        public static void SetAudioValue(bool newValue) => OnSetAudioValue?.Invoke(newValue);
    }
}