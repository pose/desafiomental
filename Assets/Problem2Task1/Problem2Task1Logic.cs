using UnityEngine;
using System.Collections;

public class Problem2Task1Logic : MonoBehaviour
{
	const float TOTAL_TIME = 60.0f;	

    public GameObject[] cubePrefab;
    public int numberOfCubes;
    public GUIStyle style;
	public AudioSource backgroundMusic;

    GameObject[] cubes;
    GameObject[] displayedCubes;
    int currentLevel;
    bool replaceScreenObjs;
    ScriptCube scriptDisplayedCube;
    float timeCounter;
    float timeElapsed;
    float totalTimeCounter;
	float timeRemaining = TOTAL_TIME;

    int signColorIndex;
    float signColorR, signColorG, signColorB;
    string signText;
	
	PointsManagerBehaviour pmb = null;
	MiniGamesGUI mg = null;

    // Use this for initialization
    void Start()
    {
		print("START!");
		if(!backgroundMusic.isPlaying)
		{
			backgroundMusic.Play();
		}
		else
		{
			print("PLAYING");
		}
		
        // Instantiate a copy of every kind of cube and put them in a galaxy far, far away.
        cubes = new GameObject[8];
        for (int i = 0; i < numberOfCubes; i++)
        {
            cubes[i] = Object.Instantiate(cubePrefab[i]) as GameObject;
            cubes[i].transform.position = new Vector3(-10000, -10000, -10000);
        }
		
		cubes[0].active = false;

        displayedCubes = new GameObject[4];
        currentLevel = 0;
        replaceScreenObjs = true;
        totalTimeCounter = 0;
				
		long totalPoints = 0;
		GameObject go = GameObject.Find("GameManager");
		if (go != null)
		{
			pmb = ((PointsManagerBehaviour)go.GetComponent("PointsManagerBehaviour"));
			if(pmb != null)
			{
				totalPoints = pmb.getPoints();
				print("Total points: " + totalPoints);
			} // End if.
		} // End if.
		else
		{
			print("PointsManagerBehaviour not found!");
		} // End else.
		
		GameObject mggo = GameObject.Find("MiniGamesGUI");
		if (mggo != null)
		{
			mg = ((MiniGamesGUI)mggo.GetComponent("MiniGamesGUI"));
			if(mg != null)
			{
				mg.totalScore = totalPoints;
				mg.setChronometerPrefix("TIEMPO RESTANTE:\n     ");
				mg.setPartialWinString("   ¡CORRECTO!");
				mg.setPartialLoseString(" ¡INCORRECTO!");
			} // End if.
		} // End if.
		else
		{
			print("MiniGamesGUI not found!");
		} // End else.
    }

    void OnGUI()
    {
        style.normal.textColor = new Color(signColorR, signColorG, signColorB, 1.0f);
        GUI.Label(new Rect(320, 110, 500, 60), signText, style);
    }

	Color color1 = new Color(0,0.6f,0,0);
	Color color2 = new Color(0.6f,0,0,0);	
	Color color3 = new Color(0,0,0.6f,0);
	Color color4 = new Color(0.75f,0.75f,0.75f,0);
	Color color5 = new Color(0.2f,0.2f,0.2f,0);
	int pingPongStep = 0;
	float duration = 15.0f;
	
