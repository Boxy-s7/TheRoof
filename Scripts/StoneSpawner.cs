using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StoneSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn; // Dein Prefab
    public Transform spawnPoint;     // Position, an der gespawnt wird
    public float nextSpawn;
    public float SpawnCooldown = 2f;
    public List<GameObject> stones = new List<GameObject>();
    public Store store;




    // Start is called before the first frame update
    void Start()
    {
        
        this.store = GameStore.Get();
        this.nextSpawn = Time.time + this.SpawnCooldown;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= this.nextSpawn){
            this.nextSpawn = Time.time + this.SpawnCooldown;
            this.SpawnPrefab();

        }
    }




    public int factor = 1;
    public void SpawnPrefab()
    {
        float zufallsX = Random.Range(8f, -10f) * factor;
        Vector3 neuePosition = new Vector3(zufallsX, transform.position.y, transform.position.z);
        transform.position = neuePosition;
        GameObject prefabToSpawn = this.stones[this.store.stone.level];
        Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
        factor *= -1;
    }








}