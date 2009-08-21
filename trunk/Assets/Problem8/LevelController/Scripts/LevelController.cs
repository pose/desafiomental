using UnityEngine;

public class LevelController : MonoBehaviour
{
    public AudioSource song;
    private GameObject[] targets;
    private bool[] complete;
    public string onWinLoad = null;
    private RaycastHit[] hits;
    private Link selectedLink = null;
    private PointsManagerBehaviour pmb;
    private MiniGamesGUI mg;

    void Start()
    {
        GameObject go = GameObject.Find("GameManager");

//        if ( song != null && !song.isPlaying)
//            song.Play();
        targets = new GameObject[transform.childCount];

        int i = 0;
        foreach (Transform g in transform)
        {
            targets[i] = g.gameObject;
            targets[i].name = "Base" + i;
            i++;
        }

        complete = new bool[targets.Length];

        long totalPoints = 0;
        if (go != null)
        {
            pmb = ((PointsManagerBehaviour)go.GetComponent("PointsManagerBehaviour"));
            totalPoints = pmb.getPoints();
        } // End if.

        GameObject mggo = GameObject.Find("MiniGamesGUI");
        if (mggo != null)
        {
            mg = ((MiniGamesGUI)mggo.GetComponent("MiniGamesGUI"));
            mg.totalScore = totalPoints;
        } // End if.
    }

    void Complete(Object target)
    {
        GameObject g = (GameObject) target;

        for (int i = 0; i < targets.Length; i++ )
        {
            if (targets[i] == g)
            {
                complete[i] = true;
                break;
            }
        }

        for (int i = 0; i < complete.Length; i++)
        {
            if (complete[i] == false)
                return;
        }
        if ( song != null )
            song.Stop();

        long totalPoints = 0;
        GameObject go = GameObject.Find("GameManager");
        if (go != null)
        {
            pmb = ((PointsManagerBehaviour)go.GetComponent("PointsManagerBehaviour"));
            totalPoints = pmb.getPoints();
        }

        GameObject mggo = GameObject.Find("MiniGamesGUI");
        MiniGamesGUI mg = ((MiniGamesGUI)mggo.GetComponent("MiniGamesGUI"));
        mg.totalScore = totalPoints;

        if (mg != null)
        {
            mg.PartialWin();
            mg.totalScore += (float)100;
        } // End if.

        if (pmb != null)
        {
            print("PGM INCREMENTED");
            pmb.incrementPoints(100);
        } // End if.


        if( onWinLoad != null && !onWinLoad.Equals("") )
            Application.LoadLevel(onWinLoad);
    }

    void Update()
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

            Link link = (Link)hits[index].transform.gameObject.GetComponent(typeof(Link));

            
            // Mouse down
            if (link != null && Input.GetMouseButtonUp(0))
            {
                link.SendMessageUpwards("Cut");
                selectedLink = null;
                return;
            }

            //Mouse Enter
            if (link != null && selectedLink == null)
            {
                link.SendMessageUpwards("SavePreviousColor");
                selectedLink = link;
                return;
            }

            //Mouse Over
            if (link != null)
            {
                link.SendMessageUpwards("Hover");
                return;
            }
        }

        //Mouse Exit
        if (selectedLink != null)
        {
            selectedLink.SendMessageUpwards("UnHover");
            selectedLink = null;
            return;
        }
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(600, 400,750,500));

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Reiniciar Nivel", GUILayout.Width(100)) )
        {
            Application.LoadLevel(Application.loadedLevelName);
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
}