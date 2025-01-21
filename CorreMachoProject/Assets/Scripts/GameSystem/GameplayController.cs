using Game.Player;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Game.GameSystem
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] GameObject m_gameplayCanvas;
        [SerializeField] GameObject m_pauseCanvas;

        int m_currentPoints;
        private void OnEnable()
        {
            PlayerHealthObserver.OnPlayerDie += LoadDeathScreen;
            GameSettingsObserver.OnPauseButtonClick += Pause;
            InputObserver.OnEscape += PauseInput;
            PlayerPointsObserver.OnUpdatePoints += SetPoints;
        }

        private void OnDisable()
        {
            PlayerHealthObserver.OnPlayerDie -= LoadDeathScreen;
            GameSettingsObserver.OnPauseButtonClick -= Pause;
            InputObserver.OnEscape -= PauseInput;
            PlayerPointsObserver.OnUpdatePoints -= SetPoints;
        }

        void SetPoints(int newValue)
        {
            m_currentPoints = newValue;
        }

        private void Awake()
        {
            m_pauseCanvas.SetActive(false);
            m_gameplayCanvas.SetActive(true);
        }

        void LoadDeathScreen()
        {
            GameManager.ProvideInstance().PauseGame(false);
            GameScenesManager gameScene = GameScenesManager.ProvideInstance();
            gameScene.LoadSingleSceneWithData(gameScene.DeathScreenScene, m_currentPoints);
        }

        void PauseInput(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            GameSettingsObserver.PauseButtonClick();
        }

        void Pause()
        {
            bool paused = GameManager.ProvideInstance().m_GamePaused;
            m_pauseCanvas.SetActive(paused);
            m_gameplayCanvas.SetActive(!paused);
        }
    }
}
