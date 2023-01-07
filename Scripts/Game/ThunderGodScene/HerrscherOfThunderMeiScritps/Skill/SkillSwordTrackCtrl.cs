using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class SkillSwordTrackCtrl : MonoBehaviour
{
    Vector3 swordFlyDir;
    bool cinemachineDollyCartIsActive;
    bool swordCanFlay;
    public int swordFlySpeed;
    bool backBengMeiIsParry;
    static int randomString;
    void Start()
    {
        swordCanFlay = false;
        backBengMeiIsParry = false;
        //����֮��_�þ� 7 ���ʧЧ
         Destroy(gameObject, 7);
        randomString++;
       // TimerMgr.Instance.OneShot(randomString.ToString(), 7.0f, DelayDestroyGameObject);
        cinemachineDollyCartIsActive = this.gameObject.GetComponent<CinemachineDollyCart>().enabled;

    }

    void Update()
    {
        if (swordCanFlay == true && backBengMeiIsParry == false)
        {
            this.gameObject.transform.position += swordFlyDir * Time.deltaTime * swordFlySpeed;
            this.gameObject.transform.forward = swordFlyDir;
        }
        else if (Vector3.Distance(this.gameObject.transform.parent.position, this.transform.position) >= 2.3f)
        {
            //������ �����������ʱ ���� BackBengMei
            swordCanFlay = true;
            //�������ʧЧ
            if (cinemachineDollyCartIsActive == true)
            {
                this.gameObject.GetComponent<CinemachineDollyCart>().enabled = false;
                cinemachineDollyCartIsActive = false;
                swordFlyDir = (BackBengMei.heroActor.position - gameObject.transform.position).normalized;
            }
        }
    }

    //BackBengMeiʱΪ����״̬ʱ ��ʧЧ׹��
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BackBengMei" &&
           BackBengMei.heroActor.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Parry"))
        {
            backBengMeiIsParry = true;
            this.gameObject.AddComponent<Rigidbody>();
            this.gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
        }
    }

    void DelayDestroyGameObject()
    {
        if (gameObject != null)
        {
            GameObject.Destroy(gameObject);
        }
    }


}
