using UnityEngine;
using System.Collections;

public class TryingFadeOut : MonoBehaviour
{

    public string sceneToLoad;
    public string textureName;
    private bool goingDown = false;
    Faders fader = null;
    // Use this for initialization
    
    void Start()
    {
        fader = new Faders();
        fader.setBackColor(new Color(.5f, .5f, .5f, .5f), textureName);
    }

    public void OnMouseDown()
    {
        goingDown = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (goingDown)
        {
            if (fader.fadeOut(textureName)== 1)
            {
                Application.LoadLevel(sceneToLoad);
            }
        }
    }
}
