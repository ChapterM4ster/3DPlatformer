using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayer : MonoBehaviour
{

    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject CheckPoint;


    Vector3 checkpoint;
    

    // Start is called before the first frame update
    void Start()
    {
        checkpoint = CheckPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="OutOfBounds")
        {
            Player.transform.position = checkpoint;
        }
    }
}
