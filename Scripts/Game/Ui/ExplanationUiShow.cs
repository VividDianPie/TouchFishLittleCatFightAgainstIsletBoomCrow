using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplanationUiShow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void ExplanationUiButton()
    {
        if (GameManager.Instance.GetThisUiIsDontDestroyUiCount() <= 0)
        {
            GameManager.Instance.LoadUI("Prefab/Uis/ExplanationPopUi");
        }
    }
}
