using Game.GameSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.HUD
{
    public class LoadSceneButton : AbstractButtonScript
    {
        [SerializeField, Min(0)] int m_sceneIndex; 
        protected override void Click()
        {
            GameScenesManager.ProvideInstance().LoadSingleScene(m_sceneIndex);
        }
    }
}
