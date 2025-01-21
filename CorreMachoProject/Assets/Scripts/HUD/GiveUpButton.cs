using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.HUD
{
    public class GiveUpButton : AbstractButtonScript
    {
        protected override void Click()
        {
            PlayerHealthObserver.PlayerDie();
        }
    }
}
