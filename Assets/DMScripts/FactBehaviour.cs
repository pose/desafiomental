using UnityEngine;
using System.Collections;


public class FactBehaviour : MonoBehaviour {

	public string firstGame;
	
	// Use this for initialization
	void Start () {
	
	}
	
	public void OnMouseDown(){
        Application.LoadLevel(firstGame);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
