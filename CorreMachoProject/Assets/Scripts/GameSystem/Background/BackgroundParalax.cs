using MigalhaSystem.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystem
{
    public class BackgroundParalax : MonoBehaviour
    {
        [SerializeField] List<Background> m_backgrounds;
        Background m_currentBackground;
        void Awake()
        {
            m_currentBackground = m_backgrounds.GetRandom();

            m_currentBackground.DoSpawnObjects();
        }

        // Update is called once per frame
        void Update()
        {
            m_currentBackground.MoveObjects();
        }
    }
}
