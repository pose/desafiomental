using UnityEngine;
using System.Collections;

public class FadeInEffectBehaviour : MonoBehaviour {
	public string[] texturesNames;
	private bool doneFading = false;
	private int qtyFades = 0, i;
    private Faders fader = null;
	
	// Use this for initialization
	
	void Start () {
		
		doneFading = false;
        fader = new Faders();

		for( i = 0 ; i < texturesNames.Length ; i++ ){
			fader.setBackColor( new Color(0.0f,0.0f,0.0f,0.5f), texturesNames[i] );
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		for( i = 0 ; i < texturesNames.Length ; i++ ){
			qtyFades += fader.fadeIn(texturesNames[i]);
		}
		
		if( !doneFading && ( qtyFades  == texturesNames.Length ) ){
					doneFading = true;
		}
		
	}
	
}
