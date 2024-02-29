using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodFactory : MonoBehaviour
{
   public static BloodFactory instance { get; private set; }

    public GameObject BloodPrefab;

    public List<GameObject> BloodPool;
    public int BloodPoolSize = 20;

    private void Awake()
    {
        instance = this;

        BloodPool = new List<GameObject>();
        for (int i = 0; i < BloodPoolSize; i++)
        {
            GameObject bloodObject = Instantiate(BloodPrefab);
            BloodPool.Add(bloodObject);
            bloodObject.SetActive(false);
        }
    }


    public void Make(Vector3 position, Vector3 normal)
    {
        foreach (GameObject bloodObject in BloodPool)
        {
            if (bloodObject.activeInHierarchy == false)
            {
                bloodObject.GetComponent<DestroyTime>()?.Init();
                bloodObject.transform.position = position;
                bloodObject.transform.forward = normal;
                bloodObject.SetActive(true);
                break;
            }
        }
    }
}

  

