using UnityEngine;
using System;

public class Pendulum : MonoBehaviour
{
    public GameObject ball;
    public GameObject link;
    
    public int numberOfLinks = 5;
    public float ballRadius = 0.5f;

    [HideInInspector] public GameObject chainGroup;
    [HideInInspector] public GameObject[] chain;

    private void join(GameObject g1, GameObject g2)
    {
        ((Joint)g1.GetComponent("Joint")).connectedBody = g2.rigidbody;
    }

    public void setEnabled(bool b)
    {
        ball.renderer.enabled = true;

        for (int i = 0; i < chain.Length; i++)
            chain[i].renderer.enabled = true;
    }


    void Start()
    {
        if (numberOfLinks < 1 || ball == null || link == null)
            throw new System.ArgumentException("Invalid Arguments provided");

        float distance = Vector3.Distance(ball.transform.position, link.transform.position) - ballRadius;

        chain = new GameObject[numberOfLinks];
        link.name = "link0";
        chain[0] = link;

        chainGroup = new GameObject("Chain");
        chainGroup.transform.position = this.transform.position;

        chainGroup.transform.parent = this.transform;
        link.transform.parent = chainGroup.transform;
        Vector3 newRotation = new Vector3(0, Mathf.PI/2);

        for (int i = 1; i < numberOfLinks; i++)
        {
            Vector3 newPosition = link.transform.position + Vector3.down * (distance / numberOfLinks) * i;


            chain[i] = (GameObject)Instantiate(link, newPosition, Quaternion.EulerAngles(newRotation));
            chain[i].name = "link" + i;
            chain[i].rigidbody.isKinematic = false;
            Destroy(chain[i].GetComponent("Rotate"));

            chain[i].transform.parent = link.transform.parent;

            join(chain[i], chain[i-1]);

            newRotation.y += Mathf.PI / 2;
        }

        join(ball, chain[chain.Length - 1]);

        this.setEnabled(true);
    }


    public void Cut()
    {
        ball.transform.parent = this.transform.parent;
        Destroy(ball.GetComponent("Sphere"));
        Destroy(this.gameObject);
    }

    private void apply(string op)
    {
        foreach (GameObject g in chain)
        {
            if ( g != null )
                g.SendMessage(op);
        }
    }

    public void Hover()
    {
        apply("Hover");
    }

    public void UnHover()
    {
        apply("UnHover");
    }

    public void SavePreviousColor()
    {
        apply("SavePreviousColor");
    }
}