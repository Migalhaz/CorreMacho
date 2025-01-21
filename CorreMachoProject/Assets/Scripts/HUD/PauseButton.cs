using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.HUD
{
    public class PauseButton : AbstractButtonScript
    {
        protected override void Click()
        {
            GameSettingsObserver.PauseButtonClick();
        }
    }
}
