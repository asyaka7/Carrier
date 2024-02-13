using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    void Update()
    {
      #if UNITY_STANDALONE
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
      #endif
    }
}
