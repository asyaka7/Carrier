using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float distance = 3;
    [SerializeField] float delay = 0.2f;
    [SerializeField] AnimationCurve curve;

    Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        float time = Time.time - delay;
        if (time < 0) return;

        float factor = curve.Evaluate(time);
        
        float d = factor * distance;
        float dPosition = originalPosition.y + d - transform.localPosition.y;
        Vector3 newDirection = new Vector3(0, dPosition, 0);
        transform.Translate(newDirection, Space.Self);

    }
}
