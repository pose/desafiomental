using UnityEngine;
using System.Collections.Generic;

public class AudioBehaviour : MonoBehaviour {
	
	public AudioSource[] sources;
    private AudioSource currentAudio;

	// Use this for initialization
	void Start () {
        setAudio(0);
        playAudio();

	}

    public void setAudio(int audioCode)
    {
        currentAudio = sources[audioCode];
    }

    public void playAudio()
    {
        if (currentAudio != null)
        {
            currentAudio.Play();
        }
    }

    public void stopAudio()
    {
        if (currentAudio != null)
        {
            currentAudio.Stop();
        }
    }
	// Update is called once per frame
	void Update () {


	}
}
