using MigalhaSystem.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        [field: SerializeField] public PlayerMove m_PlayerMove { get; private set; }
        [field: SerializeField] public PlayerHealth m_PlayerHealth { get; private set; }
        [field: SerializeField] public PlayerPoints m_PlayerPoints { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            m_PlayerMove ??= GetComponent<PlayerMove>();
            m_PlayerHealth ??= GetComponent<PlayerHealth>();
            m_PlayerPoints ??= GetComponent<PlayerPoints>();
        }

        private void Reset()
        {
            m_PlayerMove = GetComponent<PlayerMove>();
            m_PlayerHealth = GetComponent<PlayerHealth>();
            m_PlayerPoints = GetComponent<PlayerPoints>();
        }
    }
}
