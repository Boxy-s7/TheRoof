using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieNetScript : MonoBehaviour
{
    private Store store;
    public Register register;
    public ZombieLevelStats levelStats;
    public int Zombiesteinmenge
    {
        get { return stones.Count; } 
    }
     public List<GameObject> stones = new List<GameObject> { };
    // Start is called before the first frame update
    void Start()
    {
        this.store = GameStore.Get();
        this.register = GameRegister.Get();
        this.register.ZombieNet = this;
       UpgrandeZombieNet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Stone"))
        {

            // Stein als Kind vom Netz setzen
            collision.transform.SetParent(this.transform);

            // Rigidbody2D einfrieren
            var rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.isKinematic = true;
                this.stones.Add(collision.gameObject);
            }
            var col = collision.gameObject.GetComponent<Collider2D>();
            if (col != null)
            {
                col.enabled = false;
            }
            register.levelManager.LevelCheckUp("zombie");

            // Taler hinzufügen
            


        }


    }
    public void ClearZombieNet()
    {
        this.store.hero.taler += Zombiesteinmenge * this.store.stone.talerProStein;


        for (int i = 0; i < stones.Count; i++)
        {
            GameObject stone = stones[i];
            Destroy(stone);
        }
        stones.Clear();



    }
    public void UpgrandeZombieNet()
    {
        this.levelStats = LevelStats.zombie.GetValueOrDefault(this.store.inventory.zombie.zombieLevel);
        transform.localScale = new Vector3(this.levelStats.zombieNet * 0.2f, transform.localScale.y, transform.localScale.z);
        transform.localPosition = new Vector3(this.levelStats.zombieNet * 0.1f, transform.localPosition.y, transform.localPosition.z);
    }
}
