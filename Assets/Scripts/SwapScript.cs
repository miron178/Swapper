using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SwapScript : MonoBehaviour
{
    public Movement movementScript;

    //privates for active
    [SerializeField]
    private bool active = false;
    private bool lastActive = false;
    private Component[] meshRenderer;

    //private layers
    private int groundLayer;
    private int swapLayer;
    private int wallLayer;
    private int testLayer;

    //privates for swaps
    private GameObject objectInRange = null;
    private bool interactRelease = false;

    //privates for slime spawn
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private GameObject slimePrefab;

    //materials
    [SerializeField]
    private Material green;
    [SerializeField]
    private Material yellow;

    void Start()
    {
        //layer dependancies
        groundLayer = LayerMask.NameToLayer("Ground");
        testLayer = LayerMask.NameToLayer("Test");
        swapLayer = LayerMask.NameToLayer("Swappable");
        wallLayer = LayerMask.NameToLayer("Wall");

        meshRenderer = this.GetComponentsInChildren<MeshRenderer>();

        movementScript = GetComponent<Movement>();
    }

    void FixedUpdate()
    {
        if (active != lastActive)
        {
            lastActive = active;

            foreach (MeshRenderer renderer in meshRenderer)
            {
                renderer.material = active ? green : yellow;
            }
            movementScript.enabled = active;
        }

        if (active)
        {
            float interact = Input.GetAxis("Interact"); //swap

            float spawn = Input.GetAxis("Spawn"); //spawn

            if (interact > 0 && objectInRange && !interactRelease)
            {
                Swap();
                interactRelease = true;
            }
            else if (interact == 0)
            {
                interactRelease = false;
            }

            if (spawn > 0)
            {
                SpawnSlime();
            }
        }
    }

    private bool Obstacle(GameObject other)
    {
        int ignoreSwapLayer = ~(1 << swapLayer);
        Debug.DrawLine(this.gameObject.transform.position, other.gameObject.transform.position);
        return Physics.Linecast(this.gameObject.transform.position, other.gameObject.transform.position, ignoreSwapLayer);
    }

    private void MaybeInRange(GameObject other)
    {
        if (active && other.layer == swapLayer && !Obstacle(other))
        {
            objectInRange = other;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        MaybeInRange(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        MaybeInRange(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (active && objectInRange == other.gameObject)
        {
            objectInRange = null;
        }
    }
    private void Swap()
    {
        SwapScript playerInRange = objectInRange.GetComponent<SwapScript>();
        playerInRange.active = true;

        if (this.gameObject.layer != LayerMask.NameToLayer("Slime"))
        {
            this.active = false;
            objectInRange = null;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void SpawnSlime()
    {
        GameObject slime = Instantiate(slimePrefab, spawnPoint.position, spawnPoint.rotation);
        this.active = false;
    }
}
