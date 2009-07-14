using UnityEngine;
using System.Collections;


public class ContinueButtonBehaviour : MonoBehaviour {

	
	// Use this for initialization
	void Start () {
	
	}
	
	public void OnMouseDown(){
        /*
		GameObject pm = GameObject.Find("PointManager");
		PointsManagerBehaviour a = (PointsManagerBehaviour)pm.GetComponent("PointsManagerBehaviour");
		Debug.Log(Input.mousePosition.this[0] + " " + Input.mousePosition.this[1] + " " + Input.mousePosition.this[2]);
		Debug.Log( "Puntos: " + a.getPoints() );
		if( Input.mousePosition.x >= X_MIN && Input.mousePosition.x <= X_MAX &&
				Input.mousePosition.y >= Y_MIN && Input.mousePosition.y <= Y_MAX ){
					Debug.Log("adentrooo");
			Application.LoadLevel("RunScreen");
		}
		else
			Debug.Log("afuera");*/

        Application.LoadLevel("RunScreen");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
