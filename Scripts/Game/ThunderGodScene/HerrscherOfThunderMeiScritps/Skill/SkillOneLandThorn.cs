using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillOneLandThorn : MonoBehaviour
{
    [Header("ThornUpSpeed")]
    public float speed; 

    void Awake()
    {
        this.transform.position = new Vector3(
            HerrscherOfThunderMei.herrscherOfThunderMeiActor.position.x, HerrscherOfThunderMei.herrscherOfThunderMeiActor.position.y - 1.514f, 
            HerrscherOfThunderMei.herrscherOfThunderMeiActor.position.z);
        this.gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }
    void Start()
    {
        Destroy(this.gameObject, 3.5f);
    }

    void Update()
    {
        if(this.gameObject.transform.position.y >= 1.257f  )
        {
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            return;
        }
        this.gameObject.transform.position = new Vector3(
            transform.position.x, transform.position.y + Time.deltaTime * speed, transform.position.z);

    }




}
