using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResultsBehaviour : MonoBehaviour{

    public int numberOfCapacity = 0;
    private PointsManagerBehaviour pmb = null;

    void Start()
    {
        GameObject go = GameObject.Find("GameManager");
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

        switch (numberOfCapacity)
        {
            case 0:
                calculateMentalCapacity();
                break;
            case 1:
                calculateConcentrationCapacity();
                break;
            case 2:
                calculateReactionCapacity();
                break;
            case 3:
                calculateReasonCapacity();
                break;
            default:
                Debug.Log("Capacidad desconocida.");
                break;
        }

        return;
    }

    private void calculateReasonCapacity()
    {
        double result;
        /*
         * 0.3 * Puntaje(Balanza)/Maximo(Balanza) + 0.7 * Puntaje(Cadenas y esferas)/Maximo(Cadenas y esferas)
         */

        result = 0.3 * pmb.getPoints("Balanza") / pmb.getMaxPoints("Balanza") 
            + 0.7 * pmb.getPoints("CadenasEsferas") / pmb.getMaxPoints("CadenasEsferas");

        setLabel("ReasonCapacityPoints", ((int)(result * 100)).ToString());

    }

    private void calculateReactionCapacity()
    {
        double result;
        /*
         * 0.25 * Puntaje(Capacidad de respuesta)/Maximo(Capacidad de respuesta) 
         *      + 0.75 * Puntaje(Capacidad de respuesta AVANZADA)/Maximo(Capacidad de respuesta AVANZADA)
         */

        result = 0.25 * pmb.getPoints("CapacidadDeRespuesta") / pmb.getMaxPoints("CapacidadDeRespuesta")
            + 0.75 * pmb.getPoints("CapacidadDeRespuestaAvanzada") / pmb.getMaxPoints("CapacidadDeRespuestaAvanzada");

        setLabel("ReactionCapacityPoints", ((int)(result * 100)).ToString());

    }

    private void calculateConcentrationCapacity()
    {
        double result;
        /*
         * 0.3 * Puntaje(Identificación cromática)/Maximo(Identificación cromática) 
         *    + 0.3 * Puntaje(Cuenta)/Maximo(Cuenta) + 0.3 * Puntaje(Balanza AVANZADA)/Maximo(Balanza AVANZADA)         
         */

        result = 0.4 * pmb.getPoints("IdentificacionCromatica") / pmb.getMaxPoints("IdentificacionCromatica")
                    + 0.6 * pmb.getPoints("BalanzaAvanzada") / pmb.getMaxPoints("BalanzaAvanzada");

        setLabel("ConcentrationCapacityPoints", ((int)(result * 100)).ToString());

    }

    private void calculateMentalCapacity()
    {
        double result;
        /*
         * Puntaje(Suma cromática)
          */

        result = 1.0 * pmb.getPoints("SumaCromatica") / pmb.getMaxPoints("SumaCromatica");

        Debug.Log((result * 100));

        setLabel("MentalCalculationPoints", ((int)(result * 100)).ToString());

    }

    private void setLabel( string capacity, string text ){
        GameObject go = GameObject.Find(capacity);
        if (go != null)
        {
            GUIText guiText = ((GUIText)go.GetComponent("GUIText"));
            if (guiText != null)
                guiText.text = text + " %";
        }
    }

}