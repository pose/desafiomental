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
	/*
     * public PointsManagerBehaviour(){
		Start();
	}*/
	void Start () {
        DontDestroyOnLoad(this);
        gamePoints = new Dictionary<string, long>();
        gameMaxPoints = new Dictionary<string,long>();
        
        gameMaxPoints.Add( "SumaCromatica", 100 );
        gameMaxPoints.Add( "Balanza", 75 );
        gameMaxPoints.Add( "BalanzaAvanzada", 65 );
        gameMaxPoints.Add( "Cuenta", 100 ); //Deprecated :P
        gameMaxPoints.Add( "CadenasEsferas", 450);
        gameMaxPoints.Add( "IdentificacionCromatica", 75 );
        gameMaxPoints.Add( "CapacidadDeRespuesta", 45 );
        gameMaxPoints.Add( "CapacidadDeRespuestaAvanzada", 45);

        setPoints("SumaCromatica", 0);
        setPoints("Balanza", 0);
        setPoints("BalanzaAvanzada", 0);
        setPoints("CadenasEsferas", 0);
        setPoints("IdentificacionCromatica", 0);
        setPoints("CapacidadDeRespuesta", 0);
        setPoints("CapacidadDeRespuestaAvanzada", 0);

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
        long totalDiference = 0;
        
        try
        {
            // Si no estaba agregado el juego, lo agrego. Si
            // estaba agregado le actualizo los puntos y actualizo el total 
            // para que se incremente o decremente en la diferencia.

            if (!gamePoints.ContainsKey(game))
            {
                gamePoints.Add(game, points);
                totalDiference = points;
            }
            else
            {
                totalDiference = points - gamePoints[game];
                gamePoints[game] = points;
            }

            setPoints(this.points + totalDiference);
            
        }
        catch (KeyNotFoundException)
        {
            Debug.Log("No se encontró el juego correspondiente a " + game);
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
        setPoints(game, getPoints(game) + inc);
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
