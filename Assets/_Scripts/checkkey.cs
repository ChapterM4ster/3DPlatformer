using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class checkkey : MonoBehaviour

{

    [SerializeField]
    private GameObject door;
    [SerializeField]
    private GameObject keyItem;
    

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
        if (other.tag == "Key")
        {
            GameObject.Destroy(door);
            GameObject.Destroy(keyItem);

            // put audio source here!
        }
    }
}
