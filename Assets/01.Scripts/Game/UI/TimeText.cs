using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeText : MonoBehaviour
{
    public SetDataEventCahnnelSO gameTimeChannel;

    private TextMeshProUGUI _timeText;

    private void Awake()
    {
        _timeText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        gameTimeChannel.OnRaiseEvent.AddListener(HandleGameTimeChanged);
    }

    private void HandleGameTimeChanged(DataEvent evt)
    {
        FloatDataEvent floatEvt = evt as FloatDataEvent;
        _timeText.text = floatEvt.data.ToString("0.00");
    }
}
