using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletPickup : MonoBehaviour
{
    private float pickupRange = 5f;
    public LayerMask pickupLayer;
    public LayerMask placeLayer;
    public Transform playerHoldingTablet;
    public Transform placeTabletPosition;
    private bool isHoldingTablet = false;
    private Rigidbody tabletRB;
    private GameObject tabletObject;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
            // Check if the player is looking at the placeTabletPosition layer
            // If so, relocate the tablet to the placeTabletPosition
            tabletObject.transform.SetParent(null); // Remove from player's hand
            tabletRB.isKinematic = false; // Enable physics
            tabletObject.transform.position = placeTabletPosition.position; // Set position to the desired location
            isHoldingTablet = false; // Player is no longer holding the tablet
            tabletObject.transform.rotation = placeTabletPosition.transform.rotation;
            tabletRB.isKinematic = true;
        }
    }
}
