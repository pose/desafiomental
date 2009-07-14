using UnityEngine;
using System.Collections;

public class MainScreenBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void OnMouseDown(){
		GameObject pm;
		PointsManagerBehaviour a;
		Application.LoadLevel("DescriptionScreen");
		pm = GameObject.Find("GameManager");
		
		a = (PointsManagerBehaviour)pm.GetComponent("PointsManagerBehaviour");
		
		a.setPoints(290);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
