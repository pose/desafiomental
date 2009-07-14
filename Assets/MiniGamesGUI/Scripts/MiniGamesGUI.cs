using UnityEngine;

public class MiniGamesGUI : MonoBehaviour
{
    public GUIStyle frameStyle;
    public GUIStyle mainMenuButtonStyle;
    public GUIStyle totalScoreLabelStyle;
    public GUIStyle partialScoreLabelStyle;
    public GUIStyle stairStyle;
    public string mainMenu = "MainMenu";
    public float levelScore = 0.0f;
    public float totalScore = 0.0f;
    public Texture2D[] stairway;
    [HideInInspector]
    public int year;
    [HideInInspector]
    public int level;

    private float noticeTime = 0f;
    private string notice = "";
    public GUIStyle noticeStyle;

    
    void Update()
    {
        noticeTime -= Time.deltaTime;

        if (noticeTime < 0)
            notice = "";
    }

    public void PartialWin()
    {
        notice = "Correct!";
        noticeTime = 1.5f;
    }

    public void PartialLose()
    {
        notice = "Wrong :(";
        noticeTime = 1.5f;
    }

    public void Win()
    {
        notice = "You Win!";
        noticeTime = 3f;
    }

    public void Lose()
    {
        notice = "Game over, try again!";
        noticeTime = 3f;
    }

    public void Notice(string msg, float duration)
    {
        this.notice = msg;
        this.noticeTime = duration;
    }

    private int WhichYear(int problem, int task)
    {
        int[] problemLevels = { 0, 1, 1, 2, 0, 2, 3, 4 };

        return problemLevels[problem - 1];
    }

    void OnGUI()
    {


        //GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height), frameStyle);
        GUILayout.BeginArea(new Rect(0, 0, 750, 500), frameStyle);

        /*TOP*/
        GUILayout.BeginHorizontal();



        if (Application.loadedLevelName.StartsWith("Problem"))
        {
            string[] cs = Application.loadedLevelName.Split('m');
            year = int.Parse(cs[1].Split('T')[0]);
            level = int.Parse((cs[1].Split('k'))[1]);

            //new Rect((230 + Screen.width - 30 - 100)/2-100, 30, 100, 50),
            //GUILayout.Label("Año: " + year);
            //new Rect(Screen.width - 30 - 100, 30, 100, 50),
            //GUILayout.Label("Nivel: " + level);
        }
        if (GUILayout.Button("Reiniciar Nivel"))
        {
            Application.LoadLevel(Application.loadedLevelName);
        }

        GUILayout.EndHorizontal();
        
        GUILayout.BeginHorizontal(GUILayout.ExpandHeight(true));

        
       //GUILayout.BeginHorizontal();
        
        GUILayout.Label(new GUIContent(notice), noticeStyle);

        GUILayout.FlexibleSpace();
        //GUILayout.EndHorizontal();
        
        GUILayout.EndHorizontal();
        /*BOTTOM*/
        GUILayout.BeginHorizontal();
        //new Rect(30, Screen.height - 50 - 30, 100, 50), 
        if (GUILayout.Button("",mainMenuButtonStyle))
        {
            Application.LoadLevel(mainMenu);
        }

        GUILayout.Label( totalScore.ToString(), totalScoreLabelStyle);


        GUILayout.Label(levelScore.ToString(), partialScoreLabelStyle);

        GUILayout.Box(stairway[WhichYear(year, level) % stairway.Length], stairStyle);
        
        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }
}