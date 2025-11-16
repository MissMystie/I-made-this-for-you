using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Mystie.UI
{
    public class CreditsEntryUI : MonoBehaviour
    {
        public TextMeshProUGUI nameLabel;
        public TextMeshProUGUI titleLabel;

        public void Set(string name, string titleString)
        {
            nameLabel.text = name;
            titleLabel.text = titleString;
        }
    }
}
