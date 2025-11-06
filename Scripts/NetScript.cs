using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NetScript : MonoBehaviour
{

    // Start is called before the first frame update
    public string levelCheckUp;
    public string catchTag;
    protected Store store;
    public Register register;
    public LevelStats levelStats;
    protected SpriteRenderer spriteRenderer;
    public List<GameObject> catchedObjects = new List<GameObject> { };


    void Start()
    {
        this.store = GameStore.Get();
        this.register = GameRegister.Get();
        this.register.net = this;
        catchTag = LevelStats.net[store.net.level].catchedObject;
    }

    // Update is called once per frame

    void Update()

    {


    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(catchTag) && store.net.steinmenge < store.net.maxSteinmenge)
        {

            register.levelManager.LevelCheckUp(levelCheckUp);


            ConnectGamobjects(collision.gameObject);

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
        for (int i = 0; i < catchedObjects.Count; i++)
        {
            GameObject stone = catchedObjects[i];
            Destroy(stone);
        }
        catchedObjects.Clear();


    }
    public void ConnectGamobjects(GameObject collision)
    {
        collision.transform.SetParent(this.transform);

            // Rigidbody2D einfrieren
            var rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.isKinematic = true;
                this.catchedObjects.Add(collision);
            }
            var col = collision.GetComponent<Collider2D>();
            if (col != null)
            {
                col.enabled = false;
            }
    }

}
