using UnityEngine;
using System.Collections;

public class Problem4Task1Logic : MonoBehaviour {
	
	public GameObject [] prefabs;
	
	GameObject [] gameObjs;
	GameObject [] displayedCubes;
	int currentLevel = 0;
	bool replaceScreenObjs;
	float timeCounter;
	float timeTarget;
	bool lightIsOn;
	bool gameOver;
	
	private float buttonX;
	private float buttonY;
	
	PointsManagerBehaviour pmb = null;
	MiniGamesGUI mg = null;
	
	// Use this for initialization
	void Start ()
	{
		gameObjs = new GameObject[3];
		for(int i=0;i<3;i++)
		{
			gameObjs[i] = Object.Instantiate(prefabs[i]) as GameObject;			
		}
		InitializeLevel();
		
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
				mg.setChronometerPrefix("TIEMPO DE RESPUESTA:\n          ");
			} // End if.
		} // End if.
		else
		{
			print("MiniGamesGUI not found!");
		} // End else.
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(currentLevel==4)
		{
			print("FIN!");
//			backgroundMusic.Stop();
			// CARGA DE LA ESCENA			
            Application.LoadLevel("RunScreen");
		}
//		if(Input.GetMouseButton(0))
			//print("Mouse x: " + Input.mousePosition.x + " y: " + Input.mousePosition.y);
		
		if(!lightIsOn)
		{
			timeCounter += Time.deltaTime;
			if(timeCounter >= timeTarget)
			{
				lightIsOn = true;
				gameObjs[0].active = false;
				gameObjs[1].active = true;
				timeCounter = 0;
				buttonX = Random.Range(8,92)/100.0f; buttonY = Random.Range(25,45)/100.0f; 
				gameObjs[2].transform.position = new Vector3(buttonX,buttonY,0);
			}
		}
		else if(!gameOver)
		{
			timeCounter += Time.deltaTime;	
			mg.updateCronometer(timeCounter);
			
			if(false==Input.GetMouseButton(0)) return;

			int coordX = (int)(buttonX * 750);
			int coordY = (int)(buttonY * 500);
			
			print("CoordX: " + coordX + " - CoordY: " + coordY + " - MouseX: " + Input.mousePosition.x + " - MouseY: " + Input.mousePosition.y);			
			
			if(Input.mousePosition.x > (coordX-32) && Input.mousePosition.x < (coordX+32) &&
				Input.mousePosition.y >= (coordY-32) && Input.mousePosition.y <= (coordY+32))
			{
				print("Response time: " + (timeCounter*1000) + " msecs.");
				gameOver = true;
				timeCounter = 0;
				mg.updateCronometer(timeCounter);
			}
		}
		else
		{
			timeCounter += Time.deltaTime;
			if(timeCounter > 2)
			{
				InitializeLevel();				
			}
		}
	}
	
	void InitializeLevel()
	{
		gameObjs[0].transform.position = new Vector3(0.55f,0.9f,0);
		gameObjs[0].active = true;
		gameObjs[1].transform.position = new Vector3(0.55f,0.9f,0);
		gameObjs[1].active = false;
		gameObjs[2].transform.position = new Vector3(0.5f,0.25f,0);		
		currentLevel++;
		
		timeCounter = 0;
		timeTarget = Random.Range(2000,6000);
		timeTarget = timeTarget / 1000;
		lightIsOn = false;
		gameOver = false;
	}
}
