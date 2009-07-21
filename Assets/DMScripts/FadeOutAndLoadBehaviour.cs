using UnityEngine;
using System.Collections;

public class FadeOutAndLoadBehaviour : MonoBehaviour {
	
	public string sceneToLoad;
	public string[] textureNames;
    private Faders fader = null;
	private bool goingDown = false;
	private int i, qty=0;

	// Use this for initialization
	void Start () {
            qty = 0;
            fader = new Faders();
            for ( i = 0; i < textureNames.Length ; i++ ){
				fader.setBackColor(new Color(.5f,.5f,.5f,.5f), textureNames[i]);
			}
	}
	
	public void OnMouseDown(){
        goingDown = true;
	}
	
	
	// Update is called once per frame
	void Update () {
			if( goingDown ){

				for ( i = 0; i < textureNames.Length ; i++ ){
					qty += fader.fadeOut(textureNames[i]);
				}
				
                if( qty == textureNames.Length ){
					Application.LoadLevel(sceneToLoad);
				}
			}
	}
}
