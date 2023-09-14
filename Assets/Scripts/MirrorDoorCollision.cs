using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorDoorCollision : MonoBehaviour
{
    public bool roomOneEntered;
    public bool roomTwoEntered;
    public bool roomThreeEntered;

    public TimerScript timerScript;
    public GameObject thisDoor;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        timerScript.enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<BoxCollider>().isTrigger = false;
            
            if (thisDoor.tag == "Room1")
            {
                roomOneEntered = true;
                timerScript.enabled = true;
            }

            else if (this.tag == "Room2")
            {
                roomTwoEntered = true;
            }

            else if (this.tag == "Room3")
            {
                roomThreeEntered = true;
            }
        }
    }
}
