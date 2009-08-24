using UnityEngine;
using System.Collections;

public class Problem6Task1Logic : MonoBehaviour
{
	const float TOTAL_TIME = 45.0f;	

    public GUIStyle style;
	public AudioSource backgroundMusic;
	
	public GameObject scalePrefab;
	GameObject[] scales;
	public GameObject[] cubesPrefabs;
	GameObject[] cubes;
	
	GameObject[] frontCubes;
	
	int[] cubesColors;
	int[] cubesWeights;
	
	int numCubes;
	int numFrontCubes;
	int numScales;
	
	int currentLevel = 0;
	
    float timeCounter;
    float timeElapsed;
    float totalTimeCounter;
	float timeRemaining = TOTAL_TIME;

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

		print("START!");
		prepareThreeBlocks();
				
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
			} // End if.
		} // End if.
		else
		{
			print("MiniGamesGUI not found!");
		} // End else.
    }

    void OnGUI()
    {
    }

	Color color1 = new Color(0,0.6f,0,0);
	Color color2 = new Color(0.6f,0,0,0);	
	Color color3 = new Color(0,0,0.6f,0);
	Color color4 = new Color(0.75f,0.75f,0.75f,0);
	Color color5 = new Color(0.2f,0.2f,0.2f,0);
	int pingPongStep = 0;
	float duration = 7.5f;
	
    // Update is called once per frame
    void Update()
    {
		float t;
		if(totalTimeCounter < duration)
		{
			t = Mathf.PingPong (totalTimeCounter, duration) / duration;
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
                    //pManager.setCurrentGame(mapper.getGameNumber("BalanzaAvanzada"));
					pManager.setCurrentGame(mapper.getGameNumber("CadenasEsferas"));
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
				
				bool advanceLevel = false;
				GameObject hitCube = hits[index].transform.gameObject;
				if(hitCube==frontCubes[0])
				{
					print("HIT!");
					mg.PartialWin();
					mg.levelScore += 5.0f;
					mg.totalScore += 5.0f;					
					advanceLevel = true;
					
					if(pmb != null)
					{
						pmb.incrementPoints("Balanza",5);
					} // End if.
				}
				else
				{
					for(int i = 0; i < numFrontCubes; i++)
					{
						if(hitCube==frontCubes[i])
						{
							mg.PartialLose();
							advanceLevel = true;
							break;
						}
					}					
				}
				
				if(advanceLevel)
				{
					cleanupLevelGameObjects();
					
					int val = Random.Range(0,15);
					
					if(val < 5) prepareThreeBlocks();
					else if(val < 10) prepareFourBlocks();
					else if(val < 15) prepareFiveBlocks();					
				}
			}
        }
    }
	
	void prepareThreeBlocks()
	{
		numCubes = 6;
		numFrontCubes = 3;
		numScales = 3;
		cubesColors = new int[3];
		cubesWeights = new int[3];
		
		cubesWeights[0] = 7; cubesWeights[1] = 5; cubesWeights[2] = 3;
		
		ArrayList usedColors = new ArrayList();
		usedColors.Clear();
		for(int i = 0; i < 3; i++)
		{
			do
			{
				cubesColors[i] = Random.Range(0,4);
			}while(usedColors.Contains(cubesColors[i]));
			usedColors.Add(cubesColors[i]);
		}
		
		print(cubesColors[0] + " - " + cubesColors[1] + " - " + cubesColors[2]);
		
		frontCubes = new GameObject[3];
		for(int i = 0; i < 3; i++)
		{
			frontCubes[i] = Object.Instantiate(cubesPrefabs[cubesColors[i]]) as GameObject;
			frontCubes[i].rigidbody.mass = cubesWeights[i];
		}
		
		assignFrontCubesPositions3(frontCubes);		
		
		scales = new GameObject[3];
		for(int i = 0; i < 3; i++)
		{
			scales[i] = Object.Instantiate(scalePrefab) as GameObject;			
		}
	
		scales[0].transform.position = new Vector3(1.5f,0.25f,0.0f);
		scales[1].transform.position = new Vector3(-1.5f,0.25f,0.0f);
		scales[2].transform.position = new Vector3(0,0.25f,1.5f);
		
		cubes = new GameObject[6];
		int firstIndex = -1, secondIndex = -1;	
		
		for(int i = 0; i < 3; i++)
		{
			getIndicesThreeBlocks(i,out firstIndex,out secondIndex);
			cubes[i*2+0] = Object.Instantiate(cubesPrefabs[cubesColors[firstIndex]]) as GameObject;
			cubes[i*2+0].rigidbody.mass = cubesWeights[firstIndex];
			cubes[i*2+1] = Object.Instantiate(cubesPrefabs[cubesColors[secondIndex]]) as GameObject;
			cubes[i*2+1].rigidbody.mass = cubesWeights[secondIndex];
		}

		Vector3 scalePos;
		
		for(int i = 0; i < 3; i++)
		{
			scalePos = scales[i].transform.position;
			cubes[i*2+0].transform.position = new Vector3(-0.7f+scalePos.x,1.25f+scalePos.y,0.0f+scalePos.z);
			cubes[i*2+1].transform.position = new Vector3(0.7f+scalePos.x,1.25f+scalePos.y,0.0f+scalePos.z);
		}
	}
	
	void assignFrontCubesPositions3(GameObject[] frontCubes)
	{
		int [] frontCubesOrder = new int[3];
		ArrayList usedOrders = new ArrayList();
		usedOrders.Clear();
		for(int i = 0; i < 3; i++)
		{
			do
			{
				frontCubesOrder[i] = Random.Range(0,3);
			} while(usedOrders.Contains(frontCubesOrder[i]));
			usedOrders.Add(frontCubesOrder[i]);
		}
		
		Vector3[] vecPos = new Vector3[3];
		vecPos[0] = new Vector3(-1.5f,1.0f,-1.0f);
		vecPos[1] = new Vector3(0.0f,1.0f,-1.0f);
		vecPos[2] = new Vector3(1.5f,1.0f,-1.0f);
		
		for(int i = 0; i < 3; i++)
		{
			frontCubes[i].transform.position = vecPos[frontCubesOrder[i]];
		}
	}
	
	void getIndicesThreeBlocks(int scaleNumber,out int firstIndex,out int secondIndex)
	{
		bool bTest = Random.Range(1,10) <= 5;
		switch(scaleNumber)
		{
		case 0:
			firstIndex = bTest ? 0 : 1;
			secondIndex = bTest ? 1 : 0;
			break;
		case 1:
			firstIndex = bTest ? 0 : 2;
			secondIndex = bTest ? 2 : 0;
			break;
		case 2:
			firstIndex = bTest ? 1 : 2;
			secondIndex = bTest ? 2 : 1;
			break;
		default:
			firstIndex = secondIndex = -1;
			break;
		}
	}
	
	void prepareFourBlocks()
	{
		numCubes = 12;
		numFrontCubes = 4;
		numScales = 6;
		cubesColors = new int[4];
		cubesWeights = new int[4];
		
		cubesWeights[0] = 9; cubesWeights[1] = 7; cubesWeights[2] = 5; cubesWeights[3] = 3;
		
		ArrayList usedColors = new ArrayList();
		usedColors.Clear();
		for(int i = 0; i < 4; i++)
		{
			do
			{
				cubesColors[i] = Random.Range(0,4);
			}while(usedColors.Contains(cubesColors[i]));
			usedColors.Add(cubesColors[i]);
		}
		
		print(cubesColors[0] + " - " + cubesColors[1] + " - " + cubesColors[2] + " - " + cubesColors[3]);
		
		frontCubes = new GameObject[4];
		for(int i = 0; i < 4; i++)
		{
			frontCubes[i] = Object.Instantiate(cubesPrefabs[cubesColors[i]]) as GameObject;
			frontCubes[i].rigidbody.mass = cubesWeights[i];
		}
		
		assignFrontCubesPositions4(frontCubes);		
		
		scales = new GameObject[6];
		for(int i = 0; i < 6; i++)
		{
			scales[i] = Object.Instantiate(scalePrefab) as GameObject;
		}
	
		scales[0].transform.position = new Vector3(2.25f,0.25f,-1.0f);
		scales[1].transform.position = new Vector3(0.0f,0.25f,-1.0f);
		scales[2].transform.position = new Vector3(-2.25f,0.25f,-1.0f);
		
		scales[3].transform.position = new Vector3(2.25f,0.25f,0.0f);
		scales[4].transform.position = new Vector3(0.0f,0.25f,0.0f);
		scales[5].transform.position = new Vector3(-2.25f,0.25f,0.0f);
		
		cubes = new GameObject[12];
		int firstIndex = -1, secondIndex = -1;	
		
		for(int i = 0; i < 6; i++)
		{
			getIndicesFourBlocks(i,out firstIndex,out secondIndex);
			cubes[i*2+0] = Object.Instantiate(cubesPrefabs[cubesColors[firstIndex]]) as GameObject;
			cubes[i*2+0].rigidbody.mass = cubesWeights[firstIndex];
			cubes[i*2+1] = Object.Instantiate(cubesPrefabs[cubesColors[secondIndex]]) as GameObject;
			cubes[i*2+1].rigidbody.mass = cubesWeights[secondIndex];
		}

		Vector3 scalePos;
		
		for(int i = 0; i < 6; i++)
		{
			scalePos = scales[i].transform.position;
			cubes[i*2+0].transform.position = new Vector3(-0.7f+scalePos.x,1.25f+scalePos.y,0.0f+scalePos.z);
			cubes[i*2+1].transform.position = new Vector3(0.7f+scalePos.x,1.25f+scalePos.y,0.0f+scalePos.z);
		}
	}
	
	void assignFrontCubesPositions4(GameObject[] frontCubes)
	{
		int [] frontCubesOrder = new int[4];
		ArrayList usedOrders = new ArrayList();
		usedOrders.Clear();
		for(int i = 0; i < 4; i++)
		{
			do
			{
				frontCubesOrder[i] = Random.Range(0,4);
			} while(usedOrders.Contains(frontCubesOrder[i]));
			usedOrders.Add(frontCubesOrder[i]);
		}
		
		Vector3[] vecPos = new Vector3[4];
		vecPos[0] = new Vector3(-2.25f,1.0f,-1.75f);
		vecPos[1] = new Vector3(-0.75f,1.0f,-1.75f);
		vecPos[2] = new Vector3(0.75f,1.0f,-1.75f);
		vecPos[3] = new Vector3(2.25f,1.0f,-1.75f);
		
		for(int i = 0; i < 4; i++)
		{
			frontCubes[i].transform.position = vecPos[frontCubesOrder[i]];
		}
	}
	
	void getIndicesFourBlocks(int scaleNumber,out int firstIndex,out int secondIndex)
	{
		bool bTest = Random.Range(1,10) <= 5;
		switch(scaleNumber)
		{
		case 0:
			firstIndex = bTest ? 0 : 1;
			secondIndex = bTest ? 1 : 0;
			break;
		case 1:
			firstIndex = bTest ? 0 : 2;
			secondIndex = bTest ? 2 : 0;
			break;
		case 2:
			firstIndex = bTest ? 0 : 3;
			secondIndex = bTest ? 3 : 0;
			break;
		case 3:
			firstIndex = bTest ? 1 : 2;
			secondIndex = bTest ? 2 : 1;
			break;
		case 4:
			firstIndex = bTest ? 1 : 3;
			secondIndex = bTest ? 3 : 1;
			break;
		case 5:
			firstIndex = bTest ? 2 : 3;
			secondIndex = bTest ? 3 : 2;
			break;
		default:
			firstIndex = secondIndex = -1;
			break;
		}
	}
	
	void prepareFiveBlocks()
	{
		numCubes = 20;
		numFrontCubes = 5;
		numScales = 10;
		cubesColors = new int[5];
		cubesWeights = new int[5];
		
		cubesWeights[0] = 11; cubesWeights[1] = 9; cubesWeights[2] = 7; cubesWeights[3] = 5; cubesWeights[4] = 3;
		
		ArrayList usedColors = new ArrayList();
		usedColors.Clear();		
		for(int i = 0; i < 5; i++)
		{
			do
			{
				cubesColors[i] = Random.Range(0,5);
			}while(usedColors.Contains(cubesColors[i]));
			usedColors.Add(cubesColors[i]);
		}
				
		print(cubesColors[0] + " - " + cubesColors[1] + " - " + cubesColors[2] + " - " + cubesColors[3] + " - " + cubesColors[4]);
		
		frontCubes = new GameObject[5];
		for(int i = 0; i < 5; i++)
		{
			frontCubes[i] = Object.Instantiate(cubesPrefabs[cubesColors[i]]) as GameObject;
			frontCubes[i].rigidbody.mass = cubesWeights[i];
		}
		
		assignFrontCubesPositions5(frontCubes);		
		
		scales = new GameObject[10];
		for(int i = 0; i < 10; i++)
		{
			scales[i] = Object.Instantiate(scalePrefab) as GameObject;
		}
	
		scales[0].transform.position = new Vector3(2.25f,0.25f,-1.0f);
		scales[1].transform.position = new Vector3(0.0f,0.25f,-1.0f);
		scales[2].transform.position = new Vector3(-2.25f,0.25f,-1.0f);
		
		scales[3].transform.position = new Vector3(2.25f,0.25f,0.0f);
		scales[4].transform.position = new Vector3(0.0f,0.25f,0.0f);
		scales[5].transform.position = new Vector3(-2.25f,0.25f,0.0f);
		
		scales[6].transform.position = new Vector3(2.25f,0.25f,1.0f);
		scales[7].transform.position = new Vector3(0.0f,0.25f,1.0f);
		scales[8].transform.position = new Vector3(-2.25f,0.25f,1.0f);
		
		scales[9].transform.position = new Vector3(0.0f,0.25f,2.0f);
		
		cubes = new GameObject[20];
		int firstIndex = -1, secondIndex = -1;	
		
		for(int i = 0; i < 10; i++)
		{
			getIndicesFiveBlocks(i,out firstIndex,out secondIndex);
			cubes[i*2+0] = Object.Instantiate(cubesPrefabs[cubesColors[firstIndex]]) as GameObject;
			cubes[i*2+0].rigidbody.mass = cubesWeights[firstIndex];
			cubes[i*2+1] = Object.Instantiate(cubesPrefabs[cubesColors[secondIndex]]) as GameObject;
			cubes[i*2+1].rigidbody.mass = cubesWeights[secondIndex];
		}

		Vector3 scalePos;
		
		for(int i = 0; i < 10; i++)
		{
			scalePos = scales[i].transform.position;
			cubes[i*2+0].transform.position = new Vector3(-0.7f+scalePos.x,1.25f+scalePos.y,0.0f+scalePos.z);
			cubes[i*2+1].transform.position = new Vector3(0.7f+scalePos.x,1.25f+scalePos.y,0.0f+scalePos.z);
		}
		print("POR ACA PASE");
	}
	
	void assignFrontCubesPositions5(GameObject[] frontCubes)
	{
		int [] frontCubesOrder = new int[5];
		ArrayList usedOrders = new ArrayList();
		usedOrders.Clear();
		for(int i = 0; i < 5; i++)
		{
			do
			{
				frontCubesOrder[i] = Random.Range(0,5);
			} while(usedOrders.Contains(frontCubesOrder[i]));
			usedOrders.Add(frontCubesOrder[i]);
		}
		
		Vector3[] vecPos = new Vector3[5];
		vecPos[0] = new Vector3(-3.0f,1.0f,-1.75f);
		vecPos[1] = new Vector3(-1.5f,1.0f,-1.75f);
		vecPos[2] = new Vector3(0.0f,1.0f,-1.75f);
		vecPos[3] = new Vector3(1.5f,1.0f,-1.75f);
		vecPos[4] = new Vector3(3.0f,1.0f,-1.75f);
		
		for(int i = 0; i < 5; i++)
		{
			frontCubes[i].transform.position = vecPos[frontCubesOrder[i]];
		}
	}
		
	void getIndicesFiveBlocks(int scaleNumber,out int firstIndex,out int secondIndex)
	{
		bool bTest = Random.Range(1,10) <= 5;
		switch(scaleNumber)
		{
		case 0:
			firstIndex = bTest ? 0 : 1;
			secondIndex = bTest ? 1 : 0;
			break;
		case 1:
			firstIndex = bTest ? 0 : 2;
			secondIndex = bTest ? 2 : 0;
			break;
		case 2:
			firstIndex = bTest ? 0 : 3;
			secondIndex = bTest ? 3 : 0;
			break;
		case 3:
			firstIndex = bTest ? 0 : 4;
			secondIndex = bTest ? 4 : 0;
			break;
		case 4:
			firstIndex = bTest ? 1 : 2;
			secondIndex = bTest ? 2 : 1;
			break;
		case 5:
			firstIndex = bTest ? 1 : 3;
			secondIndex = bTest ? 3 : 1;
			break;
		case 6:
			firstIndex = bTest ? 1 : 4;
			secondIndex = bTest ? 4 : 1;
			break;
		case 7:
			firstIndex = bTest ? 2 : 3;
			secondIndex = bTest ? 3 : 2;
			break;
		case 8:
			firstIndex = bTest ? 2 : 4;
			secondIndex = bTest ? 4 : 2;
			break;
		case 9:
			firstIndex = bTest ? 3 : 4;
			secondIndex = bTest ? 4 : 3;
			break;
		default:
			firstIndex = secondIndex = -1;
			break;
		}
	}
	
	void cleanupLevelGameObjects()
	{
		print("CLEANUP " + currentLevel);
		
		for(int i = 0; i < numCubes; i++)
		{
			Object.Destroy(cubes[i]);
		}
		
		for(int i = 0; i < numFrontCubes; i++)
		{
			Object.Destroy(frontCubes[i]);
		}
		
		for(int i = 0; i < numScales; i++)
		{
			Object.Destroy(scales[i]);
		}
	}
}
