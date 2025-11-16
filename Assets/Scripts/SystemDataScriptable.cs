using System.Collections;
using System.Collections.Generic;
using Mystie.Audio;
using NaughtyAttributes;
using UnityEngine;

namespace Mystie.Core
{
    [CreateAssetMenu(fileName = "System Data", menuName = "Data/System/System Data", order = 0)]
    public class SystemDataScriptable : ScriptableObject
    {
        public new string name = "SystemData";

        [field: SerializeField] public CursorManager cursorPrefab { get; private set; }

        [Header("Scenes")]

        [Scene] public string mainMenuScene;
        [Scene] public string campScene;

        [Header("Audio")]

        public List<AudioBus> audioBuses = new List<AudioBus>();
    }
}
