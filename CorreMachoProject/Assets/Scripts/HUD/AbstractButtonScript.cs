using MigalhaSystem.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.HUD
{
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public abstract class AbstractButtonScript : MonoBehaviour
    {
        UnityEngine.UI.Button m_button;

        private void OnEnable()
        {
            m_button ??= gameObject.GetOrAdd<UnityEngine.UI.Button>();
            m_button.onClick.AddListener(Click);

        }

        private void OnDisable()
        {
            m_button.onClick.RemoveListener(Click);
        }

        protected abstract void Click();

        private void Reset()
        {
            TryGetComponent(out m_button);
        }
    }
}
