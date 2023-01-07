using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster0 : MonoBehaviour
{
    
    public Animator At;


    public Rigidbody Rb;


    protected ActionMachine mAm;
    protected BehaviorTree mBt;


    //true表示死 false活着
    protected bool mIsDead;
  


    void Awake()
    {
        mIsDead = false;

        //我们行为状态
        mAm = new ActionMachine(this.gameObject);

        //AI状态
        mBt = new BehaviorTree(this.gameObject,mAm, new AIMstIdle(this.gameObject, mAm, mBt, BehaviorCondition.EType.And));

        mBt.AddChild(mBt.Root, new AIMstAttack(this.gameObject, mAm, mBt, BehaviorCondition.EType.And, IsHeroCloseTo));
    }


    void Start()
    {
        //AI行为的Enter
        mBt.OnEnter();
    }


    void Update()
    {
        mAm.Update();
        //角色死亡AI就没有必要更新
        if (!mIsDead)
        {
            mBt.Update();
        }
    }


    public void OnAnimationStart()
    {
        mAm.OnAnimationStart();
    }


    public void OnAnimationHit(int i)
    {
        mAm.OnAnimationHit(i);
    }


    public void OnAnimationEnd()
    {
        mAm.OnAnimationEnd();
    }


    public void OnCollisionEnter(Collision collision)
    {
        mAm.OnCollisionEnter(collision);
    }


    public void OnCollisionStay(Collision collision)
    {
        mAm.OnCollisionStay(collision);
    }


    public void OnCollisionExit(Collision collision)
    {
        mAm.OnCollisionExit(collision);
    }


    //死亡超时处理
    public void OnDestroyDelay()
    {
        Vector3 pos = gameObject.transform.position;

        //让本游戏物体消失
        GameObject.DestroyImmediate(gameObject);

        //产生宝箱
        GameObject obj = Resources.Load<GameObject>("Prefabs/Box");
        obj = GameObject.Instantiate<GameObject>(obj);
        pos.y += 0.5f;
        obj.transform.position = pos;
    }



    //行为条件函数
    public bool IsHeroCloseTo()
    {
        //搜索目标
        GameObject heroObj = GameObject.FindGameObjectWithTag("Hero");
        float disToHero = Vector3.Distance(gameObject.transform.position, heroObj.transform.position);
        if (disToHero < 1.5f)
        {
            return true;
        }

        return false;
    }
}
