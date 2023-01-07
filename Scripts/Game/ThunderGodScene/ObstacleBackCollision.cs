using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBackCollision : MonoBehaviour
{
    GameObject backBengMeiMirror;
    GameObject herrscherOfThunderMeiMirror;
    RaycastHit hitinfo;

    private void Update()
    {

     //   if (Physics.Raycast(new Vector3(23.29f, 1.695f, 22.232f), -this.transform.right, out hitinfo, 50f) == true)
     //   {
     //       if (hitinfo.transform.tag == "BackBengMei" && backBengMeiMirror == null)
     //       {
     //           backBengMeiMirror = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Mirror"));
     //           backBengMeiMirror.transform.SetParent(transform);
     //       }
     //       else
     //       {
     //           this.transform.GetChild(0).position = new Vector3(
     //               BackBengMei.heroActor.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
     //       }
     //   }


        //if (Physics.Raycast(new Vector3(23.29f, 1.695f, 22.232f), -this.transform.right, out hitinfo, 50f) == true)
        //{
        //    if (hitinfo.transform.name == "HerrscherOfThunderMei" && herrscherOfThunderMeiMirror == null)
        //    {
        //        herrscherOfThunderMeiMirror = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/Mirror"));
        //        herrscherOfThunderMeiMirror.transform.SetParent(transform);
        //    }
        //    else if(herrscherOfThunderMeiMirror == true)
        //    {
        //        this.transform.GetChild(1).position = new Vector3(
        //          HerrscherOfThunderMei.herrscherOfThunderMeiActor.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        //    }
        //}
          


    }



}
