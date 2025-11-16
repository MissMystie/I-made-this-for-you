using System;
using System.Collections.Generic;
using UnityEngine;
using VInspector;
using static HealthManager;

public class Mechanism : MonoBehaviour
{
    public Action<bool> onToggle;
    public Animator anim;
    public Mechanism input;
    public bool isOn;
    public string activateParam = "IsOn";

    public void Awake()
    {
        Activate(isOn, true);
    }

    void OnEnable()
    {
        if (input) input.onToggle += Toggle;
    }

    void OnDisable()
    {
        if (input) input.onToggle -= Toggle;
    }

    [Button()]
    public void Toggle(bool on)
    {
        Activate(!isOn);
    }

    public virtual void Activate(bool on, bool init = false)
    {
        isOn = on;
        if (anim != null) anim.SetBool(activateParam, on);
        if (!init) onToggle?.Invoke(isOn);
    }
}
