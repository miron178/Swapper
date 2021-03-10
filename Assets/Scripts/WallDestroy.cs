using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestroy : MonoBehaviour
{
    //public GameObject destroyedVersion;

#if true
    void OnCollisionEnter(Collision other)
	{
        if (other.gameObject.tag == "Strong")
        {
            Destroy(this.gameObject);
        }
    }

#else
    void OnTriggerEnter()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
#endif
}
