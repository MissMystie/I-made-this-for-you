using System;
using UnityEngine;
using VInspector;

public class SwitchInput : MonoBehaviour
{
    public Animator anim;
    public Action<bool> onToggle;
    public bool isOn;
    public string activateParam = "IsOn";

}
