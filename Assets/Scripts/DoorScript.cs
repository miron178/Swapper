using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject doorObjectL;
    public GameObject doorObjectR;

    private bool DoorOpen;
    private bool buttonPressed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            DoorOpen = !DoorOpen;
            doorObjectL.GetComponent<Animator>().SetBool("DoorOpen", DoorOpen);
            doorObjectR.GetComponent<Animator>().SetBool("DoorOpen", DoorOpen);
            FindObjectOfType<AudioManager>().Play("DoorOpening");
        }
        else if (other.gameObject.tag == "Strong")
        {
            DoorOpen = !DoorOpen;
            doorObjectL.GetComponent<Animator>().SetBool("DoorOpen", DoorOpen);
            doorObjectR.GetComponent<Animator>().SetBool("DoorOpen", DoorOpen);
            FindObjectOfType<AudioManager>().Play("DoorOpening");
        }
        else if (other.gameObject.tag == "Small")
        {
            DoorOpen = !DoorOpen;
            doorObjectL.GetComponent<Animator>().SetBool("DoorOpen", DoorOpen);
            doorObjectR.GetComponent<Animator>().SetBool("DoorOpen", DoorOpen);
            FindObjectOfType<AudioManager>().Play("DoorOpening");
        }
        else if (other.gameObject.tag == "Armoured")
        {
            DoorOpen = !DoorOpen;
            doorObjectL.GetComponent<Animator>().SetBool("DoorOpen", DoorOpen);
            doorObjectR.GetComponent<Animator>().SetBool("DoorOpen", DoorOpen);
            FindObjectOfType<AudioManager>().Play("DoorOpening");
        }
    }
}
