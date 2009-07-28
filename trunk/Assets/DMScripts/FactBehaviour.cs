using UnityEngine;
using System.Collections;


public class FactBehaviour : MonoBehaviour {

    public string gameToLoad;
    private const int AUDIO_CODE = 1;
    private AudioBehaviour aManager = null;
	// Use this for initialization
	void Start () {
        GameObject go = GameObject.Find("GameManager");

        if (go != null)
        {
            aManager = ((AudioBehaviour)go.GetComponent("AudioBehaviour"));
            if (aManager != null)
            {
                aManager.setAudio(AUDIO_CODE);
                aManager.playAudio();
            }
        }
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
