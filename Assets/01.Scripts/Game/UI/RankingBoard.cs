using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingBoard : MonoBehaviour
{
    public RankItem rankItemPrefab;

    public SetDataEventCahnnelSO rankDataItemChnner;
    public RectTransform rankHolderTrm;
    private readonly int rankCount = 5;
    private List<RankItem> _rankItemList;
    private void Awake()
    {
        _rankItemList = new List<RankItem>();
    }
    private void Start()
    {
        rankDataItemChnner.OnRaiseEvent.AddListener(HandleRankDataChanged);
    }

    private void HandleRankDataChanged(DataEvent evt)
    {
        if (_rankItemList.Count != 0)
        {
            for (int i = 0; i < _rankItemList.Count; ++i)
            {
                _rankItemList[i].SelfDestroy();
            }
            _rankItemList.Clear();
        }

        RankDataEvent rankEvt = evt as RankDataEvent;
        int spawnCount = 0;
        foreach (RankData data in rankEvt.list)
        {
            if (++spawnCount > rankCount)
                break;
            RankItem item = Instantiate(rankItemPrefab, rankHolderTrm);
            item.Text = $"{data.name}-{data.time.ToString("0.00")}";
            _rankItemList.Add(item);
        }
    }


}
