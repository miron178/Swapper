using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SwapScript : MonoBehaviour
{
    private Movement movementScript;
    private AnimationController animationScript;

    //privates for active
    [SerializeField]
    private bool active = false;
    private bool lastActive = false;
    private Component[] meshRenderer;

    //private layers
    private int swapLayer;
    private int slimeLayer;

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
        swapLayer = LayerMask.NameToLayer("Swappable");
        slimeLayer = LayerMask.NameToLayer("Slime");

        meshRenderer = this.GetComponentsInChildren<MeshRenderer>();

        movementScript  = GetComponent<Movement>();
        animationScript = GetComponent<AnimationController>();
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
            animationScript.enabled = active;
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

            if (spawn > 0 && this.gameObject.layer != slimeLayer)
            {
                SpawnSlime();
            }
        }
    }

    private bool Obstacle(GameObject other)
    {
        SwapScript swap = other.GetComponent<SwapScript>();
        Vector3 target = swap.spawnPoint.position;
        int ignoreSwapLayer = ~(1 << swapLayer);
        Debug.DrawLine(this.gameObject.transform.position, target);
        return Physics.Linecast(this.gameObject.transform.position, target, ignoreSwapLayer);
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

        if (this.gameObject.layer != slimeLayer)
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
