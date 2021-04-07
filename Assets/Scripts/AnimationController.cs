using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
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
        bool keyPressed = Input.GetKey("w") | Input.GetKey("a") | Input.GetKey("s") | Input.GetKey("d");
        bool isPunching = animator.GetBool("isPunching");

        //if player pressed w key play walking anim
        if (!isWalking && keyPressed)
        {
            animator.SetBool("isWalking", true);
        }

        // if player stops pressing w key, stop walking anim
        if (isWalking && !keyPressed)
        {
            animator.SetBool("isWalking", false);
        }

        if (!isPunching && Input.GetKeyDown("space"))
        {
            animator.SetBool("isPunching", true);
        }

        // if player stops pressing w key, stop walking anim
        if (isPunching && Input.GetKeyUp("space"))
        {
            animator.SetBool("isPunching", false);
        }
    }
}
