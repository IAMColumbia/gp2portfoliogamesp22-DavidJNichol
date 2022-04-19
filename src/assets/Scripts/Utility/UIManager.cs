using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject meter;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private Text timer;
    [SerializeField] private Text goalAmount;
    [SerializeField] private GameObject shop;
    [SerializeField] private Text shopCurrency;
    [SerializeField] private Text CaughtAmount;
    [SerializeField] private Camera shopCamera;


    void Start()
    {
        Player.SharedInstance.OnScoreUpdate += UpdateScore;
        Player.SharedInstance.OnMeterLoss += UpdateMeterLoss;
        Player.SharedInstance.OnMeterGain += UpdateMeterGain;
        Player.SharedInstance.OnMeterReset += ResetMeter;

        WaveManager.SharedInstance.OnWaveEnd += ShowEndScreen;
        WaveManager.SharedInstance.StartNewWave();
        goalAmount.text = WaveManager.SharedInstance.CurrentWave.GoalAmount.ToString();
    }

    private void Update()
    {
        var ts = TimeSpan.FromSeconds(WaveManager.SharedInstance.timer.TimeRemaining);
        timer.text = string.Format("{0:00}:{1:00}:{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds);
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

    private void ShowEndScreen()
    {
        CaughtAmount.text = Player.SharedInstance.Score.ToString();
        endScreen.gameObject.SetActive(true);
        Time.timeScale = 0; // stop time

        if(Cursor.visible == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }    
    }

    public void ShowShop()
    {
        endScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
        shop.SetActive(true);
        shopCamera.enabled = true;
        Camera.main.enabled = false;
        transform.parent.gameObject.SetActive(false); // deactivate game
        shopCurrency.text = Player.SharedInstance.Score.ToString();
    }
}
