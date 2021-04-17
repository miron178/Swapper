using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EcsapeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Esc") != 0)
        {
            Debug.Log("Main Menu");
            SceneManager.LoadScene("MainMenu");
        }
    }
}
