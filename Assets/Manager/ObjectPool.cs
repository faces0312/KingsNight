using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject michaelPrefab;
    public GameObject monkPrefab;
    public GameObject thiefPrefab;
    GameObject[] michael;
    GameObject[] monk;
    GameObject[] thief;

    GameObject[] targetPool;
    private void Awake()
    {
        michael = new GameObject[50];
        monk = new GameObject[50];
        thief = new GameObject[50];

        Generate();
    }

    void Generate()
    {
        for(int index =0; index < michael.Length; index++)
        {
            michael[index] = Instantiate(michaelPrefab);
            michael[index].SetActive(false);
        }
        for (int index = 0; index < monk.Length; index++)
        {
            monk[index] = Instantiate(monkPrefab);
            monk[index].SetActive(false);
        }
        for (int index = 0; index < thief.Length; index++)
        {
            thief[index] = Instantiate(thiefPrefab);
            thief[index].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "Michael":
                targetPool = michael;
                break;
            case "Monk":
                targetPool = monk;
                break;
            case "Thief":
                targetPool = thief;
                break;
        }
        for (int index = 0; index < targetPool.Length; index++)
        {
            if(!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null;
    }
}
