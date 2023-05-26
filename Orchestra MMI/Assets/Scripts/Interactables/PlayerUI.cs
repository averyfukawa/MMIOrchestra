using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI instrumentText;
    public void UpdateText(string instrument)
    {
        instrumentText.text = instrument;
    }
}
