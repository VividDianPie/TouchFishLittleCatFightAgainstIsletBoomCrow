using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundCheck : MonoBehaviour
{
    List<GameObject> popWeaponUiList;
    GameObject popWeaponUi;
    string havePopWeapon;
    void Start()
    {
        popWeaponUiList = new List<GameObject>();
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 1);
        if (popWeaponUiList.Count == 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].tag == "PopWeapon")
                {
                    popWeaponUi = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefab/Uis/PopWeaponUi"));
                    popWeaponUiList.Add(popWeaponUi);
                    return;
                }
            }
        }
        else if (popWeaponUiList.Count > 0)
        {
            havePopWeapon = "NoHavePopWeapon";
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].tag == "PopWeapon")
                {
                    havePopWeapon = "HavePopWeapon";
                    return;
                }
            }
            if (havePopWeapon == "NoHavePopWeapon")
            {
                Destroy(popWeaponUi);
                popWeaponUiList.Clear();
                return;
            }
        }
    }






}
