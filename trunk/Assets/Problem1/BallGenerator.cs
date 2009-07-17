using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    public int numberOfBalls = 20;
    public GameObject ball = null;
    public Color[] possibleBallColors = { Color.red, Color.green, Color.blue };
    public string[] colorNames = { "Rojo", "Verde", "Azul" };
    public int[] colorsValue = { 3, 5, 7 };	
	int levelScore = 0;
    public float minutesToPlay = 3;
    private GameObject[] balls;
    [HideInInspector] public int totalSum = 0;
    private string stringToEdit = "";
    float totalTime = 0.001f;
	
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
		GameObject go = GameObject.Find("PointsManagerBehaviour");
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
				mg.totalScore += partialPoints;
			} // End if.
			
			if(pmb != null)
			{
				pmb.incrementPoints(partialPoints);
			} // End if.
			
            Reset2();
        }
    
    }


    void Update()
    {
        totalTime += Time.deltaTime;

        if (totalTime >= minutesToPlay * 60)
        {
			if (pmb != null)
			{
				pmb.incrementLevelsCompleted(1);
			} // End if.
            // Change level
			Application.LoadLevel("RunScreen");
        }
        
    }
}