using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationAngle = 2f;
    public float rotationsPerCycle = 5f;
    public Vector3 rotationVector = new Vector3(0, 0, 1);

    void FixedUpdate()
    {
        float deltaAngle = rotationAngle * Mathf.Cos(Time.time * rotationsPerCycle)* Time.fixedDeltaTime;
        this.transform.RotateAround(rotationVector, deltaAngle);
    }
}