using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PointsManagerBehaviour : MonoBehaviour {

	private long points = 0;
    private int levelsCompleted = 0;
    private Dictionary<string, long> gamePoints = null;
    private Dictionary<string, long> gameMaxPoints = null;
    private int currentGame = 0;

	// Use this for initialization
	public PointsManagerBehaviour(){
		Start();
	}
	void Start () {
        DontDestroyOnLoad(this);
        gamePoints = new Dictionary<string, long>();
        gameMaxPoints = new Dictionary<string,long>();
        
        gameMaxPoints.Add( "suma cromatica", 300 );
        gameMaxPoints.Add( "balanza", 75 );
        gameMaxPoints.Add( "balanza avanzada", 100 ); //Revisar y modificar de ser necesario.
        gameMaxPoints.Add( "contar", 100 ); //Revisar y modificar de ser necesario.
		gameMaxPoints.Add( "esferas y cadenas", 450 );
        gameMaxPoints.Add( "identificacion cromatica", 75 );
        gameMaxPoints.Add( "capacidad de respuesta", 45 );
        gameMaxPoints.Add( "capacidad de respuesta avanzada", 45 );


        points = 0;
	}

    public string getAllGames()
    {
        return gamePoints.Keys.ToString();
    }
	
    public long getMaxPoints( string game ){
        return gameMaxPoints[game];
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
            // Si ya estaba agregado el juego
            if (!gamePoints.ContainsKey(game))
            {
                gamePoints.Add(game, points);
            }
            else
            {
                gamePoints[game] = points;
            }
            
            
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
