using UnityEngine;
using System.Collections;


public class FactBehaviour : MonoBehaviour {

    public string gameToLoad;
	
	// Use this for initialization
	void Start () {
	
	}
	
	public void OnMouseDown(){
        // TODO - Setear el currentGame para la descripción.
        GameObject go = GameObject.Find("GameManager");
        GamesMapper mapper = new GamesMapper();

        if (go != null)
        {
            PointsManagerBehaviour pManager = ((PointsManagerBehaviour)go.GetComponent("PointsManagerBehaviour"));
            if (pManager != null)
            {
                pManager.setCurrentGame(mapper.getGameNumber(gameToLoad));
            }
        }

        Application.LoadLevel("GameDescription");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
