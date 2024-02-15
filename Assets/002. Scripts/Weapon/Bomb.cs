using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject BoomEffectPrefab;

    public GameObject BombPrefab;

    private void OnCollisionEnter(Collision collision)  
    {
        if(collision.collider.tag != "Player")
        {

            gameObject.SetActive(false);

            Instantiate(BoomEffectPrefab, transform.position, transform.rotation);
            
        }
    }
}