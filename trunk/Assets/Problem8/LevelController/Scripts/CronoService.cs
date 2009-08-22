using UnityEngine;

public class CronoService : MonoBehaviour
{
    public float minutesToPlay = 0.5f;
    float timeRemaining;
    float totalTime = 0.001f;

    void Start()
    {
        timeRemaining = minutesToPlay * 60;
    }
    void Update()
    {
        
        MiniGamesGUI mg = Component.FindObjectOfType(System.Type.GetType("MiniGamesGUI")) as MiniGamesGUI;
        PointsManagerBehaviour pmb = null;
        
        GameObject go = GameObject.Find("GameManager");
        if (go != null)
            pmb = ((PointsManagerBehaviour)go.GetComponent("PointsManagerBehaviour"));
        
        GamesMapper mapper = new GamesMapper();
        
        timeRemaining -= Time.deltaTime;
        mg.updateCronometer(timeRemaining);

        totalTime += Time.deltaTime;

        if (totalTime >= minutesToPlay * 60)
        {
            if (pmb != null)
            {
                pmb.incrementLevelsCompleted(1);
            } // End if.
            // Change level

            GameObject l = GameObject.Find("LevelController");
            LevelController lc = l.GetComponent("LevelController") as LevelController;
            lc.SendMessage("TimeIsUp");
            Application.LoadLevel("ResultsScreen");
        }
    }
}