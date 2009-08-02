using UnityEngine;

public class RotatingBox : MonoBehaviour
{
    public Vector3 rotateAxis = new Vector3(0, 0, 1);
    public float angle = Mathf.PI / 36;
    void FixedUpdate()
    {
        this.transform.RotateAroundLocal(rotateAxis, angle);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("hi");
        other.rigidbody.isKinematic = false;
        other.rigidbody.velocity = new Vector3();
        other.rigidbody.useGravity = false;
        
    }
}