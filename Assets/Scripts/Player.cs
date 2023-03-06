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

    [SerializeField]
    private GameObject inHandItem;

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
