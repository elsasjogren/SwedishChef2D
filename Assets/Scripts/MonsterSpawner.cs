using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject myPrefab;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 pos = new Vector3(0, 0, 0);
        Instantiate(myPrefab, pos, Quaternion.identity);
    }

    //void DirectionOfEnemy ()
    ////{
    ////    if (!grounded)
    ////    {
    ////        direction = 0;
    ////        return 0;
    ////    }
    //}
}
