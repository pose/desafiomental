using UnityEngine;

public class TargetCollider : MonoBehaviour
{
    public GameObject collideTarget;
    public Color pairColor = Color.red;
    public bool destroyOnCorrectHit = true;
    

    void Start()
    {
        if (collideTarget == null)
            throw new System.ArgumentException("Invalid Arguments provided");

        this.renderer.material.color = pairColor;
        collideTarget.renderer.material.color = pairColor;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == null || collideTarget == null)
            return; 

        if (collideTarget.collider == other)
        {
            Destroy(other.gameObject);
            this.transform.parent.SendMessage("Complete", this.gameObject);
            if (destroyOnCorrectHit)
                Destroy(gameObject);

        }
        else
        {
            /*TODO: Lose game*/
            
        }
    }
}