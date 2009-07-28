using UnityEngine;
using System.Collections;


public class YearButtonBehaviour : MonoBehaviour {

	public int year;
    public AudioSource screenAudio;
	// Use this for initialization
	void Start () {

	}
	
	public void OnMouseDown(){
        GameObject go = GameObject.Find("GameManager");
        GamesMapper mapper = new GamesMapper();

        if (go != null)
        {
            AudioBehaviour audioManager = ((AudioBehaviour)go.GetComponent("AudioBehaviour"));
            if (audioManager != null)
            {
                audioManager.screenAudio.Stop();
            }
        }
        Application.LoadLevel("ITBAFact" + year  + "Screen");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
