using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadlyWorldBounds : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
