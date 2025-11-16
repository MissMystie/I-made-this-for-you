using Mystie.Core;
using System.Collections;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public bool autoTransition;
    public float delay = 11f;

    public IEnumerator Start()
    {
        if (autoTransition) yield return new WaitForSeconds(delay);
        else yield break;

        NextLevelButton();
    }
    public void NextLevelButton()
    {
        LevelManager.Instance.NextLevel();
    }
}
