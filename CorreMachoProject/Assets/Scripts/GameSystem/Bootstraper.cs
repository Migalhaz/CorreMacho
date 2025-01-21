using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystem
{
    public class Bootstraper : MonoBehaviour
    {
        [SerializeField, Min(0)] int m_firstSceneIndex;

        private void Awake()
        {
            Bootstrap();
        }

        void Bootstrap()
        {
            InputManager.ProvideInstance();
            GameManager.ProvideInstance();
            GameScenesManager.ProvideInstance().LoadSingleScene(m_firstSceneIndex);
        }
    }
}
