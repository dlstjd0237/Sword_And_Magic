using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "SO/Event/NetworkEvent")]
public class NetworkEventChannelSO : ScriptableObject
{
    public UnityEvent<NetworkEvent> OnRaiseEvent;

    public void RaiseEvent(NetworkEvent evt)
    {
        OnRaiseEvent?.Invoke(evt);
    }
}
