using UnityEngine;
using System.Collections;

public class MainBehaviour : MonoBehaviour {
	
	
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
                audioManager.stopAudio();
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
