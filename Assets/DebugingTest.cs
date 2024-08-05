using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DebugingTest : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        string token = PlayerPrefs.GetString("Token");
        if (token == string.Empty)
            _text.SetText("Null");
        else
            _text.SetText(token);

    }
}
