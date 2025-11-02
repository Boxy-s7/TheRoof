using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronenBodyScript : ItemScript
{
    public DroneLevelStats levelStats;
    public int steinmenge;
    
    public List<GameObject> stones = new List<GameObject> { };
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {

    }
     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Stone") && steinmenge < store.inventory.drone.maxSteinmenge)
        {
            Debug.Log("collision detectet");
            // Stein als Kind vom Netz setzen
            collision.transform.SetParent(this.transform);

            // Rigidbody2D einfrieren
            var rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.isKinematic = true;
                
            }
            var col = collision.gameObject.GetComponent<Collider2D>();
            if (col != null)
            {
                col.enabled = false;
            }

            // Taler hinzufügen
            steinmenge += 1;
            register.levelManager.LevelCheckUp("drone");
        
            this.stones.Add(collision.gameObject);
            if (steinmenge == store.inventory.drone.maxSteinmenge)
            {
                register.drone.drone.Full();
                Debug.Log("calling full");
            }
            else
            {
                register.drone.drone.Home();
                Debug.Log("calling Home");
                if (store.inventory.drone.smartnis > 2)
                {
                    register.drone.dronenDetector.Scan();
                }
                
            }

        }

    }

    public override string ItemType()
    {
        return "dronenBody";
    }

    public override void Register()
    {
        this.register.drone.dronenBody = this;
    }
    public void Sell()
    {
        this.store.hero.taler += steinmenge * this.store.stone.talerProStein;


        for (int i = 0; i < stones.Count; i++)
        {
            GameObject stone = stones[i];
            Destroy(stone);
        }
        stones.Clear();
        steinmenge = 0;
        register.drone.dronenDetector.Scan();


    }
}
