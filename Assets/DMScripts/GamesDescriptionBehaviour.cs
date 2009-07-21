using UnityEngine;
using System.Collections;

public class GamesDescriptionBehaviour : MonoBehaviour {
	
	private string sceneToLoad;
    public Texture[] textures;
    private Faders fader = null;
	private bool goingDown = false;
    private int idx = 0;
   
	// Use this for initialization
    void Start()
    {
        GameObject go = GameObject.Find("GameManager");
        GamesMapper mapper = new GamesMapper();
        fader = new Faders();
        if (go != null)
        {
            PointsManagerBehaviour pManager = ((PointsManagerBehaviour)go.GetComponent("PointsManagerBehaviour"));
            if (pManager != null)
            {
                idx = pManager.getCurrentGame();
            }
        }
      
        sceneToLoad = mapper.getGameScene(idx);

        GameObject.Find("GameDescriptionTexture").guiTexture.texture = textures[idx - 1];

       // ((FadeInEffectBehaviour)(GameObject.Find("GameDescriptionTexture").GetComponent("FadeInEffectBehaviour"))).texturesNames[0] = textures[idx - 1].name;
        
        fader.setBackColor(new Color(.5f, .5f, .5f, .5f), textures[idx - 1].name);

    }
	
	public void OnMouseDown(){
        goingDown = true;
	}
	
	
	// Update is called once per frame
	void Update () {
			if( goingDown ){

				if ( fader.fadeOut(textures[idx-1].name) == 1 )
                {
					Application.LoadLevel(sceneToLoad);
				}
			}
	}
}
