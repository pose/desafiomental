using UnityEngine;
using System.Collections;


public class YearButtonBehaviour : MonoBehaviour {

	public int year;
	// Use this for initialization
	void Start () {
	
	}
	
	public void OnMouseDown(){
        Application.LoadLevel("ITBAFact" + year  + "Screen");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
