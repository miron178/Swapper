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

    private void OnDisable()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isPunching", false);
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool("isWalking");
        bool keyPressed = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
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

        if (!isPunching && Input.GetButtonDown("Action"))
        {
            animator.SetBool("isPunching", true);
        }

        // if player stops pressing w key, stop walking anim
        if (isPunching && Input.GetButtonUp("Action"))
        {
            animator.SetBool("isPunching", false);
        }
    }
}
