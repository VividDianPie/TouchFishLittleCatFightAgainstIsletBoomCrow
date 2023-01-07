using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 天殛之境_裁决BezierCuves : MonoBehaviour
{
    Vector3 startPoint;
    public Vector3 controlPoint1;
    public Vector3 controlPoint2;
    Vector3 endPoint;
    Vector3[] bezierPath;
    float distance;
    float Speed;




    void Start()
    {
        Speed = 9;
        startPoint = HerrscherOfThunderMei.herrscherOfThunderMeiActor.position + new Vector3(0, 1.539f, 0);

        endPoint = BackBengMei.heroActor.position;
        controlPoint1 = HerrscherOfThunderMeiSkillBezierCuves.controlPointOne;
        controlPoint2 = HerrscherOfThunderMeiSkillBezierCuves.controlPointTwo;

        bezierPath = GetThreePowerBeizerList(startPoint, controlPoint1, controlPoint2, endPoint, 50);

    }
    void Update()
    {

        distance += Speed * Time.deltaTime;
        Vector3 pos = GetPoint(distance);
        this.transform.position = pos;


    }

    public Vector3 GetPoint(float dis)
    {
        Vector3 start = new Vector3();
        Vector3 end = new Vector3();
        float ds = GetPoints(dis, ref start, ref end);
        Vector3 dir = (end - start).normalized;
        transform.forward = dir;//物体朝向为移动方向
        return start + dir * ds;
    }
    public float GetPoints(float dis, ref Vector3 start, ref Vector3 end)
    {
        int bigger = 0;
        for (int i = 0; i < bezierPath.Length; i++)
        {
            if (GetDistance(i) > dis)
            {
                bigger = i;
                break;
            }
        }
        if (bigger == 0)
        {
            return 0.0f;
        }
        end = bezierPath[bigger];//找到当前对象正处的前后两个点
        start = bezierPath[bigger - 1];

        return dis - GetDistance(bigger - 1);//当前对象距离上一个经过的点走出了多远
    }
    float GetDistance(int index)
    {
        float ret = 0;
        if (index == 0)
        {
            return 0f;
        }

        for (int i = 1; i <= index; i++)
        {
            ret += (bezierPath[i] - bezierPath[i - 1]).magnitude;
        }

        return ret;
    }


    public static Vector3[] GetThreePowerBeizerList(
       Vector3 startPoint, Vector3 controlPoint1, Vector3 controlPoint2, Vector3 endPoint, int segmentNum)
    {
        Vector3[] path = new Vector3[segmentNum];
        for (int i = 1; i <= segmentNum; i++)
        {
            float t = i / (float)segmentNum;
            Vector3 pixel = CalculateThreePowerBezierPoint(t, startPoint, controlPoint1, controlPoint2, endPoint);
            path[i - 1] = pixel;
        }
        return path;
    }
    private static Vector3 CalculateThreePowerBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float ttt = tt * t;
        float uuu = uu * u;

        Vector3 p = uuu * p0;
        p += 3 * t * uu * p1;
        p += 3 * tt * u * p2;
        p += ttt * p3;
        return p;
    }
}
