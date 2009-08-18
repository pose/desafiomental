using UnityEngine;

public class MiniGamesGUI : MonoBehaviour
{
    public GUIStyle frameStyle;
    public GUIStyle mainMenuButtonStyle;
    public GUIStyle totalScoreLabelStyle;
    public GUIStyle partialScoreLabelStyle;
    public GUIStyle stairStyle;
	public GUIStyle chronometerStyle;
    public string mainMenu = "MainMenu";
    public float levelScore = 0.0f;
    public float totalScore = 0.0f;
    public Texture2D[] stairway;
    [HideInInspector]
    public int year;
    [HideInInspector]
    public int level;

	private string partialWinString = "  ¡CORRECTO!";
	private string partialLoseString = "¡INCORRECTO!";
	private float partialWinLoseDisplayTime = 1.0f;
	private int noticeX = 270;
	private int noticeY = 60;
    private float noticeTime = 0f;
	private float noticeCyclesCounter = 0;
    private string notice = "";
	private string backupNotice = "";
    public GUIStyle noticeStyle;
	public AudioSource partialWinAudio;
	public AudioSource partialLoseAudio;
	
	private string chronometerPrefix = "";
	private int minutes;
	private int seconds;
	private int msecs;
	
	private bool displayRestartLevelButton;
	
	void resetToDefaultParams()
	{
		partialWinString = "  ¡CORRECTO!";
		partialLoseString = "¡INCORRECTO!";
		partialWinLoseDisplayTime = 1.0f;
		noticeX = 270;
		noticeY = 60;
		noticeTime = 0f;
		noticeCyclesCounter = 0;
		notice = "";
		backupNotice = "";
		displayRestartLevelButton = false;
	}
    
    void Update()
    {
        noticeTime -= Time.deltaTime;

        if (noticeTime < 0)
		{
            notice = "";
		}
		else
		{
			noticeCyclesCounter++;
			if(noticeCyclesCounter < 3)
			{				
				notice = "";
			}
			else if(noticeCyclesCounter < 6)
			{				
				notice = backupNotice;
			}
			else
			{
				noticeCyclesCounter = 0;
			}
		}
    }
	
	public void setPartialWinString(string str)
	{
		partialWinString = str;
	}
	
	public void setPartialLoseString(string str)
	{
		partialLoseString = str;
	}
	
	public float getPartialWinLoseDisplayTime()
	{
		return partialWinLoseDisplayTime;
	}
	
	public void setPartialWinLoseDisplayTime(float ftime)
	{
		partialWinLoseDisplayTime = ftime;
	}
	
	public int getNoticeX() { return noticeX; }
	public int getNoticeY() { return noticeY; }
	
	public void setNoticeXY(int x,int y)
	{
		noticeX = x; noticeY = y;
		print("NoticeX: " + noticeX + " - NoticeY: " + noticeY);
	}
	
    public void PartialWin()
    {
        backupNotice = notice = partialWinString;
        noticeTime = partialWinLoseDisplayTime;
		noticeStyle.normal.textColor = new Color(0.0f,0.75f,0.0f, 1.0f);
		partialWinAudio.Play();
    }

    public void PartialLose()
    {
        backupNotice = notice = partialLoseString;
        noticeTime = partialWinLoseDisplayTime;
		noticeStyle.normal.textColor = new Color(0.75f,0.0f,0.0f, 1.0f);
		partialLoseAudio.Play();
    }

    public void Win()
    {
        backupNotice = notice = "You Win!";
        noticeTime = 3f;
    }

    public void Lose()
    {
        backupNotice = notice = "Game over, try again!";
        noticeTime = 3f;
    }

    public void Notice(string msg, float duration)
    {
        backupNotice= this.notice = msg;
        this.noticeTime = duration;
    }
	
	public void setChronometerPrefix(string prefix)
	{
		chronometerPrefix = prefix;
	}		
	
	public void updateCronometer(float time)
	{
		if(time < 0) time = 0;
		minutes = ((int)time) / 60;
		seconds = ((int)time) % 60;
		msecs = (int)((time - ((int)time)) * 100);
	}

    private int WhichYear(int problem, int task)
    {
        int[] problemLevels = { 0, 1, 1, 2, 0, 2, 3, 4 };
        Debug.Log(problem);
        return problemLevels[problem - 1];
    }
	
	public void enableRestartLevelButton()
	{
		displayRestartLevelButton = true;
	}
	
	public void disableRestartLevelButton()
	{
		displayRestartLevelButton = false;
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
		if(displayRestartLevelButton)
		{
			if (GUILayout.Button("Reiniciar Nivel"))
			{
				Application.LoadLevel(Application.loadedLevelName);
			}
		}

        GUILayout.EndHorizontal();
        
        GUILayout.BeginHorizontal(GUILayout.ExpandHeight(true));

        
       //GUILayout.BeginHorizontal();
        
		// Print the notices.
        //GUILayout.Label(new GUIContent(notice), noticeStyle); // Old code.		
        GUI.Label(new Rect(noticeX,noticeY,500,60),notice, noticeStyle);
		

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

		GUILayout.Label( totalScore.ToString(),totalScoreLabelStyle);
        GUILayout.Label(levelScore.ToString(),partialScoreLabelStyle);
		
		// Print the chronometer.
		chronometerStyle.normal.textColor = new Color(0.75f,0.0f,0.0f, 1.0f);
        GUI.Label(new Rect(25, 110, 500, 60), string.Format("{0}{1:00}:{2:00}:{3:00}",chronometerPrefix,minutes,seconds,msecs),
			chronometerStyle); // Format the string  using the oh-so-intuitive C# way.

        GUILayout.Box(stairway[WhichYear(year, level) % stairway.Length], stairStyle);
        
        GUILayout.EndHorizontal();

        GUILayout.EndArea();
    }
}