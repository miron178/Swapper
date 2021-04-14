using UnityEngine;
using UnityEngine.SceneManagement;
public class BulletArmoured : MonoBehaviour
{
    private Transform armouredTarget;

    public float speed = 70f;

    public GameObject impacteEffect;

    public void Seek(Transform _targetA)
    {
        armouredTarget = _targetA;
    }

    // Update is called once per frame
    void Update()
    {
        if (armouredTarget == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = armouredTarget.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            Debug.Log("HitArmoured");
            GameObject effectIns = Instantiate(impacteEffect, transform.position, transform.rotation);
            Destroy(effectIns, 2f);
            Destroy(gameObject);
            
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }

}
