using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpawn : MonoBehaviour
{
    [SerializeField] private GameObject knifePrefab;

    public void KnifeSpawning()
    {
        GameObject knifeInstance = Instantiate(knifePrefab);
    }
}
