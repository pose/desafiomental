using UnityEngine;

public class Sphere : MonoBehaviour
{
    public float amp = 5f;
    public float freq = 5f;
    public float offset = 0;
    void FixedUpdate()
    {
    /*    if (!initialized && Time.time > 0.1f)
        {
            this.initialPositionY = this.transform.position.y;
            this.rigidbody.velocity = new Vector3(0.1f, 0);
            updateVelocity();
            initialized = true;
        }
 

        myTime += Time.deltaTime;
        if (myTime > 1f )
        {
            updateVelocity();
        }

      */
        this.rigidbody.velocity = new Vector3(amp * Mathf.Cos(Time.time*freq + offset), 0f);

    }
   

}