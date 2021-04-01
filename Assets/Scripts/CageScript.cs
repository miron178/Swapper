using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageScript : MonoBehaviour
{
    public GameObject cageGlassObject;
    public GameObject cageObject;

    private bool CageOpen;
    private bool buttonPressed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CageOpen = !CageOpen;
            cageObject.GetComponent<Animator>().SetBool("CageOpen", CageOpen);
            cageGlassObject.GetComponent<Animator>().SetBool("CageOpen", CageOpen);
        }
    }
}
