using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCageScript : MonoBehaviour
{
    public GameObject smallCageObject;

    private bool CageOpen;
    private bool buttonPressed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CageOpen = !CageOpen;
            smallCageObject.GetComponent<Animator>().SetBool("CageOpen", CageOpen);
        }
    }
}
