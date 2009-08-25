using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestPoints : MonoBehaviour
{
    public string holder = null;
    private PointsManagerBehaviour pmb = null;

    void Start()
    {
        GameObject go = GameObject.Find(holder);
        if (go != null)
        {
            pmb = ((PointsManagerBehaviour)go.GetComponent("PointsManagerBehaviour"));
            if (pmb == null)
            {
                print("PointsManagerBehaviour not found!");
            }
        }
        else
        {
            print("GameManager not found!");
        }

        pmb.setPoints("contar", 100);
        pmb.setPoints("balanza", 70);
        pmb.setPoints("balanza avanzada", 80);
        pmb.setPoints("suma cromatica", 220);
        pmb.setPoints("identificacion cromatica", 72);
        pmb.setPoints("capacidad de respuesta", 20);
        pmb.setPoints("capacidad de respuesta avanzada", 20);
        pmb.setPoints("esferas y cadenas", 450);

        return;
    }



}