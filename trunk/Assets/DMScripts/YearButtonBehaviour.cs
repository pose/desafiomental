using UnityEngine;
using System.Collections;


public class YearButtonBehaviour : MonoBehaviour {

	public int year;
    public AudioSource screenAudio;
	// Use this for initialization
	void Start () {
        if (screenAudio != null)
        {
            screenAudio.Play();
        }
	}
	
	public void OnMouseDown(){
        if (screenAudio != null)
        {
            screenAudio.Stop();
        }
        Application.LoadLevel("ITBAFact" + year  + "Screen");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
