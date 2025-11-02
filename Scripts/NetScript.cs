using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NetScript : MonoBehaviour
{
    
    // Start is called before the first frame update

    private Store store;
    public Register register;

    
        private SpriteRenderer spriteRenderer;
    public List<GameObject> stones = new List<GameObject> { };


    void Start()
    {
        this.store = GameStore.Get();
        this.register = GameRegister.Get();
        this.register.net = this;
    }

    // Update is called once per frame

    void Update()

    {


    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Stone") && store.net.steinmenge < store.net.maxSteinmenge)
        {
            
            register.levelManager.LevelCheckUp("net");
        
            
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

            // Taler hinzufügen
            store.net.steinmenge += 1;


        }

    }

    void Awake()

    {

        Invoke(nameof(NetzLehrung), 1f);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }
    void NetzLehrung()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("Kein SpriteRenderer auf diesem GameObject!");
        }

        store = GameStore.Get();
        if (store == null)
        {

            return;
        }

        if (store.net == null)
        {
            store.net = new Net();
        }

        store.net.steinmenge = 0;

    }
    public void ClearNet()
    {
        for (int i = 0; i < stones.Count; i++)
        {
            GameObject stone = stones[i];
            Destroy(stone);
        }
        stones.Clear();


    }
    


}
