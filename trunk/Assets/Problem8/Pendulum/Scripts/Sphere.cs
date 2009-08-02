using UnityEngine;

public class Sphere : MonoBehaviour
{
    public float energy;
    private float myTime = 0f;
    private bool initialized = false;
    private float initialPositionY;
    void FixedUpdate()
    {
        if (!initialized && Time.time > 0.1f)
        {
            this.initialPositionY = this.transform.position.y;
            this.rigidbody.velocity = new Vector3(0.1f, 0);
            updateVelocity();
            initialized = true;
        }
 

        myTime += Time.deltaTime;
        if (myTime > 0.1f && this.transform.position.y - this.initialPositionY < 0.01)
        {
            updateVelocity();
        }

        

    }
    void updateVelocity()
    {
        float m = this.rigidbody.mass;
        float g = Physics.gravity.magnitude;
        float h = this.transform.position.y - this.initialPositionY;
        float wnc = 0.5f * m * this.rigidbody.velocity.sqrMagnitude +
            m * g * h - energy;
        

        //float dif = Mathf.Abs(this.rigidbody.velocity.x - 2f * (energy - wnc - m * g * h) / m);

        this.rigidbody.velocity = new Vector3(Mathf.Sqrt(Mathf.Abs( 2f * (m * this.rigidbody.velocity.sqrMagnitude + 
            m * g * h - energy) / m)) *
            Mathf.Sign(this.rigidbody.velocity.x), 0);
        myTime = 0;
        
    }

}