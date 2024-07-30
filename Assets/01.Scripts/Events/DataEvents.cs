using System.Collections.Generic;
using System;
public class DataEvent
{

}
public class DataEvents
{
    public static DataEvent VoidDataEvent = new DataEvent();
    public static StringDataEvent StringDataEvent = new StringDataEvent();
    public static FloatDataEvent FloatDataEvent = new FloatDataEvent();
    public static RankDataEvent RankDataEvent = new RankDataEvent();
}



public class StringDataEvent : DataEvent
{
    public string data;
}

public class FloatDataEvent : DataEvent
{
    public float data;
}

[Serializable]
public class RankData
{
    public string name;
    public float time;
}

[Serializable]
public class RankDataEvent : DataEvent
{
    public List<RankData> list;
}
