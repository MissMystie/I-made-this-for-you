using System;
using UnityEngine;
using VInspector;

public class Mechanism : MonoBehaviour
{
    public Action<bool> onToggle;
    public Animator anim;
    public Mechanism input;
    public bool isOn;
    public string activateParam = "IsOn";

    public void Awake()
    {
        Activate(isOn);
    }

    void OnEnable()
    {
        if (input) input.onToggle += Activate;
    }

    void OnDisable()
    {
        if (input) input.onToggle -= Activate;
    }

    [Button()]
    public void Toggle()
    {
        Activate(!isOn);
    }

    public virtual void Activate(bool on)
    {
        isOn = on;
        anim.SetBool(activateParam, on);
        onToggle?.Invoke(isOn);
    }
}
