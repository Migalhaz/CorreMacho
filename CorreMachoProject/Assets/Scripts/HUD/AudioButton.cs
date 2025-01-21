using MigalhaSystem.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.HUD
{
    public class AudioButton : AbstractButtonScript
    {
        protected override void Click()
        {
            GameSettingsObserver.AudioButtonClick();
        }
    }
}
