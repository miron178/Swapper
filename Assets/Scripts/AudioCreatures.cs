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

        if (gameObject.tag == "Player")
        {
            if (!isWalking == true)
            {
                FindObjectOfType<AudioManager>().Play("SlimeWalk");
            }
        }

        if (gameObject.tag == "Armoured")
        {
            if (!isWalking == true)
            {
                FindObjectOfType<AudioManager>().Play("ArmourWalk");
            }
        }

        if (gameObject.tag == "Small")
        {
            if (!isWalking == true)
            {
                FindObjectOfType<AudioManager>().Play("SmallWalk");
            }
        }

        if (gameObject.tag == "Strong")
        {
            if (isWalking == true)
            {
                FindObjectOfType<AudioManager>().Play("BigChickens");
            }
        }

        if (gameObject.tag == "Strong")
        {
            if (!isWalking == true)
            {
                FindObjectOfType<AudioManager>().Play("BigWalk");
            }
        }

    }
}
