using UnityEngine;
using UnityEngine.SceneManagement;
public class BulletSlime : MonoBehaviour
{
    private Transform slimeTarget;

    public float speed = 70f;

    public void Seek(Transform _target)
    {
        slimeTarget = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (slimeTarget == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = slimeTarget.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            Debug.Log("HitSlime");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Destroy(gameObject);
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }
}
