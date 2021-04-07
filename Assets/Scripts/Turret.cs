using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public Transform target;
    public float range = 15f;
    public string enemyTag = "Slime";

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemy = GameObject.FindGameObjectsWithTag(enemyTag);

        foreach (GameObject Slime in enemy)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
