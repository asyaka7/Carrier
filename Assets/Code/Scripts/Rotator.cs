using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float speed = 3;
    [SerializeField] float maxAngle = 45;

    float currenAngle = 0;
    float directionFactor = 1;

    // Start is called before the first frame update
    void Start()
    {
        currenAngle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float deltaAngle = speed * Time.deltaTime * directionFactor;
        currenAngle += deltaAngle;
        transform.Rotate(0, 0, deltaAngle);

        if (Mathf.Abs(currenAngle) > maxAngle)
        {
            //currenAngle = 0;
            directionFactor *= -1;
        }
    }
}
