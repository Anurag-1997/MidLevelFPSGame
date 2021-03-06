using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    public GameObject zombiePrefab;

    public int number;
    public float spawnRadius;
    public bool spawnOnStart = true;
    // Start is called before the first frame update
    void Start()
    {
        if (spawnOnStart)
        {
            SpawnAllZombies();
        }

    }

    private void SpawnAllZombies()
    {
        for (int i = 0; i < number; i++)//we want to randomly spawn zombies.
        {
            Vector3 randomPoint = this.transform.position + Random.insideUnitSphere * spawnRadius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 10.0f, NavMesh.AllAreas))
            {
                Instantiate(zombiePrefab, hit.position, Quaternion.identity);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!spawnOnStart)
        {
            if(other.gameObject.tag=="Player")
            {
                SpawnAllZombies();
            }    
            
        }
       
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
