using System;
using System.Collections.Generic;
using Mystie;
using UnityEngine;
using VInspector;
using static HealthManager;

public class Mechanism : MonoBehaviour
{
    public Action onToggle;
    public Action<bool> onActivate;
    public Animator anim;
    public Mechanism input;
    public bool isOn;
    //public bool locked;
    public bool autoLock;
    [ShowIf("autoLock")] public bool autoLockValue;
    [ShowIf("autoLock")] public float autoTime;
    [ShowIf("autoLock")] public bool autoToggle;
    private Timer timer;
    public string activateParam = "IsOn";

    public void Awake()
    {
        timer = new Timer();
        Activate(isOn, true);
    }

    void OnEnable()
    {
        if (input) input.onToggle += Toggle;
        if (autoToggle) timer.onTimerEnd += Toggle;
    }

    void OnDisable()
    {
        if (input) input.onToggle -= Toggle;
        if (autoToggle) timer.onTimerEnd -= Toggle;
    }

    private void Update()
    {
        timer.Tick(Time.deltaTime);
    }

    [Button()]
    public void Toggle()
    {
        Activate(!isOn);
    }

    public virtual void Activate(bool on, bool init = false)
    {
        if (timer.time > 0) return;

        isOn = on;
        if (anim != null) anim.SetBool(activateParam, on);
        if (!init)
        {
            if (autoLock && on == autoLockValue)
                timer.SetTime(autoTime);

            onToggle?.Invoke();
        }
    }
}
