using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true; // Make sure the cursor is visible
        Cursor.lockState = CursorLockMode.None; // Ensure the cursor is not locked
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
