using UnityEngine;

public class CronoService : MonoBehaviour
{
    private static CronoService instance;
    public const float minutesToPlay = 0.5f;
    float timeRemaining = minutesToPlay * 60;
    float totalTime = 0.001f;

    private ServiceLocator sl;
    void Start()
    {
        sl = ServiceLocator.GetInstance();
    }
    void Update()
    {

        MiniGamesGUI mg = sl.Get("MiniGamesGUI") as MiniGamesGUI;
        PointsManagerBehaviour pmb = sl.Get("PointsManagerBehavoiur") as PointsManagerBehaviour;
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
            GameObject go = GameObject.Find("GameManager");

            if (go != null)
            {
                PointsManagerBehaviour pManager = ((PointsManagerBehaviour)go.GetComponent("PointsManagerBehaviour"));
                if (pManager != null)
                {
                    pManager.setCurrentGame(mapper.getGameNumber("IdentificacionCromatica"));
                }
            }

            Application.LoadLevel("GameDescription");
        }
    }

    public static CronoService GetInstance()
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load("CronoService")) as CronoService;
        }
        return instance;
    }
}