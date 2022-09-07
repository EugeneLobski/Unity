using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManger : MonoBehaviour
{
    [SerializeField] private float _spawnDelay = 2f;

    void Start() {
        Spawner[] _spawners = gameObject.GetComponentsInChildren<Spawner>();
        StartCoroutine(SpawnEnemy(_spawners, _spawnDelay));
    }

    private IEnumerator SpawnEnemy(Spawner[] spawners, float delay) {
        while (true) // в задаче нет условия выхода... не стал ничего придумывать
        {
            foreach (Spawner spawner in spawners) {
                spawner.CreateEnemy();
                yield return new WaitForSeconds(delay);
            }
            
            yield return null;
        }        
    }
}
