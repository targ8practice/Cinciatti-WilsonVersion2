using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorDoorCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
