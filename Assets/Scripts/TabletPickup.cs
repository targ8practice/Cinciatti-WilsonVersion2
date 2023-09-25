using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TabletPickup : MonoBehaviour
{
    private float pickupRange = 5f;
    public LayerMask pickupLayer;
    public LayerMask placeLayer;
    public Transform playerHoldingTablet;
    public Transform placeTabletPosition;

    public Transform RoomeOnePlaceTabletPosition;
    public Transform RoomeTwoPlaceTabletPosition;
    public Transform RoomeThreePlaceTabletPosition;

    private bool isHoldingTablet = false;
    private Rigidbody tabletRB;
    private Rigidbody[] deathBlocksRB;
    private GameObject tabletObject;

    public Animator openRoom1;

    public PlayerDetection playerDetection;

    bool roomOneCheck;
    bool roomTwoCheck;
    bool roomThreeCheck;
    public bool puzzleOneComplete;
    public bool puzzleTwoComplete;
    public bool puzzleThreeComplete;

    public GameObject mirrorDoorOne;
    public GameObject mirrorDoorTwo;
    public GameObject mirrorDoorThree;

    public GameObject roomTwoBars;
    public GameObject roomTwoBarsUp;
    public GameObject roomThreeBars;
    public GameObject roomThreeBarsUp;

    public Animator deathSpikes;
    public Animator deathWater;
    public PlayerInput playerInput;
    public Camera playerCam;
    public Transform room1DeathCam;
    public Transform room2DeathCam;
    public Transform room3DeathCam;

    public GameObject[] deathBlocks;

    public TimerScript timerScript;
    public AudioSource correctAnswer;
    public AudioSource incorrectAnswer;
    double playerScoreValue = 0;
    public Text playerScore;

    private void Start()
    {
        deathBlocksRB = new Rigidbody[deathBlocks.Length];
        for (int i = 0; i < deathBlocks.Length; i++)
        {
            deathBlocksRB[i] = deathBlocks[i].GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        roomOneCheck = playerDetection.inRoomOne;
        roomTwoCheck = playerDetection.inRoomTwo;
        roomThreeCheck = playerDetection.inRoomThree;

        RaycastHit hit;

        if (!isHoldingTablet && Physics.Raycast(transform.position, transform.forward, out hit, pickupRange, pickupLayer))
        {
            GameObject item = hit.collider.gameObject;

            if (Input.GetKeyDown(KeyCode.E))
            {
                tabletRB = item.GetComponent<Rigidbody>();
                tabletRB.isKinematic = true;

                // Set the item's parent to the playerHoldingTablet's transform
                item.transform.SetParent(playerHoldingTablet);
                item.transform.rotation = playerHoldingTablet.transform.rotation;

                // Set the item's position relative to the new parent (zero local position)
                item.transform.localPosition = Vector3.zero;

                // Store a reference to the tablet object for later use
                tabletObject = item;
                isHoldingTablet = true;
            }
        }

        else if (isHoldingTablet && Input.GetKeyDown(KeyCode.E) && Physics.Raycast(transform.position, transform.forward, out hit, pickupRange, placeLayer))
        {

            if (!roomOneCheck && !roomTwoCheck && !roomThreeCheck)
            {
                // Check if the player is looking at the placeTabletPosition layer
                // If so, relocate the tablet to the placeTabletPosition
                tabletObject.transform.SetParent(null); // Remove from player's hand
                tabletRB.isKinematic = false; // Enable physics
                tabletObject.transform.position = placeTabletPosition.position; // Set position to the desired location
                isHoldingTablet = false; // Player is no longer holding the tablet
                tabletObject.transform.rotation = placeTabletPosition.transform.rotation;
                tabletRB.isKinematic = true;
                tabletObject.GetComponent<BoxCollider>().enabled = false;
                openRoom1.SetBool("OpenRoom1", true);
                correctAnswer.Play();
            }

            else if (roomOneCheck)
            {
                // Same as above except position and rotation is now specific to room one
                tabletObject.transform.SetParent(null);
                tabletRB.isKinematic = false;
                tabletObject.transform.position = RoomeOnePlaceTabletPosition.position;
                isHoldingTablet = false;
                tabletObject.transform.rotation = RoomeOnePlaceTabletPosition.transform.rotation;
                tabletRB.isKinematic = true;
                tabletObject.GetComponent<BoxCollider>().enabled = false;

                if (tabletObject.tag == "XOR")
                {
                    Destroy(mirrorDoorOne);
                    roomTwoBars.SetActive(false);
                    roomTwoBarsUp.SetActive(true);
                    puzzleOneComplete = true;
                    correctAnswer.Play();
                    playerScoreValue = playerScoreValue + 100;
                    AddTimeBonus();
                    playerScore.text = playerScoreValue.ToString();
                }

                else
                {
                    playerInput.enabled = false;
                    playerCam.transform.position = room1DeathCam.transform.position;
                    playerCam.transform.rotation = room1DeathCam.transform.rotation;
                    deathSpikes.SetBool("RoomOneIncorrect", true);
                    incorrectAnswer.Play();
                }
            }

            else if (roomTwoCheck)
            {
                tabletObject.transform.SetParent(null);
                tabletRB.isKinematic = false;
                tabletObject.transform.position = RoomeTwoPlaceTabletPosition.position;
                isHoldingTablet = false;
                tabletObject.transform.rotation = RoomeTwoPlaceTabletPosition.transform.rotation;
                tabletRB.isKinematic = true;
                tabletObject.GetComponent<BoxCollider>().enabled = false;

                if (tabletObject.tag == "XOR")
                {
                    Destroy(mirrorDoorTwo);
                    roomThreeBars.SetActive(false);
                    roomThreeBarsUp.SetActive(true);
                    puzzleTwoComplete = true;
                    correctAnswer.Play();
                    playerScoreValue = playerScoreValue + 100;
                    AddTimeBonus();
                    playerScore.text = playerScoreValue.ToString();
                }

                else
                {
                    playerInput.enabled = false;
                    playerCam.transform.position = room2DeathCam.transform.position;
                    playerCam.transform.rotation = room2DeathCam.transform.rotation;
                    deathWater.SetBool("RoomTwoIncorrect", true);
                    incorrectAnswer.Play();
                }
            }

            else if (roomThreeCheck)
            {
                tabletObject.transform.SetParent(null);
                tabletRB.isKinematic = false;
                tabletObject.transform.position = RoomeThreePlaceTabletPosition.position;
                isHoldingTablet = false;
                tabletObject.transform.rotation = RoomeThreePlaceTabletPosition.transform.rotation;
                tabletRB.isKinematic = true;
                tabletObject.GetComponent<BoxCollider>().enabled = false;

                if (tabletObject.tag == "NAND")
                {
                    Destroy(mirrorDoorThree);
                    puzzleThreeComplete = true;
                    correctAnswer.Play();
                    playerScoreValue = playerScoreValue + 100;
                    AddTimeBonus();
                    playerScore.text = playerScoreValue.ToString();
                }

                else
                {
                    playerInput.enabled = false;
                    playerCam.transform.position = room3DeathCam.transform.position;
                    playerCam.transform.rotation = room3DeathCam.transform.rotation;

                    foreach (Rigidbody rb in deathBlocksRB)
                    {
                        rb.isKinematic = false;
                        StartCoroutine(LoadSceneWithDelay("MainMenu", 5.0f));
                        incorrectAnswer.Play();
                    }
                }
            }
        }
    }

    private void AddTimeBonus()
    {
        if (double.TryParse(timerScript.timerText.text, out double timerValue))
        {
            playerScoreValue += timerValue;
        }

        else
        {
            Debug.LogError("Failed to parse timerText.text as a double.");
        }
    }

    private IEnumerator LoadSceneWithDelay(string sceneName, float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(sceneName);
    }
}
