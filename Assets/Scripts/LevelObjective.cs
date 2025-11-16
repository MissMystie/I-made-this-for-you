using Mystie.Core;
using UnityEngine;

public class LevelObjective : Mechanism
{
    public override void Activate(bool on, bool init = false)
    {
        if (on)
        {
            base.Activate(on);
            if (!init) LevelManager.Instance.CompleteLevel();
        }
    }
}
