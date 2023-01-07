using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesChangeUiButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void ScenesChanegeUiButton()
    {
        if (GameManager.Instance.GetThisUiIsDontDestroyUiCount() <= 0)
        {
            GameManager.Instance.LoadUI("Prefab/Uis/MainChangeSceneUiCanvas");
        }
    }
}
