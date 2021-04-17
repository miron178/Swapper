using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarsScript : MonoBehaviour
{
    public GameObject barsObject;

    private bool BarsOpen;
    private bool buttonPressed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            BarsOpen = !BarsOpen;
            barsObject.GetComponent<Animator>().SetBool("BarsOpen", BarsOpen);
        }
    }
}
