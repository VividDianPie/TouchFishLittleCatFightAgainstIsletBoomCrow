using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private void Awake()
    {
        EventManager.Instance.AddListener(EEventType.GameStop, GameTimeStop);
        EventManager.Instance.AddListener(EEventType.GameRun, GameTimeRun);
    }

    void OnDestroy()
    {
        EventManager.Instance.DeleteListener(EEventType.GameStop, GameTimeStop);
        EventManager.Instance.AddListener(EEventType.GameRun, GameTimeRun);
    }
    void GameTimeStop(MyEvent evt)
    {
        BackBengMei.heroActor.GetComponent<BackBengMei>().At.speed = 0;
        Time.timeScale = 0;
        return;
    }
    void GameTimeRun(MyEvent evt)
    {
        BackBengMei.heroActor.GetComponent<BackBengMei>().At.speed = 1;
        Time.timeScale = 1;
        return;
    }

    void Start()
    {
       
    }

 

    void Update()
    {
        //¼àÌý°´¼üiµ¯³ö±³°ü
        if (Input.GetKeyDown(KeyCode.B) && GameManager.Instance.GetThisUiIsDontDestroyUiCount() <= 0)
        {
            GameManager.Instance.LoadUI("Prefab/Uis/BagPopup");
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.GetThisUiIsDontDestroyUiCount() <= 0)
        {
            GameManager.Instance.LoadUI("Prefab/Uis/MainChangeSceneUiCanvas");
        }
    }

     

}
