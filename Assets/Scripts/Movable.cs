using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    private bool movable = false;

    Rigidbody m_Rigidbody;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Strong")
        {
            m_Rigidbody.constraints = RigidbodyConstraints.None;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }
}
