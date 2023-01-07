using UnityEngine;

public class FPS : MonoBehaviour
{
    public static float f_Fps;
    public float f_UpdateInterval = 0.5f; //ÿ��0.5��ˢ��һ��  
    private float f_LastInterval; //��Ϸʱ��  
    private int i_Frames = 0;//֡��  
    void Awake()
    {
        Application.targetFrameRate = 60;
    }
    void OnGUI()
    {
        if (f_Fps > 50)
        {
            GUI.color = new Color(0, 1, 0);
        }
        else if (f_Fps > 40)
        {
            GUI.color = new Color(1, 1, 0);
        }
        else
        {
            GUI.color = new Color(1.0f, 0, 0);
        }


        GUI.Box(new Rect(10, 10, 100, 30), "FPS:" + f_Fps.ToString("f2"));
    }
    void Update()
    {
        ++i_Frames;

        if (Time.realtimeSinceStartup > f_LastInterval + f_UpdateInterval)
        {
            f_Fps = i_Frames / (Time.realtimeSinceStartup - f_LastInterval);

            i_Frames = 0;

            f_LastInterval = Time.realtimeSinceStartup;
        }
    }
}