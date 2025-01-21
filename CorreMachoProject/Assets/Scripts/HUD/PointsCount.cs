using MigalhaSystem.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.HUD
{
    [RequireComponent(typeof(TMPro.TextMeshProUGUI))]
    public class PointsCount : MonoBehaviour
    {
        TMPro.TextMeshProUGUI m_pointsTMPro;

        private void Awake()
        {
            m_pointsTMPro ??= gameObject.GetOrAdd<TMPro.TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            PlayerPointsObserver.OnUpdatePoints += UpdatePoints;
        }

        private void OnDisable()
        {
            PlayerPointsObserver.OnUpdatePoints -= UpdatePoints;
        }

        void UpdatePoints(int newPointValue)
        {
            if (!m_pointsTMPro)
            {
                m_pointsTMPro = gameObject.GetOrAdd<TMPro.TextMeshProUGUI>();
                if (!m_pointsTMPro) return;
            }
            m_pointsTMPro.text = $"{newPointValue}m";
        }

        private void Reset()
        {
            m_pointsTMPro ??= gameObject.GetOrAdd<TMPro.TextMeshProUGUI>();
        }
    }
}
