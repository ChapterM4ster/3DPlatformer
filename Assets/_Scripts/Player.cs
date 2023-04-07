using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private LayerMask pickableLayerMask;

    [SerializeField]
    private Transform playerCameraTransform;

    [SerializeField]
    private GameObject pickupUI;

    [SerializeField]
    [Min(1)]
    private float hitrange = 2;

    [SerializeField]
    private Transform pickUpParent;
    private Transform pickUpGun;

    [SerializeField]
    private GameObject inHandItem;
    private GameObject GrappleGun;

    private RaycastHit hit;

    private void Update()
    {
        if (hit.collider != null)
        {
            hit.collider.GetComponent<HighLight>()?.ToggleHighLight(false);
            pickupUI.SetActive(false);
        }

        if (inHandItem != null)
        {
            return;
        }

        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, hitrange, pickableLayerMask))
        {
            hit.collider.GetComponent<HighLight>()?.ToggleHighLight(true);
            pickupUI.SetActive(true);
        }


        if (Input.GetKeyDown(KeyCode.E))
        {

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (inHandItem != null)
            {
                Debug.Log("dropped the item you were holding");
                inHandItem.transform.SetParent(null);
                inHandItem = null;
                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (hit.collider != null)
            {
                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                if (hit.collider.GetComponent<Food>())
                {
                    inHandItem = hit.collider.gameObject;
                    inHandItem.transform.position = Vector3.zero;
                    inHandItem.transform.rotation = Quaternion.identity;
                    inHandItem.transform.SetParent(pickUpParent.transform, false);
                    if (rb != null)
                    {
                        rb.isKinematic = true;
                    }
                    return;
                }

                if (hit.collider.GetComponent<Tool>())
                {
                    GrappleGun = hit.collider.gameObject;
                    GrappleGun.transform.position = Vector3.zero;
                    GrappleGun.transform.rotation = Quaternion.identity;
                    GrappleGun.transform.SetParent(pickUpGun.transform, false);
                    if (rb != null)
                    {
                        rb.isKinematic = true;
                    }
                    return;
                }

                if (hit.collider.GetComponent<Item>())
                {
                    inHandItem = hit.collider.gameObject;
                    inHandItem.transform.SetParent(pickUpParent.transform, true);
                    if (rb != null)
                    {
                        rb.isKinematic = true;
                    } 
                    return;
                }

            }
        }

    }
}
