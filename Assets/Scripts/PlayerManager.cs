using System.Collections.Generic;
using UnityEngine;
using VInspector;
using static UnityEngine.InputSystem.InputAction;
using Unity.Cinemachine;
using FMOD.Studio;

public class PlayerManager : MonoBehaviour
{
    public CinemachineCamera vcam;
    public Controls controls;
    public List<CharacterController> characters;
    bool canChangeCharacter = true;

    public int characterIndex = 0;

    public void Awake()
    {
        controls = new Controls();
        controls.Enable();
    }

    public void Start()
    {
        ChangeCharacter(0);

        controls.Player.Switch.performed += ChangeCharacter;

        foreach (CharacterController character in characters)
        {
            character.health.onDeath += DisableCharacterChange;
            character.health.onRespawn += EnableCharacterChange;
        }
    }

    public void OnDestroy()
    {
        controls.Player.Switch.performed -= ChangeCharacter;

        foreach (CharacterController character in characters)
        {
            character.health.onDeath -= DisableCharacterChange;
            character.health.onRespawn -= EnableCharacterChange;
        }
    }

    public void EnableCharacterChange()
    {
        canChangeCharacter = true;
    }

    public void DisableCharacterChange()
    {
        canChangeCharacter = false;
    }

    [Button]
    public void ChangeCharacter(CallbackContext ctx = default)
    {
        if (!canChangeCharacter) return;

        int i = characterIndex + 1;
        if (i >= characters.Count)
        {
            i = 0;
        }

        ChangeCharacter(i);
    }

    public void ChangeCharacter(int i)
    {
        characters[characterIndex].DisableInputs(controls);

        characterIndex = i;
        vcam.Follow = characters[characterIndex].transform;

        characters[characterIndex].EnableInputs(controls);
    }
}
