using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject doorObject;

    private bool DoorOpen;
    private bool buttonPressed;

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
        if (other.gameObject.tag != "Player")
        {
            DoorOpen = !DoorOpen;
            doorObject.GetComponent<Animator>().SetBool("DoorOpen", DoorOpen);
        }
    }
}
