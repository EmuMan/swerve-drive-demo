using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitOnEsc : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();

        if (Input.GetKey("r"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        for (int k = 0; k < 9; k++)
        {
            if (Input.GetKey("joystick 1 button " + k))
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
