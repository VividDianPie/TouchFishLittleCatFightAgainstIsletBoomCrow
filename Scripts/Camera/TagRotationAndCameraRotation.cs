using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TagRotationAndCameraRotation : MonoBehaviour
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
        cameraRotationSpeed = 2;
    }
    void Update()
    {
        ////��⵱ǰ�������֮���Ƿ��� Ui ��ָ֮����Uiʱ ������ ��ת��Ļ����
        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    return;
        //}


        ////�����ָ����֮���Ƿ���Ui
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        //{
        //    //Check if finger is over a Ui element
        //    if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        //    {
        //        return;
        //    }
        //}

        
        if (GameManager.Instance.GetThisUiIsDontDestroyUiCount() > 0) { return; };
        this.gameObject.transform.position = new Vector3(fllowObject.transform.position.x, fllowObject.transform.position.y + 1.0f, fllowObject.transform.position.z);

        //��ָ���ƾ�ͷ������ת�ƶ�
        //��ֻ��ָ����ʱ X ��Ӧ�� 
        if (FingerSlideDir.Instance.TouchDirectionX1() != 0)
        {
            float axisX = 0; Input.GetAxis("Mouse X");
            axisX = FingerSlideDir.Instance.TouchDirectionX1()/3;
            this.gameObject.transform.rotation = Quaternion.AngleAxis(temp += (axisX * cameraRotationSpeed), Vector3.up);
        }
        //��ֻ��ָ����ʱ X ��Ӧ��
        if (FingerSlideDir.Instance.TouchDirectionX2() != 0)
        {
            float axisX = 0; Input.GetAxis("Mouse X");
            axisX = FingerSlideDir.Instance.TouchDirectionX2()/3;
            this.gameObject.transform.rotation = Quaternion.AngleAxis(temp += (axisX * cameraRotationSpeed), Vector3.up);
        }

    }
}
