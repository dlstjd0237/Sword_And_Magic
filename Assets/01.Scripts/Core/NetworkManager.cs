using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum MsgType : int
{
    ERROR = 0,
    SUCCESS = 1
}

[Serializable]
public class ResponseMsg
{
    public MsgType type;
    public string msg;
}

public class NetworkManager : MonoBehaviour
{
    [SerializeField] private string _host;
    [SerializeField] private ushort _port;

    public NetworkEventChannelSO registerChannel, postResultChannel, loginChannel, logChannel, clearLogChannel, rankChanner;
    public SetDataEventCahnnelSO tokenChannel, gameStartChannel, rankDataChannel;

    [field: SerializeField, TextArea] public string Token { get; private set; } = string.Empty;

    private void Awake()
    {
        registerChannel.OnRaiseEvent.AddListener(HandleRegisterEvent);
        loginChannel.OnRaiseEvent.AddListener(HandleLoginEvent);
        tokenChannel.OnRaiseEvent.AddListener(HandleTokenChangeEvent);
        logChannel.OnRaiseEvent.AddListener(HandleGameLogEvent);
        clearLogChannel.OnRaiseEvent.AddListener(HandleClearLogEvent);
        rankChanner.OnRaiseEvent.AddListener(HandleRankRequestEvent);
    }

    private void HandleRankRequestEvent(NetworkEvent evt)
    {
        GetRequest("game_rank", (res) =>
        {
            //랭크 데이터 채널에 넘겨주면 됩니다.
            string payload = res.msg;
            var evt = JsonUtility.FromJson<RankDataEvent>(payload);
            rankDataChannel.RaiseEvent(evt);
        });
    }

    private void HandleClearLogEvent(NetworkEvent evt)
    {
        ClearLogNetworkEvent logEvt = evt as ClearLogNetworkEvent;
        PostRequest("game_clear_log", logEvt.GetWWWForm());
    }

    private void HandleGameLogEvent(NetworkEvent evt)
    {
        LogNetworkEvent logEvt = evt as LogNetworkEvent;
        PostRequest("game_reset_log", logEvt.GetWWWForm());
    }

    private void Start()
    {
        Token = PlayerPrefs.GetString("Token", "");

        if (Token != "")
        {
            GetRequest("verify_token", (res) =>
            {
                if (res.type == MsgType.SUCCESS)
                {
                    gameStartChannel.RaiseEvent(DataEvents.VoidDataEvent);
                    Debug.Log("이미 로그인된 유저");
                }
            });
        }
    }
    [ContextMenu("DeleteToken")]
    private void DeleteToken()
    {
        PlayerPrefs.DeleteKey("Token");
    }


    private void SetRequestToken(UnityWebRequest req)
    {
        if (!string.IsNullOrEmpty(Token))
        {
            req.SetRequestHeader("Authorization", $"Bearer{Token}");
        }
    }

    private void HandleTokenChangeEvent(DataEvent evt)
    {
        var strEvt = evt as StringDataEvent;
        Token = strEvt.data;
        PlayerPrefs.SetString("Token", Token);
    }

    private void HandleLoginEvent(NetworkEvent evt)
    {

        var loginEvt = evt as LoginNetworkEvent;
        PostRequest("user_login", loginEvt.GetWWWForm());
    }

    private void HandleRegisterEvent(NetworkEvent evt)
    {
        var registerEvt = evt as RegisterNetworkEvent;
        PostRequest("user_register", registerEvt.GetWWWForm());
    }

    public void GetRequest(string path, Action<ResponseMsg> Callback = null)
    {
        StartCoroutine(GetCoroutine(path, Callback));
    }

    private IEnumerator GetCoroutine(string path, Action<ResponseMsg> Callback)
    {
        string url = $"{_host}:{_port}/{path}";

        using (var req = UnityWebRequest.Get(url))
        {
            SetRequestToken(req); //토큰 셋팅

            yield return req.SendWebRequest();

            //if(req.responseCode == UnityWebRequest.Result.Success)

            Debug.Log(req.downloadHandler.text);
            ResponseMsg responseMsg = JsonUtility.FromJson<ResponseMsg>(req.downloadHandler.text);
            Callback?.Invoke(responseMsg);
        }
    }

    public void PostRequest(string path, WWWForm form)
    {
        StartCoroutine(PostCoroutine(path, form));
    }

    private IEnumerator PostCoroutine(string path, WWWForm form)
    {
        string url = $"{_host}:{_port}/{path}";

        using (var req = UnityWebRequest.Post(url, form))
        {
            SetRequestToken(req);

            yield return req.SendWebRequest();

            ResponseMsg msg = JsonUtility.FromJson<ResponseMsg>(req.downloadHandler.text);

            var evt = Events.PostResultEvent;
            evt.uri = path;
            evt.response = msg;

            postResultChannel.RaiseEvent(evt);
        }
    }
}