    // Update is called once per frame
    void Update()
    {
		float t;
		if(totalTimeCounter < duration)
		{
			t = Mathf.PingPong (totalTimeCounter, duration) / duration;
			print(t);
			Camera.main.backgroundColor = Color.Lerp(color1,color2,t);
		}
		else if(totalTimeCounter < 2*duration)
		{
			t = Mathf.PingPong (totalTimeCounter-duration,duration) / duration;
			Camera.main.backgroundColor = Color.Lerp(color2,color3,t);
		}
		else if(totalTimeCounter < 3*duration)
		{
			t = Mathf.PingPong (totalTimeCounter-2*duration,duration) / duration;
			Camera.main.backgroundColor = Color.Lerp(color3,color4,t);
		}
		else if(totalTimeCounter < 4*duration)
		{
			t = Mathf.PingPong (totalTimeCounter-3*duration,duration) / duration;
			Camera.main.backgroundColor = Color.Lerp(color4,color5,t);
		}
		
		
        if (replaceScreenObjs)
        {
            replaceScreenObjs = false;
            currentLevel++;
//			print(currentLevel);
            assignDisplayedCubes();
        }

		timeRemaining -= Time.deltaTime;		
		mg.updateCronometer(timeRemaining);
		
        totalTimeCounter += Time.deltaTime;
        if (totalTimeCounter > TOTAL_TIME)
        {
			backgroundMusic.Stop();
			// CARGA DE LA ESCENA
            GameObject go = GameObject.Find("GameManager");
            GamesMapper mapper = new GamesMapper();

            if (go != null)
            {
                PointsManagerBehaviour pManager = ((PointsManagerBehaviour)go.GetComponent("PointsManagerBehaviour"));
                if (pManager != null)
                {
                    pManager.setCurrentGame(mapper.getGameNumber("CapacidadDeRespuesta"));
                }
            }
            Application.LoadLevel("GameDescription");
        }
        timeCounter += Time.deltaTime;
        
        if (timeCounter < 0.5f)
            return;

        timeElapsed += Time.deltaTime;

        RaycastHit[] hits;
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hits = Physics.RaycastAll(ray);
            int index = 0;
            float dist = 0;
            if (hits.Length > 0)
            {
                dist = hits[0].distance;
                for (int i = 1; i < hits.Length; i++)
                {
                    if (dist > hits[i].distance)
                    {
                        dist = hits[i].distance;
                        index = i;
                    }
                }

                ScriptCube scriptCubeHit = (ScriptCube)hits[index].transform.gameObject.GetComponent("ScriptCube");
                if (scriptCubeHit == scriptDisplayedCube)
                {
                    // Don't let the player cheat!
                    return;
                }

//				print(scriptDisplayedCube.getColorName() + " " + scriptCubeHit.getColorName());
                if (scriptDisplayedCube.getColorName() == scriptCubeHit.getColorName())
                {
                    //Object.Destroy(hits[index].transform.gameObject);
					int i;
                    for(i=0;i<4;i++)
                    {
                        Object.Destroy(displayedCubes[i]);
                    }
					for(i=0;i<8;i++)
					{
						Object.Destroy(cubes[i]);
					}
//                    print("Response time: " + timeElapsed + " msecs.");
                    replaceScreenObjs = true;
					
					// Add the score.
					if(mg != null)
					{
						mg.PartialWin();
						mg.levelScore += 5.0f;
						mg.totalScore += 5.0f;
					} // End if.
					
					if(pmb != null)
					{
						pmb.incrementPoints(5);
					} // End if.
                }
				else
				{
					mg.PartialLose();
				} // End else.
            }
        }
    }

    protected void assignDisplayedCubes()
    {
		int i;
		int tempVal;
		
		// Determine the main three colors that will be used.
		ArrayList usedColors = new ArrayList();
		print("Used colors: ");
		for(i=0;i<3;i++)
		{
			do
			{
				tempVal = Random.Range(0,numberOfCubes);
			} while(usedColors.Contains(tempVal));
			print(tempVal);
			usedColors.Add(tempVal);
		}
		
		// Determine the two that will be used as the main and sign color, respectively.
		int mainColorIndex = (int)usedColors[Random.Range(0,3)];
		int signColorIndex;
		do
		{
			signColorIndex = (int)usedColors[Random.Range(0,3)];
		} while(signColorIndex==mainColorIndex);		
		
		signColorR = ((ScriptCube)cubePrefab[signColorIndex].GetComponent("ScriptCube")).fColorR;
        signColorG = ((ScriptCube)cubePrefab[signColorIndex].GetComponent("ScriptCube")).fColorG;
        signColorB = ((ScriptCube)cubePrefab[signColorIndex].GetComponent("ScriptCube")).fColorB;
		
		displayedCubes[0] = Object.Instantiate(cubePrefab[mainColorIndex]) as GameObject;
        scriptDisplayedCube = (ScriptCube)displayedCubes[0].GetComponent("ScriptCube");
        signText = scriptDisplayedCube.colorNameSP;		
		
		int index;
		index = ((int)(usedColors[0])); displayedCubes[1] = Object.Instantiate(cubePrefab[index]) as GameObject;
		index = ((int)(usedColors[1])); displayedCubes[2] = Object.Instantiate(cubePrefab[index]) as GameObject;
		index = ((int)(usedColors[2])); displayedCubes[3] = Object.Instantiate(cubePrefab[index]) as GameObject;

		displayedCubes[0].transform.position = new Vector3(-10000,-10000,-10000);
        displayedCubes[1].transform.position = new Vector3(-3, 0, 0);
        displayedCubes[2].transform.position = new Vector3(0, 0, 0);
        displayedCubes[3].transform.position = new Vector3(3, 0, 0);		

        timeCounter = 0;
        timeElapsed = 0;
    }
}
