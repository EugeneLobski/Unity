using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManger : MonoBehaviour
{
    [SerializeField] private float _spawnDelay = 2f;

    private float _timer = 0f;
    private Spawner[] _spawners;
    private int spawnIndex = 0;

    private void Awake() {
        _spawners = gameObject.GetComponentsInChildren<Spawner>();
    }
    private void FixedUpdate()
    {
        if (_timer < _spawnDelay) {
            _timer += Time.deltaTime;
        } else {
            _spawners[spawnIndex].CreateEnemy();
            _timer = 0f;

            if (spawnIndex < _spawners.Length - 1) {
                spawnIndex++;
            } else {
                spawnIndex = 0;
            }
        }
    }
}
