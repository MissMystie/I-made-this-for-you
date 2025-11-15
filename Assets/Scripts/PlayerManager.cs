using System.Collections.Generic;
using UnityEngine;
using VInspector;

public class PlayerManager : MonoBehaviour
{
    public List<CharacterController> characters;

    public int characterIndex = 0;

    public void Start()
    {
        ChangeCharacter(0);
    }

    [Button]
    public void ChangeCharacter()
    {
        int i = characterIndex + 1;
        if (i >= characters.Count) {
            i = 0;
        }

        ChangeCharacter(i);
    }

    public void ChangeCharacter(int i)
    {
        characters[characterIndex].DisableInputs();

        characterIndex = i;

        characters[characterIndex].EnableInputs();
    }
}
