using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float speed = 3;
    [SerializeField] float maxAngle = 45;

    float currenAngle = 0;
    float directionFactor = 1;

    void Start()
    {
        currenAngle = 0;
    }

    void Update()
    {
        float deltaAngle = speed * Time.deltaTime * directionFactor;

        if (Mathf.Abs(currenAngle + deltaAngle) > maxAngle)
        {
            directionFactor *= -1;
        }
        else
        {
            currenAngle += deltaAngle;
            transform.Rotate(0, 0, deltaAngle);
        }
    }
}
