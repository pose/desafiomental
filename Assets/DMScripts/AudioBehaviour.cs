using UnityEngine;
using System.Collections;

public class AudioBehaviour : MonoBehaviour {
	
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
            screenAudio.Play();
        }	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
