using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    [SerializeField] float time = 4;
    bool isGravityEnabled = false;
    Rigidbody _rigidbody;
    MeshRenderer _meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody.useGravity = false;
        _meshRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGravityEnabled && Time.timeSinceLevelLoad > time)
        {
            _rigidbody.useGravity = true;
            _meshRenderer.enabled = true;
            isGravityEnabled = true;
        }
    }
}
