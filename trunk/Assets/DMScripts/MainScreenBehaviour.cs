using UnityEngine;
using System.Collections;

public class MainScreenBehaviour : MonoBehaviour {
	
	Color backColor;
	bool goingDown = true;
	float fadeSpeed = 0.005f;
	
	public AudioSource mainScreenAudio;

	// Use this for initialization
	void Start () {
		backColor = new Color(.5f,.5f,.5f,.5f);
		mainScreenAudio.Play();
	}
		
	public void OnMouseDown(){
		GameObject pm;
		PointsManagerBehaviour a;
		Application.LoadLevel("DescriptionScreen");
		pm = GameObject.Find("GameManager");
		mainScreenAudio.Stop();
		print("LALA");
		
		a = (PointsManagerBehaviour)pm.GetComponent("PointsManagerBehaviour");
		
		a.setPoints(290);
	}
	
	// Update is called once per frame
	void Update () {
		if(goingDown)
		{
			backColor.r -= fadeSpeed;
			backColor.g -= fadeSpeed;
			backColor.b -= fadeSpeed;
			if(backColor.r < 0) {
				backColor.r = backColor.g = backColor.b = 0.0f;
				goingDown = false;
			} // End if.
		}
		else
		{
			backColor.r += fadeSpeed;
			backColor.g += fadeSpeed;
			backColor.b += fadeSpeed;
			if(backColor.r >= 0.5f) {
				backColor.r = backColor.g = backColor.b = 0.5f;
				goingDown = true;
			} // End if.
		} // End else.
		
		GameObject screenClick = GameObject.Find("001_Screen click_v2");
		if(screenClick != null)
		{
			GUITexture screenBack = (GUITexture)screenClick.GetComponent("GUITexture");
			if(screenBack != null)
			{				
				screenBack.color = backColor;
			} // End if.
		} // End if.
	
	}
}
