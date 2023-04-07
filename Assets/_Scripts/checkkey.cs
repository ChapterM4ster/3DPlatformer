using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;



public class checkkey : MonoBehaviour

{

    [SerializeField]
    private GameObject door;
    [SerializeField]
    private GameObject keyItem;
    [SerializeField]
    private AudioSource UnlockingGate;
    

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
            UnlockingGate.Play();
            GameObject.Destroy(door);
            GameObject.Destroy(keyItem);

            // put audio source here!
        }
    }
}
