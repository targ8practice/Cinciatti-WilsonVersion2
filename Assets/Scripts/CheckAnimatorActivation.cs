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

    public Camera playerCam;
    public Transform room1DeathCam;
    public Transform room2DeathCam;
    public Transform room3DeathCam;
    public PlayerInput playerInput;
    public PlayerDetection playerDetection;

    public GameObject[] deathBlocks;
    public Rigidbody[] deathBlocksRB;

    void Start()
    {
        count = 0;
        //check if the animator reference is set
        if (room1SpikesAnimator == null)
        {
            Debug.LogError("Animator reference not set!");
            return;
        }

        deathBlocksRB = new Rigidbody[deathBlocks.Length];
        for (int i = 0; i < deathBlocks.Length; i++)
        {
            deathBlocksRB[i] = deathBlocks[i].GetComponent<Rigidbody>();
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

            if (playerDetection.inRoomOne)
            {
                // Room One
                tabletPickup.deathSpikes.SetBool("RoomOneIncorrect", true);
                playerInput.enabled = false;
                playerCam.transform.position = room1DeathCam.transform.position;
                playerCam.transform.rotation = room1DeathCam.transform.rotation;
            }

            else if (playerDetection.inRoomTwo)
            {
                // Room Two
                tabletPickup.deathWater.SetBool("RoomTwoIncorrect", true);
                playerInput.enabled = false;
                playerCam.transform.position = room2DeathCam.transform.position;
                playerCam.transform.rotation = room2DeathCam.transform.rotation;
            }

            else if (playerDetection.inRoomThree)
            {
                // Room Three
                foreach (Rigidbody rb in deathBlocksRB)
                {
                    rb.isKinematic = false;
                }
                playerInput.enabled = false;
                playerCam.transform.position = room3DeathCam.transform.position;
                playerCam.transform.rotation = room3DeathCam.transform.rotation;
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
