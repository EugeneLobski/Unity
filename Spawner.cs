using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField] private GameObject pfEnemy;
   
    public void CreateEnemy() {
        Instantiate(pfEnemy, transform.position, Quaternion.identity, transform);
    }
}
