using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{

    [Header("Setup")]
    private LineRenderer lr;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, PlayerCamera, player;
    private Vector3 grapplePoint;
    private SpringJoint joint;
    

    [Header("Grapple")]
    private float maxDistance = 100f;
    public bool grappling = false;
    public bool IsAvailable = true;
    public float CooldownDuration = 2.0f;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (IsAvailable == false)
        {
            return;
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {

                StartGrapple();

            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopGrapple();
                StartCoroutine(StartCooldown());
            }
        }
        
    }

    void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {

        RaycastHit hit;
        if (Physics.Raycast(origin: PlayerCamera.position, direction: PlayerCamera.forward, out hit, maxDistance, whatIsGrappleable))
        {
            grappling = true;
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(a: player.position, b: grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.5f;
            joint.minDistance = distanceFromPoint * 0.15f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
        }

    }

    void DrawRope()
    {
        if (!joint) return;
        lr.SetPosition(index: 0, gunTip.position);
        lr.SetPosition(index: 1, grapplePoint);
    }

    void StopGrapple()
    {
        grappling = false;
        lr.positionCount = 0;
        Destroy(joint);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }

    public IEnumerator StartCooldown()
    {
        IsAvailable = false;
        yield return new WaitForSeconds(CooldownDuration);
        IsAvailable = true;
    }
}
