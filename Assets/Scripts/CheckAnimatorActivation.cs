using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckAnimatorActivation : MonoBehaviour
{
    public Animator room1SpikesAnimator;

    private bool wasActivated = false;
    private int count;

    public TimerScript timerScript;
    public TabletPickup tabletPickup;
    public Text timerValueText;
    public AudioSource timeExpired;
    bool audioPlayed;

    void Start()
    {
        count = 0;
        //check if the animator reference is set
        if (room1SpikesAnimator == null)
        {
            Debug.LogError("Animator reference not set!");
            return;
        }
    }

    void Update()
    {
        //timerValueText = timerScript.timerText;

        //check if the animator is currently playing an animation
        bool isPlaying = room1SpikesAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f;

        
        if (isPlaying && !wasActivated)
        {
            //animator was just activated
            Debug.Log("Animator was activated");
            wasActivated = true;

            if (count == 1)
            {
                count = 2;
            }


        }
        else if (!isPlaying && wasActivated)
        {
            if (count == 0)
            {
                count = 1;
            }

            if (count == 2)
            {
                count = 0;
                //load mainmenu
                //SceneManager.LoadScene("MainMenu");
                StartCoroutine(LoadSceneWithDelay("MainMenu", 2.0f)); // Load "MainMenu" after 2 seconds
            }
            // Animator has stopped playing
            Debug.Log("Animator was deactivated");
            wasActivated = false;
        }

        if (double.TryParse(timerValueText.text, out double timerValue) && timerValue < 1)
        {
            StartCoroutine(LoadSceneWithDelay("MainMenu", 2.0f));

            if (audioPlayed == false)
            {
                timeExpired.Play();
                audioPlayed = true;
            }
        }

        else if (tabletPickup.puzzleThreeComplete == true)
        {
            StartCoroutine(LoadSceneWithDelay("Congratulations", 2.0f));
        }
    }
    private IEnumerator LoadSceneWithDelay(string sceneName, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(sceneName);
    }
}
