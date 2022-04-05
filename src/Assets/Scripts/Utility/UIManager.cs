using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject meter;

    void Start()
    {
        Player.SharedInstance.OnScoreUpdate += UpdateScore;
        Player.SharedInstance.OnMeterLoss += UpdateMeterLoss;
        Player.SharedInstance.OnMeterGain += UpdateMeterGain;
        Player.SharedInstance.OnMeterReset += ResetMeter;
    }

    private void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    private void UpdateMeterLoss(float lossAmount)
    {
       if(meter.transform.localScale.y > 0)
       {
            meter.transform.localScale = new Vector2(meter.transform.localScale.x,
                meter.transform.localScale.y -
                  lossAmount);
       }
    }

    private void UpdateMeterGain(float maxMeterScaleY, int targetTimesReeled, int timesReeled)
    {
        float meterRemaining = maxMeterScaleY - meter.transform.localScale.y;

        meter.transform.localScale =
                     new Vector2(meter.transform.localScale.x,
                     meter.transform.localScale.y +
                     (meterRemaining / (targetTimesReeled - timesReeled)));
    }

    private void ResetMeter()
    {
        meter.transform.localScale = new Vector2(meter.transform.localScale.x, 0);
    }
}
