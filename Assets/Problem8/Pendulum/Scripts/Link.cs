using UnityEngine;

public class Link : MonoBehaviour
{
    public Color hoverColor1 = Color.red;
    public Color hoverColor2 = Color.green;
    float duration = 1.0f;
    private Color originalColor;

    void SavePreviousColor()
    {
        originalColor = renderer.material.color;
    }

    void Hover()
    {
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        renderer.material.color = Color.Lerp(hoverColor1, hoverColor2, lerp);
    }

    void UnHover()
    {
        renderer.material.color = originalColor;
    }
}