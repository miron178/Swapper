using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestroy : MonoBehaviour
{
    public GameObject destroyedVersion;

	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Strong")
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}