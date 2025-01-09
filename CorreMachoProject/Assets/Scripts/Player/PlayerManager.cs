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

        private void Reset()
        {
            m_PlayerMove = GetComponent<PlayerMove>();
            m_PlayerHealth = GetComponent<PlayerHealth>();
        }
    }
}
