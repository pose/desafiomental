using UnityEngine;
using System.Collections;

public class PointsManagerBehaviour : MonoBehaviour {

	long points;
    int levelsCompleted = 0;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this); 
		points = 0;
	}
	
	public long setPoints(long p) {		
		this.points = p;
		GameObject go = GameObject.Find("MiniGamesGUI");
		if (go != null)
		{
			MiniGamesGUI mgGUI = ((MiniGamesGUI)go.GetComponent("MiniGamesGUI"));
			if (mgGUI != null )
				mgGUI.totalScore = p;
		}		
		return getPoints();
	}
	
	public long getPoints() {
		return this.points;
	}
	
	public long incrementPoints(long inc) {
		setPoints(getPoints() + inc);
		return getPoints();
	}
	
	public long decrementPoints(long dec) {
		return incrementPoints( -dec );
	}

    public int setLevelsCompleted(int p) {

        this.levelsCompleted = p;

        return getLevelsCompleted();
    }

    public int getLevelsCompleted() {
        return this.levelsCompleted;
    }

    public int incrementLevelsCompleted(int inc) {
        setLevelsCompleted(getLevelsCompleted() + inc);
        return getLevelsCompleted();
    }

    public int decrementLevelsCompleted(int dec) {
        return incrementLevelsCompleted(-dec);
    }

    public bool gameOver() {
        return getLevelsCompleted() == 5;
    }

	// Update is called once per frame
	void Update () {	
	}
}
