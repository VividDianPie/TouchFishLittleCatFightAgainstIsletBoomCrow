using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastPhysics : MonoBehaviour
{
    //float startSpeed;
    //float gravityAddSpeed;
    //bool usGravity;

    ////世界前射线是否碰撞
    //bool forwardRaycastIsCollision = false;
    ////世界前射线距芽衣距离
    //float meiToforwardRaycastDistance = 1;
    ////世界前射线从上至下长度
    //float forwardRaycastLenth = 2.0f;
    ////世界前射线信息
    //RaycastHit forwardRaycastHitinfo = new RaycastHit();

    ////世界后射线是否碰撞
    //bool backRaycastIsCollision = false;
    ////世界后射线距芽衣距离
    //float meiTobackRaycastDistance = 1;
    ////世界后射线从上至下长度
    //float backRaycastLenth = 2.0f;
    ////世界后射线信息
    //RaycastHit backRaycastHitinfo = new RaycastHit();

    ////世界左射线是否碰撞
    //bool leftRaycastIsCollision = false;
    ////世界左射线距芽衣距离
    //float meiToleftRaycastDistance = 1;
    ////世界左射线从上至下长度
    //float leftRaycastLenth = 2.0f;
    ////世界左射线信息
    //RaycastHit leftRaycastHitinfo = new RaycastHit();

    ////世界右射线是否碰撞
    //bool rightRaycastIsCollision = false;
    ////世界右射线距芽衣距离
    //float meiTorightRaycastDistance = 1;
    ////世界右射线从上至下长度
    //float rightRaycastLenth = 2.0f;
    ////世界右射线信息
    //RaycastHit rightRaycastHitinfo = new RaycastHit();

    ////角色中下射线是否碰撞
    //bool centerDownRaycastIsCollision = false;
    ////角色中下射线从上至下长度
    //float centerDownRaycastLenth = 0.2f;
    ////角色中下射线信息
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

    //    //世界前射线
    //    forwardRaycastIsCollision = Physics.Raycast(transform.position + transform.forward * meiToforwardRaycastDistance + Vector3.up * forwardRaycastLenth,
    //        -Vector3.up, out forwardRaycastHitinfo, forwardRaycastLenth);
    //    //世界后射线
    //    backRaycastIsCollision = Physics.Raycast(transform.position + -transform.forward * meiTobackRaycastDistance + Vector3.up * backRaycastLenth,
    //      -Vector3.up, out backRaycastHitinfo, backRaycastLenth);
    //    //世界左射线
    //    leftRaycastIsCollision = Physics.Raycast(transform.position + -transform.right * meiToleftRaycastDistance + Vector3.up * leftRaycastLenth,
    //      -Vector3.up, out leftRaycastHitinfo, leftRaycastLenth);
    //    //世界右射线
    //    rightRaycastIsCollision = Physics.Raycast(transform.position + transform.right * meiTorightRaycastDistance + Vector3.up * rightRaycastLenth,
    //      -Vector3.up, out rightRaycastHitinfo, rightRaycastLenth);
    //    //角色中下射线
    //    centerDownRaycastIsCollision = Physics.Raycast(transform.position, -Vector3.up, out centerDownRaycastHitinfo, centerDownRaycastLenth);

    //    //全部射线无检测视为踏空 那么下降
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
    //    //有任意一条射线有碰撞视为 站立
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






















    ////对撞机
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
    ////触发器
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
