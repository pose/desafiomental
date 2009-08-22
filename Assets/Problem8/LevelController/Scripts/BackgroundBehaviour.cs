using UnityEngine;

public class BackgroundBehaviour : MonoBehaviour
{
    float duration = 15.0f;
    Color color1 = new Color(0.6f, 0.6f, 0.6f, 0);
    Color color2 = new Color(0.1f, 0.1f, 0.1f, 0);
    Color color3 = new Color(0.3f, 0.3f, 0.3f, 0);
    Color color4 = new Color(0.75f, 0.75f, 0.75f, 0);
    Color color5 = new Color(0.2f, 0.2f, 0.2f, 0);
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        float t;
        if (Time.time < duration)
        {
            t = Mathf.PingPong(Time.time, duration) / duration;
            Camera.main.backgroundColor = Color.Lerp(color1, color2, t);
        }
        else if (Time.time < 2 * duration)
        {
            t = Mathf.PingPong(Time.time - duration, duration) / duration;
            Camera.main.backgroundColor = Color.Lerp(color2, color3, t);
        }
        else if (Time.time < 3 * duration)
        {
            t = Mathf.PingPong(Time.time - 2 * duration, duration) / duration;
            Camera.main.backgroundColor = Color.Lerp(color3, color4, t);
        }
        else if (Time.time < 4 * duration)
        {
            t = Mathf.PingPong(Time.time - 3 * duration, duration) / duration;
            Camera.main.backgroundColor = Color.Lerp(color4, color5, t);
        }
    }
}