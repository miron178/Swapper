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
        else
        {
            FindObjectOfType<AudioManager>().StopPlaying("SlimeWalk");
        }

        if (gameObject.tag == "Armoured")
        {
            if (!isWalking == true)
            {
                FindObjectOfType<AudioManager>().Play("ArmourWalk");
            }
        }
        else
        {
            FindObjectOfType<AudioManager>().StopPlaying("ArmourWalk");
        }

        if (gameObject.tag == "Small")
        {
            if (!isWalking == true)
            {
                FindObjectOfType<AudioManager>().Play("SmallWalk");
            }
        }
        else
        {
            FindObjectOfType<AudioManager>().StopPlaying("SmallWalk");
        }

        if (gameObject.tag == "Strong")
        {
            if (isWalking == true)
            {
                FindObjectOfType<AudioManager>().Play("BigChicken");
            }
        }
        else
        {
            FindObjectOfType<AudioManager>().StopPlaying("BigChicken");
        }

        if (gameObject.tag == "Strong")
        {
            if (!isWalking == true)
            {
                FindObjectOfType<AudioManager>().Play("BigWalk");
            }
        }
        else
        {
            FindObjectOfType<AudioManager>().StopPlaying("BigWalk");
        }

    }
}
