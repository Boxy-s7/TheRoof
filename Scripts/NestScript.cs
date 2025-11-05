using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestScript : MonoBehaviour
{
    public Vector3 nestPos;
    public Register register;
    public GameObject prefabToSpawn; // Dein Prefab
    public Transform spawnPoint;     // Position, an der gespawnt wird
    public float nextSpawn;
    public float SpawnCooldown = 30f;
    public List<GameObject> birds = new List<GameObject>();
    public Store store;




    // Start is called before the first frame update
    void Start()
    {
        this.store = GameStore.Get();
        register = GameRegister.Get();
        nestPos = gameObject.transform.position;
        register.nest = this;
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
        float zufallsY = Random.Range(5f, 0f) * factor;
        int richtung = Random.Range(-1f, 1f) > 0 ? 1 : -1;
        Vector3 neuePosition = new Vector3(10 * richtung, zufallsY, transform.position.z);
        GameObject prefabToSpawn = this.birds[this.store.inventory.nest.nestLevel];
        Instantiate(prefabToSpawn, neuePosition, Quaternion.identity);
        factor *= -1;
    }
    
}
