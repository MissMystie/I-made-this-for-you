using Mystie.Core;
using UnityEngine;

public class LevelObjective : Mechanism
{
    public override void Activate(bool on)
    {
        base.Activate(on);
        LevelManager.Instance.CompleteLevel();
    }
}
