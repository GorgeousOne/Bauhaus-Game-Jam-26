using System;
using UnityEngine;
using UnityEngine.Events;

public class CandlePuzzle : MonoBehaviour
{

    public AudioClip extinguishSound;
    public AudioClip lightUpSound;

    public GameObject candleUi;
    public UnityEvent FinishEvent;

    AudioSource speaker;

    int[] candleOrder = {3, 0, 1, 4, 2};
    CandleLogic[] candles;
    int numCorrectCandles = 0;

    void Awake()
    {
        speaker = GetComponent<AudioSource>();
        candles = candleUi.transform.GetComponentsInChildren<CandleLogic>();
        foreach (CandleLogic c in candles)
        {
            c.CandleClick.AddListener(OnCandleClick);
        }
    }

    void OnCandleClick(CandleLogic candle)
    {
        // speaker.PlayOneShot(lightUpSound);
        int clickedIdx = Array.IndexOf(candles, candle);
        int correctIdx = candleOrder[numCorrectCandles];

        if (clickedIdx == correctIdx)
        {
            candle.SetLit(true);
            numCorrectCandles += 1;
            Debug.Log("correct" + numCorrectCandles);

            if (numCorrectCandles == candleOrder.Length)
            {
                GameState.Instance.solvedCandles();
                FinishEvent.Invoke();
                Invoke(nameof(Hide), 1.5f);
            }
        } else
        {
            Debug.Log("WRONG");
            // speaker.PlayOneShot(extinguishSound);
            foreach (CandleLogic c in candles)
            {
                c.SetLit(false);
                numCorrectCandles = 0;
            }
        }
    }

    public void Show()
    {
        candleUi.SetActive(true);
    }

    public void Hide()
    {
        candleUi.SetActive(false);
    }
}
