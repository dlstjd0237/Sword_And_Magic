using UnityEngine;
using UnityEngine.UI;

public class BtnReset : MonoBehaviour
{
    public SetDataEventCahnnelSO gameStartChannel;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(HandleGameReset);    
    }

    private void HandleGameReset()
    {
        var evt = DataEvents.VoidDataEvent;
        gameStartChannel.RaiseEvent(evt);
    }
}
