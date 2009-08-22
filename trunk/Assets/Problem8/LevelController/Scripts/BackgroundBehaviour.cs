using UnityEngine;

public class BackgroundBehaviour : MonoBehaviour
{
    public float duration = 15.0f;
    public Color[] colors = {new Color(0.6f, 0.6f, 0.6f, 0),
                                new Color(0.1f, 0.1f, 0.1f, 0),
                                new Color(0.3f, 0.3f, 0.3f, 0),
                                new Color(0.75f, 0.75f, 0.75f, 0),
                                new Color(0.2f, 0.2f, 0.2f, 0)};
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
            Camera.main.backgroundColor = Color.Lerp(colors[0], colors[1], t);
        }
        else if (Time.time < 2 * duration)
        {
            t = Mathf.PingPong(Time.time - duration, duration) / duration;
            Camera.main.backgroundColor = Color.Lerp(colors[1], colors[2], t);
        }
        else if (Time.time < 3 * duration)
        {
            t = Mathf.PingPong(Time.time - 2 * duration, duration) / duration;
            Camera.main.backgroundColor = Color.Lerp(colors[2], colors[3], t);
        }
        else if (Time.time < 4 * duration)
        {
            t = Mathf.PingPong(Time.time - 3 * duration, duration) / duration;
            Camera.main.backgroundColor = Color.Lerp(colors[3], colors[4], t);
        }
    }
}