using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class CheckAnimatorActivation : MonoBehaviour
{
    public Animator room1SpikesAnimator;

    private bool wasActivated = false;
    private int count;
    

    void Start()
    {
        count = 0;
        //check if the animator reference is set
        if (room1SpikesAnimator == null)
        {
            Debug.LogError("Room1Spikes Animator reference not set!");
            return;
        }
    }

    void Update()
    {
        //check if the animator is currently playing an animation
        bool isPlaying = room1SpikesAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f;

        if (isPlaying && !wasActivated)
        {
            //animator was just activated
            Debug.Log("Room1Spikes Animator was activated");
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
            Debug.Log("Room1Spikes Animator was deactivated");
            wasActivated = false;
        }
    }
    private IEnumerator LoadSceneWithDelay(string sceneName, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(sceneName);
    }
}
