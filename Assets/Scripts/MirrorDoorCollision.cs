using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorDoorCollision : MonoBehaviour
{
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
        }
    }
}
