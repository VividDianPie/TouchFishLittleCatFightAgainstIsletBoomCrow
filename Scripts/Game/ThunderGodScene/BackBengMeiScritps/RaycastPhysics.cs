using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastPhysics : MonoBehaviour
{
    //float startSpeed;
    //float gravityAddSpeed;
    //bool usGravity;

    ////����ǰ�����Ƿ���ײ
    //bool forwardRaycastIsCollision = false;
    ////����ǰ���߾�ѿ�¾���
    //float meiToforwardRaycastDistance = 1;
    ////����ǰ���ߴ������³���
    //float forwardRaycastLenth = 2.0f;
    ////����ǰ������Ϣ
    //RaycastHit forwardRaycastHitinfo = new RaycastHit();

    ////����������Ƿ���ײ
    //bool backRaycastIsCollision = false;
    ////��������߾�ѿ�¾���
    //float meiTobackRaycastDistance = 1;
    ////��������ߴ������³���
    //float backRaycastLenth = 2.0f;
    ////�����������Ϣ
    //RaycastHit backRaycastHitinfo = new RaycastHit();

    ////�����������Ƿ���ײ
    //bool leftRaycastIsCollision = false;
    ////���������߾�ѿ�¾���
    //float meiToleftRaycastDistance = 1;
    ////���������ߴ������³���
    //float leftRaycastLenth = 2.0f;
    ////������������Ϣ
    //RaycastHit leftRaycastHitinfo = new RaycastHit();

    ////�����������Ƿ���ײ
    //bool rightRaycastIsCollision = false;
    ////���������߾�ѿ�¾���
    //float meiTorightRaycastDistance = 1;
    ////���������ߴ������³���
    //float rightRaycastLenth = 2.0f;
    ////������������Ϣ
    //RaycastHit rightRaycastHitinfo = new RaycastHit();

    ////��ɫ���������Ƿ���ײ
    //bool centerDownRaycastIsCollision = false;
    ////��ɫ�������ߴ������³���
    //float centerDownRaycastLenth = 0.2f;
    ////��ɫ����������Ϣ
    //RaycastHit centerDownRaycastHitinfo = new RaycastHit();

    //void Start()
    //{
    //    startSpeed = 0;
    //    gravityAddSpeed = -10;
    //    usGravity = false;
    //}
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.U))
    //    {
    //        usGravity = !usGravity;
    //        startSpeed = 0;
    //    }

    //    Debug.DrawLine(transform.position + transform.forward * meiToforwardRaycastDistance + Vector3.up * forwardRaycastLenth,
    //         transform.position + transform.forward * meiToforwardRaycastDistance, Color.red);

    //    Debug.DrawLine(transform.position + -transform.forward * meiTobackRaycastDistance + Vector3.up * backRaycastLenth,
    //         transform.position + -transform.forward * meiTobackRaycastDistance, Color.red);

    //    Debug.DrawLine(transform.position + -transform.right * meiToleftRaycastDistance + Vector3.up * leftRaycastLenth,
    //         transform.position + -transform.right * meiToleftRaycastDistance, Color.red);

    //    Debug.DrawLine(transform.position + transform.right * meiTorightRaycastDistance + Vector3.up * rightRaycastLenth,
    //         transform.position + transform.right * meiTorightRaycastDistance, Color.red);

    //    //����ǰ����
    //    forwardRaycastIsCollision = Physics.Raycast(transform.position + transform.forward * meiToforwardRaycastDistance + Vector3.up * forwardRaycastLenth,
    //        -Vector3.up, out forwardRaycastHitinfo, forwardRaycastLenth);
    //    //���������
    //    backRaycastIsCollision = Physics.Raycast(transform.position + -transform.forward * meiTobackRaycastDistance + Vector3.up * backRaycastLenth,
    //      -Vector3.up, out backRaycastHitinfo, backRaycastLenth);
    //    //����������
    //    leftRaycastIsCollision = Physics.Raycast(transform.position + -transform.right * meiToleftRaycastDistance + Vector3.up * leftRaycastLenth,
    //      -Vector3.up, out leftRaycastHitinfo, leftRaycastLenth);
    //    //����������
    //    rightRaycastIsCollision = Physics.Raycast(transform.position + transform.right * meiTorightRaycastDistance + Vector3.up * rightRaycastLenth,
    //      -Vector3.up, out rightRaycastHitinfo, rightRaycastLenth);
    //    //��ɫ��������
    //    centerDownRaycastIsCollision = Physics.Raycast(transform.position, -Vector3.up, out centerDownRaycastHitinfo, centerDownRaycastLenth);

    //    //ȫ�������޼����Ϊ̤�� ��ô�½�
    //    if (forwardRaycastIsCollision == false && backRaycastIsCollision == false && leftRaycastIsCollision == false && rightRaycastIsCollision == false &&
    //        centerDownRaycastIsCollision == false)
    //    {
    //        //Debug.Log("RaycastCollderFalse");
    //        if (usGravity == true)
    //        {
    //            float _t = startSpeed * Time.unscaledDeltaTime + gravityAddSpeed * Time.unscaledDeltaTime * Time.unscaledDeltaTime / 2;
    //            startSpeed += gravityAddSpeed * Time.unscaledDeltaTime;
    //            transform.position += Vector3.up * _t;
    //        }
    //    }
    //    //������һ����������ײ��Ϊ վ��
    //    if (forwardRaycastIsCollision == true || backRaycastIsCollision == true || leftRaycastIsCollision == true || rightRaycastIsCollision == true)
    //    {
    //        if (forwardRaycastIsCollision == true)
    //        {
    //           // Debug.Log("Forward   " + forwardRaycastHitinfo.transform.name);
    //            Debug.Log(leftRaycastHitinfo.collider.gameObject.name);
    //        }
    //        if (backRaycastIsCollision == true)
    //        {
    //            //Debug.Log("Back   " + backRaycastHitinfo.transform.name);
    //            Debug.Log(leftRaycastHitinfo.collider.gameObject.name);
    //        }
    //        if (leftRaycastIsCollision == true)
    //        {
    //           // Debug.Log("Left   " + leftRaycastHitinfo.transform.name);
    //            Debug.Log(leftRaycastHitinfo.collider.gameObject.name);
    //        }
    //        if (rightRaycastIsCollision == true)
    //        {
    //         //   Debug.Log("Right   " + rightRaycastHitinfo.transform.name);
    //            Debug.Log(leftRaycastHitinfo.collider.gameObject.name);
    //        }
    //        startSpeed = 0;
    //    }
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        transform.position += Vector3.up * 0.22f;
    //        startSpeed = 8;
    //    }

    //}






















    ////��ײ��
    //public void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("OnCollisionEnter");
    //    // startSpeed = 0;
    //}
    //public void OnCollisionStay(Collision collision)
    //{
    //    Debug.Log("OnCollisionStay");
    //    // startSpeed = 0;
    //}
    //public void OnCollisionExit(Collision collision)
    //{
    //    Debug.Log("OnCollisionExit");
    //}
    ////������
    //public void OnTriggerStay(Collider other)
    //{
    //    Debug.Log("OnTriggerStay");
    //}
    //public void OnTriggerExit(Collider other)
    //{
    //    Debug.Log("OnTriggerExit");
    //}
    //public void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("OnTriggerEnter");
    //}



}
