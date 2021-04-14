using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCreatures : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool("isWalking");

        if (gameObject.tag == "player")
        {
            if (!isWalking)
            {
                FindObjectOfType<AudioManager>().Play("SlimeWalk");
            }
        }
    }
}
