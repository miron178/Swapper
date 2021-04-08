using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestroy : MonoBehaviour
{
    public GameObject destroyedVersion;

    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetKey("space"))
        {
            if (other.gameObject.tag == "Strong")
            {
                StartCoroutine(HoldOn());
                Debug.Log("Hold On");
            }
            return;
        }
    }
    IEnumerator HoldOn()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}