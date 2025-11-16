using Mystie.Core;
using Mystie.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mystie.UI
{
    public class CreditsUI : MonoBehaviour
    {
        public List<CreditsEntry> entries;

        void Start()
        {
            UpdateEntries();
        }

        public void UpdateEntries()
        {
            if (!entries.IsNullOrEmpty())
            {
                foreach (CreditsEntry entry in entries)
                {
                    if (entry.ui != null)
                    {
                        entry.ui.gameObject.name = entry.name + " credit";
                        entry.ui.Set(entry.name, entry.title);
                    }
                }
            }
        }

        [Serializable]
        public struct CreditsEntry
        {
            [field: SerializeField] public string name { get; private set; }
            [field: SerializeField] public CreditsEntryUI ui { get; private set; }
            [field: SerializeField] public string title { get; private set; }
        }
    }
}
