using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChikFingerPosHaveUi : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        //Check if there is  a touch
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //Check if finger is over a Ui element
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                Debug.Log("Touched the Ui");
            }
        }
    }
}
