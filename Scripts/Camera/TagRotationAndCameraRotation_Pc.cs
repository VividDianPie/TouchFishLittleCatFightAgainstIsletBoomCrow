using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TagRotationAndCameraRotation_Pc: MonoBehaviour
{
    //�����ʼ�Ƕ�
    float temp;
    //���ת��
    float cameraRotationSpeed;
    //ta������ɫ
    public GameObject fllowObject;
    void Start()
    {
        temp = 0;
        cameraRotationSpeed = 5;
    }
    void Update()
    {
        ////��⵱ǰ�������֮���Ƿ��� Ui ��ָ֮����Uiʱ ������ ��ת��Ļ����
        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    return;
        //}



        //�����ָ����֮���Ƿ���Ui
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
