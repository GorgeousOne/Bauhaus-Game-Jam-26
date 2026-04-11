using System;
using UnityEngine;

public class CandlePuzzle : MonoBehaviour
{

    int[] candleOrder = {3, 0, 1, 4, 2};
    CandleLogic[] candles;
    int successCounter = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        candles = GetComponentsInChildren<CandleLogic>();

        foreach (CandleLogic c in candles)
        {
            c.CandleClick.AddListener(OnCandleClick);
        }
    }

    void OnCandleClick(CandleLogic c)
    {
        int idx = Array.IndexOf(candles, c);


    }

    // Update is called once per frame
    void Update()
    {

    }
}
