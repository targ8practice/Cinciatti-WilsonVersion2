using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public bool inRoomOne;
    public bool inRoomTwo;
    public bool inRoomThree;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RoomOnePlayerDetection")
        {
            inRoomOne = true;
        }

        if (other.tag == "RoomTwoPlayerDetection")
        {
            inRoomTwo = true;
        }

        if (other.tag == "RoomThreePlayerDetection")
        {
            inRoomThree = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "RoomOnePlayerDetection")
        {
            inRoomOne = false;
        }

        if (other.tag == "RoomTwoPlayerDetection")
        {
            inRoomTwo = false;
        }

        if (other.tag == "RoomThreePlayerDetection")
        {
            inRoomThree = false;
        }
    }
}
