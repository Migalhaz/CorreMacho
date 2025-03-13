using Game.GameSystem;
using MigalhaSystem.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.HUD
{
    public class AudioButton : AbstractButtonScript
    {
        [SerializeField] UnityEngine.UI.Image m_buttonImageComponent;
        [SerializeField] Sprite m_enabledAudioSprite;
        [SerializeField] Sprite m_disabledAudioSprite;


        protected override void OnEnable()
        {
            base.OnEnable();
            GameManagerStaticObserver.OnSetAudioValue += SetSprite;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            GameManagerStaticObserver.OnSetAudioValue -= SetSprite;

        }

        private void Start()
        {
            SetSprite(GameManager.ProvideInstance().m_AudioEnabled);
        }

        protected override void Click()
        {
            GameSettingsObserver.AudioButtonClick();
        }

        void SetSprite(bool audioEnabled)
        {
            if (audioEnabled)
            {
                m_buttonImageComponent.sprite = m_disabledAudioSprite;
            }
            else
            {
                m_buttonImageComponent.sprite = m_enabledAudioSprite;
            }
        }
    }
}
