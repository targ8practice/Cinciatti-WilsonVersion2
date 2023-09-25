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

    public void PauseGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Start()
    {
        playerUI.enabled = false;
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
        playerUI.enabled = true;

        if (countDown > 0f)
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
        if (playerDetection.inRoomOne == true)
        {
            CounterRestartOne();
            StartTimer();
        }

        if (tabletPickup.puzzleOneComplete == true)
        {
            playerUI.enabled = false;
        }

        if (tabletPickup.puzzleOneComplete == true && playerDetection.inRoomTwo)
        {
            CounterRestartTwo();
            StartTimer();
        }

        if (tabletPickup.puzzleTwoComplete == true)
        {
            playerUI.enabled = false;
            restartCounter = true;
        }

        if (tabletPickup.puzzleTwoComplete == true && playerDetection.inRoomThree)
        {
            CounterRestartOne();
            StartTimer();
        }
    }
}
