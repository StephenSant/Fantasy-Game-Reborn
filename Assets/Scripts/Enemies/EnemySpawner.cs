using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public struct EnemyPoint
    {
        public Transform areaPoint;
        public GameObject enemy;
        public float area;
        public int minAmount;
        public int maxAmount;
    }

    public EnemyPoint[] enemyPoints;

    void Start()
    {
        for (int i = 0; i < enemyPoints.Length; i++)
        {
            int randomNum;
            randomNum = Random.Range(enemyPoints[i].minAmount-1, enemyPoints[i].maxAmount);
            for (int a = 0; a <= randomNum; a++)
            {
                Instantiate(enemyPoints[i].enemy, 
                    enemyPoints[i].areaPoint.position + new Vector3(Random.Range(0,enemyPoints[i].area),0,Random.Range(0, enemyPoints[i].area)),
                    enemyPoints[i].areaPoint.rotation,
                    null);
            }

        }

    }
}
