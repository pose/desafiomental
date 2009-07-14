using UnityEngine;
using System.Collections;

public class Problem4Task1Logic : MonoBehaviour {
	
	public GameObject [] prefabs;	
	public GUIStyle style;
	
	GameObject [] gameObjs;
	GameObject [] displayedCubes;
	int currentLevel;
	bool replaceScreenObjs;
	float timeCounter;
	float timeTarget;
	bool lightIsOn;
	bool gameOver;
	
	// Use this for initialization
	void Start () {
		gameObjs = new GameObject[3];
		for(int i=0;i<3;i++) {
			gameObjs[i] = Object.Instantiate(prefabs[i]) as GameObject;			
		}
		InitializeLevel();
		((AudioSource)this.GetComponent("AudioSource")).Play();
	}
	
	void OnGUI() {		
	}
	
	// Update is called once per frame
	void Update () {		
		if(!lightIsOn) {
			timeCounter += Time.deltaTime;
			if(timeCounter >= timeTarget) {
				lightIsOn = true;
				gameObjs[0].active = false;
				gameObjs[1].active = true;
				timeCounter = 0;
				gameObjs[2].transform.position = new Vector3(Random.Range(-4,4),Random.Range(-4,4),0);
			}
		}else if(!gameOver) {
			timeCounter += Time.deltaTime;
			
			RaycastHit [] hits;
			if(Input.GetMouseButton(0)) {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				hits = Physics.RaycastAll(ray);
				int index = 0;
				float dist = 0;
				if (hits.Length>0) {
					dist = hits[0].distance;
					for (int i=1; i<hits.Length; i++) {
						if (dist>hits[i].distance) {
							dist = hits[i].distance;
							index = i;
						}
					}
				}
				if(gameObjs[2]==hits[index].transform.gameObject) {
					print("Response time: " + (timeCounter*1000) + " msecs.");
					gameOver = true;
					timeCounter = 0;
				}
			}
		}else {
			timeCounter += Time.deltaTime;
			if(timeCounter > 2) {
				InitializeLevel();				
			}
		}
	}
	
	void InitializeLevel() {
		// Lamp on.
		gameObjs[0].transform.position = new Vector3(0,3,0);
		gameObjs[0].active = true;
		// Lamp off.
		gameObjs[1].transform.position = new Vector3(0.5f,0.5f,0);
		gameObjs[1].active = false;
		// Button.
		gameObjs[2].transform.position = new Vector3(0,-3,0);		
		currentLevel = 0;
		
		timeCounter = 0;
		timeTarget = Random.Range(2000,6000);
		timeTarget = timeTarget / 1000;
		lightIsOn = false;
		gameOver = false;
	}
}
