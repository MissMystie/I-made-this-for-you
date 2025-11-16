using Mystie.Core;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public void NextLevelButton()
    {
        LevelManager.Instance.NextLevel();
    }
}
