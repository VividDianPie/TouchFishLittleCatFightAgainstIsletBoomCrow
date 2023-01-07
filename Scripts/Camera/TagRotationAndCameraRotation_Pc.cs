using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TagRotationAndCameraRotation_Pc: MonoBehaviour
{
    //相机初始角度
    float temp;
    //相机转速
    float cameraRotationSpeed;
    //ta点跟随角色
    public GameObject fllowObject;
    void Start()
    {
        temp = 0;
        cameraRotationSpeed = 5;
    }
    void Update()
    {
        ////检测当前鼠标坐标之下是否有 Ui 手指之下有Ui时 不进行 旋转屏幕操作
        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    return;
        //}



        //检测手指触摸之下是否有Ui
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            //Check if finger is over a Ui element
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return;
            }
        }











        if (GameManager.Instance.GetThisUiIsDontDestroyUiCount() > 0) { return; } ;

        this.gameObject.transform.position = new Vector3(fllowObject.transform.position.x,
            fllowObject.transform.position.y + 1.0f, 
            fllowObject.transform.position.z);
        this.gameObject.transform.rotation = Quaternion.AngleAxis(temp += (Input.GetAxis("Mouse X") * cameraRotationSpeed), Vector3.up);
    }
}
