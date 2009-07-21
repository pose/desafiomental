using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PointsManagerBehaviour : MonoBehaviour {

	long points = 0;
    int levelsCompleted = 0;
    Dictionary<string, long> gamePoints = null;
    int currentGame = 0;

	// Use this for initialization

	void Start () {
		DontDestroyOnLoad(this);
        gamePoints = new Dictionary<string, long>();
		points = 0;
	}
	
    public void setCurrentGame( int gNumber ){

        this.currentGame = gNumber;
    }

    public int getCurrentGame()
    {

        return this.currentGame;
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

    public long setPoints(string game, long points)
    {
        this.points = points;
        GameObject go = GameObject.Find("MiniGamesGUI");
        try
        {
            gamePoints.Add(game, points);
        }
        catch (KeyNotFoundException)
        {
            Debug.Log("No se encontró el juego correspondiente a " + game);
        }
        
        if (go != null)
        {
            MiniGamesGUI mgGUI = ((MiniGamesGUI)go.GetComponent("MiniGamesGUI"));
            if (mgGUI != null)
                mgGUI.totalScore += points;
        }

        return getPoints(game);
    }

	public long getPoints() {
		return this.points;
	}

    public long getPoints(string game)
    {
        return this.gamePoints[game];
    }
	
	public long incrementPoints(long inc) {
		setPoints(getPoints() + inc);
		return getPoints();
	}
	
	public long decrementPoints(long dec) {
		return incrementPoints( -dec );
	}

    public long incrementPoints(string game, long inc)
    {
        setPoints(game, getPoints() + inc);
        return getPoints(game);
    }

    public long decrementPoints(string game, long dec)
    {
        return incrementPoints(game, -dec);
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
