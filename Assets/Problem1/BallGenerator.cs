using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    public int numberOfBalls = 20;
    public AudioSource song;
    public GameObject ball = null;
    public Color[] possibleBallColors = { Color.red, Color.green, Color.blue };
    public string[] colorNames = { "Rojo", "Verde", "Azul" };
    public int[] colorsValue = { 3, 5, 7 };	
	int levelScore = 0;
    public const float minutesToPlay = 1;
    private GameObject[] balls;
    [HideInInspector] public int totalSum = 0;
    private string stringToEdit = "";
    float totalTime = 0.001f;
	float timeRemaining = minutesToPlay * 60;
	
	PointsManagerBehaviour pmb = null;
	MiniGamesGUI mg = null;
    
    void Start()
    {
        
        if (numberOfBalls < 1)
            throw new System.ArgumentException("numberOfBalls can't be less than 1");

        if ( ball == null || colorsValue == null )
            throw new System.ArgumentNullException("ball or colorsValue can't be null");

        if (colorsValue.Length != possibleBallColors.Length)
            throw new System.ArgumentException("colorsValue and possibleBallColors can't have" +
                " different Length");

        int randomAcum = 0;
        balls = new GameObject[numberOfBalls];
        for (int i = 0; i < numberOfBalls; i++)
        {
            randomAcum += Random.RandomRange(1, 10);
            
            int randomPosition = Random.Range(0, possibleBallColors.Length );
            GameObject g = (GameObject) Instantiate(ball, 
                                new Vector3(randomAcum % 10 - 5, 0, randomAcum / 10 - 5), Quaternion.identity);

            g.renderer.material.color = possibleBallColors[randomPosition];
            totalSum += colorsValue[randomPosition];
            balls[i] = g;
        }

		long totalPoints = 0;
		GameObject go = GameObject.Find("GameManager");
		if (go != null)
		{
			pmb = ((PointsManagerBehaviour)go.GetComponent("PointsManagerBehaviour"));
			totalPoints = pmb.getPoints();
		} // End if.
		
		GameObject mggo = GameObject.Find("MiniGamesGUI");
		if (mggo != null)
		{
			mg = ((MiniGamesGUI)mggo.GetComponent("MiniGamesGUI"));
			mg.totalScore = totalPoints;
		} // End if.

        if (!song.isPlaying)
            song.Play();



    }

    void Reset2()
    {
        for (int i = 0; i < balls.Length; i++)
            Destroy(balls[i]);

        stringToEdit = "";
        totalSum = 0;
        Start();
    }

    
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(500, 50, 220, 300));


        GUILayout.BeginVertical();
        GUILayout.Box("Cantidad de esferas:", GUILayout.Width(220));
        for(int i = 0; i < colorNames.Length; i++ )
        {
            GUILayout.BeginHorizontal();
            GUILayout.Box(colorNames[i], GUILayout.Width(100));
            GUILayout.Box(colorsValue[i].ToString());
            GUILayout.EndHorizontal();
        }
        
        stringToEdit = GUILayout.TextField(stringToEdit);
        GUILayout.EndVertical();

        GUILayout.EndArea();

        int val;

        try
        {
            val = int.Parse(stringToEdit);
        }
        catch (System.Exception e) 
        {
            val = 0;
        }


        if (val  == totalSum)
        {
            Debug.Log("Win!");
			int partialPoints = (int) (150 / totalTime);
            Debug.Log(partialPoints);
			
			if(mg != null)
			{
				mg.PartialWin();
				levelScore = (int)(mg.levelScore += (float)partialPoints);
				mg.totalScore += (float)partialPoints;
			} // End if.
			
			if(pmb != null)
			{
				print("PGM INCREMENTED");
				pmb.incrementPoints(partialPoints);
			} // End if.
			
            Reset2();
        }
    
    }

    Color color1 = new Color(0,0.6f,0,0);
	Color color2 = new Color(0.6f,0,0,0);	
	Color color3 = new Color(0,0,0.6f,0);
	Color color4 = new Color(0.75f,0.75f,0.75f,0);
	Color color5 = new Color(0.2f,0.2f,0.2f,0);
	int pingPongStep = 0;
	float duration = 15.0f;
	
    private float timeMoved = 0.0f;
    private Vector3 direction = new Vector3(0.01f, 0);
    private Vector3 rotation = new Vector3(0.02f, 0);
    void Update()
    {
        float t;
        if (Time.time < duration)
        {
            t = Mathf.PingPong(Time.time, duration) / duration;
            print(t);
            Camera.main.backgroundColor = Color.Lerp(color1, color2, t);
        }
        else if (Time.time < 2 * duration)
        {
            t = Mathf.PingPong(Time.time - duration, duration) / duration;
            Camera.main.backgroundColor = Color.Lerp(color2, color3, t);
        }
        else if (Time.time < 3 * duration)
        {
            t = Mathf.PingPong(Time.time - 2 * duration, duration) / duration;
            Camera.main.backgroundColor = Color.Lerp(color3, color4, t);
        }
        else if (Time.time < 4 * duration)
        {
            t = Mathf.PingPong(Time.time - 3 * duration, duration) / duration;
            Camera.main.backgroundColor = Color.Lerp(color4, color5, t);
        }


        GameObject camera = GameObject.Find("DirectionalLightCamera");
        if (camera != null)
        {
            if (timeMoved > 3f)
            {
                timeMoved = 0f;
                direction = -direction;
                rotation = -rotation;
            }
            timeMoved += Time.deltaTime;
            camera.transform.Translate(new Vector3(0.01f * Mathf.Sin(Time.time-Mathf.PI/2f), 0.01f * Mathf.Sin(Time.time)));
            camera.transform.Rotate(new Vector3(0.01f * Mathf.Sin(Time.time+Mathf.PI/2f), 0.01f * Mathf.Sin(Time.time)));
        }
		timeRemaining -= Time.deltaTime;
		mg.updateCronometer(timeRemaining);
		
        totalTime += Time.deltaTime;

        if (totalTime >= minutesToPlay * 60)
        {
			if (pmb != null)
			{
				pmb.incrementLevelsCompleted(1);
			} // End if.
            // Change level
            GameObject go = GameObject.Find("GameManager");
            GamesMapper mapper = new GamesMapper();

            if (go != null)
            {
                PointsManagerBehaviour pManager = ((PointsManagerBehaviour)go.GetComponent("PointsManagerBehaviour"));
                if (pManager != null)
                {
                    pManager.setCurrentGame(mapper.getGameNumber("IdentificacionCromatica"));
                }
            }
            song.Stop();
			Application.LoadLevel("GameDescription");
        }
        
    }
}