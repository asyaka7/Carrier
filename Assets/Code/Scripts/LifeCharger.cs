using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

[ExecuteAlways]
public class LifeCharger : MonoBehaviour
{
    [SerializeField] float LightIntensity = 1.0f;
    Light innerLight;

    void Awake()
    {
        innerLight = GetComponentInChildren<Light>();
        innerLight.intensity = LightIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        EditorLightIntensityChange();
    }
    
    private void EditorLightIntensityChange()
    {
        if (!Application.isPlaying)
        {
            if (innerLight != null)
            {
                innerLight.intensity = LightIntensity;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;
        
        Debug.Log($"{name} catched");
        Debug.Log($"{gameObject.name} catched");
        Destroy(gameObject);
        
        // add player glowing effect
        // ticking glowing effect
        // it must kill the player and reload the level

    }
}
