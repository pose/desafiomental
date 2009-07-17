using UnityEngine;
using System.Collections;

public class Problem2Task1Logic : MonoBehaviour
{

    public GameObject[] cubePrefab;
    public int numberOfCubes;
    public GUIStyle style;

    GameObject[] cubes;
    GameObject[] displayedCubes;
    int currentLevel;
    bool replaceScreenObjs;
    ScriptCube scriptDisplayedCube;
    float timeCounter;
    float timeElapsed;
    float totalTimeCounter;

    int signColorIndex;
    float signColorR, signColorG, signColorB;
    string signText;
	
	PointsManagerBehaviour pmb = null;
	MiniGamesGUI mg = null;

    // Use this for initialization
    void Start()
    {

        // Instantiate a copy of every kind of cube and put them in a
        // galaxy far, far away.
        cubes = new GameObject[8];
        for (int i = 0; i < numberOfCubes; i++)
        {
            cubes[i] = Object.Instantiate(cubePrefab[i]) as GameObject;
            cubes[i].transform.position = new Vector3(0, 0, -10000);
        }

        displayedCubes = new GameObject[4];
        currentLevel = 0;
        replaceScreenObjs = true;
        totalTimeCounter = 0;
				
		long totalPoints = 0;
		GameObject go = GameObject.Find("GameManager");
		if (go != null)
		{
			pmb = ((PointsManagerBehaviour)go.GetComponent("PointsManagerBehaviour"));
			print("FOUND");
			if(pmb != null)
			{
				print("FOUND2");
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

    // Update is called once per frame
    void Update()
    {
        if (replaceScreenObjs)
        {
            replaceScreenObjs = false;
            currentLevel++;
//			print(currentLevel);
            assignDisplayedCubes();
        }

        totalTimeCounter += Time.deltaTime;
        if (totalTimeCounter > 60)
        {
//            print("Time's up!");
			// CARGA DE LA ESCENA
            Application.LoadLevel("RunScreen");
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
                    for (int i = 0; i < 4; i++)
                    {
                        Object.Destroy(displayedCubes[i]);
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
            }
        }
    }

    protected void assignDisplayedCubes()
    {
        int mainColorIndex = Random.Range(0, numberOfCubes);
        displayedCubes[0] = Object.Instantiate(cubePrefab[mainColorIndex]) as GameObject;
        scriptDisplayedCube = (ScriptCube)displayedCubes[0].GetComponent("ScriptCube");
		
//		print("NEW ROUND");

		ArrayList usedColors = new ArrayList();
		usedColors.Add(mainColorIndex);
        do
        {
            signColorIndex = Random.Range(0, numberOfCubes);
        } while (true==usedColors.Contains(signColorIndex));
		usedColors.Add(signColorIndex);		

        signColorR = ((ScriptCube)cubePrefab[signColorIndex].GetComponent("ScriptCube")).fColorR;
        signColorG = ((ScriptCube)cubePrefab[signColorIndex].GetComponent("ScriptCube")).fColorG;
        signColorB = ((ScriptCube)cubePrefab[signColorIndex].GetComponent("ScriptCube")).fColorB;
        signText = scriptDisplayedCube.colorNameSP;

		ArrayList usedSlots = new ArrayList();		
        int mainColorAssignedSlot = Random.Range(1, 4);
		usedSlots.Add(mainColorAssignedSlot);
        int signColorAssignedSlot;
        do
        {
            signColorAssignedSlot = Random.Range(1, 4);
        } while (true==usedSlots.Contains(signColorAssignedSlot));
		usedSlots.Add(signColorAssignedSlot);

        displayedCubes[mainColorAssignedSlot] = Object.Instantiate(cubePrefab[mainColorIndex]) as GameObject;
        displayedCubes[signColorAssignedSlot] = Object.Instantiate(cubePrefab[signColorIndex]) as GameObject;

        for (int i = 1; i < 4; i++)
        {
            if (false==usedSlots.Contains(i))
            {
				usedSlots.Add(i);
                int randomVal;
                do
                {
                    randomVal = Random.Range(0, numberOfCubes);
                } while (true==usedColors.Contains(randomVal));
                usedColors.Add(randomVal);
                displayedCubes[i] = Object.Instantiate(cubePrefab[randomVal]) as GameObject;
            }
        }
		
//		print("Colors: " + usedColors[0] + " " + usedColors[1] + " " + usedColors[2]);
//		print("Slots: " + usedSlots[0] + " " + usedSlots[1] + " " + usedSlots[2]);

//		displayedCubes[0].transform.position = new Vector3(0,4,0); // No need to display a cube at slot 0.
        displayedCubes[1].transform.position = new Vector3(-3, 0, 0);
        displayedCubes[2].transform.position = new Vector3(0, 0, 0);
        displayedCubes[3].transform.position = new Vector3(3, 0, 0);

        timeCounter = 0;
        timeElapsed = 0;
    }
}
