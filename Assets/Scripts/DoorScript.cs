using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject doorObjectL;
    public GameObject doorObjectR;

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
            doorObjectL.GetComponent<Animator>().SetBool("DoorOpen", DoorOpen);
            doorObjectR.GetComponent<Animator>().SetBool("DoorOpen", DoorOpen);
        }
    }
}
