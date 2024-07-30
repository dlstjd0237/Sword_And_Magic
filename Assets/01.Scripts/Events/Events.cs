using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SendType
{
    GET,
    POS
}
public class NetworkEvent
{
    public SendType sendType;
}
public static class Events
{
    public static RegisterNetworkEvent RegisterNetworkEvent = new RegisterNetworkEvent();
    public static PostResultEvent PostResultEvent = new PostResultEvent();
    public static LoginNetworkEvent LoginNetworkEvent = new LoginNetworkEvent();
    public static LogNetworkEvent LogNetworkEvent = new LogNetworkEvent();
    public static ClearLogNetworkEvent ClearLogNetworkEvent = new ClearLogNetworkEvent();
    public static NetworkEvent VoidEvent = new NetworkEvent();
}

public class RegisterNetworkEvent : NetworkEvent, IWWWFormable
{
    public string email;
    public string name;
    public string password;

    public WWWForm GetWWWForm()
    {
        var form = new WWWForm();
        form.AddField("email", email);
        form.AddField("name", name);
        form.AddField("password", password);

        return form;
    }
}

public class PostResultEvent : NetworkEvent
{
    public string uri;
    public ResponseMsg response;
}

public class LoginNetworkEvent : NetworkEvent, IWWWFormable
{
    public string email;
    public string password;

    public WWWForm GetWWWForm()
    {
        var form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", password);

        return form;
    }
}

public class LogNetworkEvent : NetworkEvent, IWWWFormable
{
    public float record;

    public WWWForm GetWWWForm()
    {
        var form = new WWWForm();
        form.AddField("record", record.ToString());
        return form;
    }
}

public class ClearLogNetworkEvent : NetworkEvent, IWWWFormable
{
    public float time;

    public WWWForm GetWWWForm()
    {
        var form = new WWWForm();
        form.AddField("time", time.ToString());
        return form;
    }
}