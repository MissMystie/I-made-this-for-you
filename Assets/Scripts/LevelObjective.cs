using Mystie.Core;
using UnityEngine;

public class LevelObjective : Mechanism
{
    public override void Activate(bool on)
    {
        if (on)
        {
            base.Activate(on);
            LevelManager.Instance.CompleteLevel();
        }
    }
}
