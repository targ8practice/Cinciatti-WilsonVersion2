using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TimerScript : MonoBehaviour
{
    [SerializeField]
    float countDown;
    [SerializeField]
    Text timerTextField;
    [SerializeField]

    public PlayerDetection playerDetection;
    public TabletPickup tabletPickup;
    public Canvas playerUI;

    public bool restartCounter = true;

    public Text timerText;
    public Text timerLabel;

    public bool pauseTimer;

    public void PauseGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Start()
    {
        timerLabel.enabled = false;
        timerText.enabled = false;
    }

    public void CounterRestartOne()
    {
        if (restartCounter == true)
        {
            countDown = 60;
        }

        restartCounter = false;
    }

    public void CounterRestartTwo()
    {
        if (restartCounter == false)
        {
            countDown = 60;
        }

        restartCounter = true;
    }

    public void StartTimer()
    {
        timerLabel.enabled = true;
        timerText.enabled = true;

        if (countDown > 0f && pauseTimer == false)
        {
            countDown -= Time.deltaTime;

            if (countDown <= 0f)
            {
                countDown = 0f;
            }
            double correctedCountdownValue = System.Math.Round(countDown, 0);
            timerTextField.text = correctedCountdownValue.ToString();
        }
    }

    private void Update()
    {
        if (tabletPickup.puzzleOneComplete)
        {
            pauseTimer = true;
        }

        if (tabletPickup.puzzleOneComplete && playerDetection.inRoomTwo)
        {
            pauseTimer = false;
        }

        if (tabletPickup.puzzleTwoComplete)
        {
            pauseTimer = true;
        }

        if (tabletPickup.puzzleTwoComplete && playerDetection.inRoomThree)
        {
            pauseTimer = false;
        }

        if (tabletPickup.puzzleThreeComplete)
        {
            pauseTimer = true;
        }

        if (countDown <= 45)
        {
            timerText.color = Color.yellow;
        }

        if (countDown <= 30)
        {
            timerText.color = new Color(1.0f, 0.64f, 0.0f);
        }

        if (countDown <= 15)
        {
            timerText.color = Color.red;
        }

        if (playerDetection.inRoomOne == true)
        {
            CounterRestartOne();
            StartTimer();
        }

        if (tabletPickup.puzzleOneComplete == true)
        {
            timerLabel.enabled = false;
            timerText.enabled = false;
        }

        if (tabletPickup.puzzleOneComplete == true && playerDetection.inRoomTwo)
        {
            CounterRestartTwo();
            StartTimer();
        }

        if (tabletPickup.puzzleTwoComplete == true)
        {
            //restartCounter = true;
            timerLabel.enabled = false;
            timerText.enabled = false;
        }

        if (tabletPickup.puzzleTwoComplete == true && playerDetection.inRoomThree)
        {
            CounterRestartOne();
            StartTimer();
        }

        if (tabletPickup.puzzleThreeComplete == true)
        {
            // Add victory transtition
            timerLabel.enabled = false;
            timerText.enabled = false;
        }
    }
}
