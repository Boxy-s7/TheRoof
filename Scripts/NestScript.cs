using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class NestScript : MonoBehaviour
{
    public bool isMoving;
    public Vector3 nestPos;
    public GameObject nestPosGameObject;
    public Register register;
    public Transform spawnPoint;     // Position, an der gespawnt wird
    public float nextSpawn;
    public float SpawnCooldown;
    public List<GameObject> birds = new List<GameObject>();
    public Store store;




    // Start is called before the first frame update
    void Start()
    {
        this.store = GameStore.Get();
        register = GameRegister.Get();
        register.nest = this;
        this.nextSpawn = Time.time + this.SpawnCooldown;
        nestPos = nestPosGameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= this.nextSpawn)
        {
            this.nextSpawn = Time.time + this.SpawnCooldown;
            this.SpawnPrefab();

        }
        if (isMoving)
        {
            if (DetectPress())
            {
                isMoving = false;
                GetComponent<BoxCollider2D>().enabled = true;
                gameObject.GetComponentInChildren<BoxCollider2D>().enabled = true;
                this.register.market.BuySthStop();
                this.store.inventory.nest.positionX = transform.position.x;
                this.store.inventory.nest.positionY = transform.position.y;
                nestPos = gameObject.transform.position;
            }
            transform.position = new Vector3(GetPosition().x, transform.position.y, 0);
        }

    }



    public void Move()
    {
        this.isMoving = true;
        GetComponent<BoxCollider2D>().enabled = false;
        foreach (BoxCollider2D col in gameObject.GetComponentsInChildren<BoxCollider2D>())
    {
    col.enabled = false;
    }

    }
    public int factor = 1;
    public void SpawnPrefab()
    {
        if (!isMoving)
        {
            float zufallsY = Random.Range(5f, 0f) * factor;
            int richtung = Random.Range(-1f, 1f) > 0 ? 1 : -1;
            Vector3 neuePosition = new Vector3(10 * richtung, zufallsY, transform.position.z);
            GameObject prefabToSpawn = this.birds[this.store.inventory.nest.bird];
            Instantiate(prefabToSpawn, neuePosition, Quaternion.identity);
            factor *= -1;
        }
    }
    bool DetectPress()
    {
        return DetectTouch() || DetectMouse();
    }
    bool DetectTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Finger hat den Screen berührt");
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                Debug.Log("Finger wird gehalten");
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                Debug.Log("Finger losgelassen");
                return true;
            }
        }
        return false;
    }

    bool DetectMouse()
    {
        // Wird nur im Frame ausgelöst, wenn die Maus gedrückt wird
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Maus gedrückt");
        }

        // Solange die Maustaste gedrückt bleibt
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Maus wird gehalten");
        }

        // Wird nur im Frame ausgelöst, wenn die Maus losgelassen wird
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Maus losgelassen");
            return true;
        }
        return false;
    }
    private Vector3 GetPosition()
    {
        // Mausposition von Bildschirm in Welt umrechnen
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Z-Koordinate anpassen, weil ScreenToWorldPoint sonst - je nach Kamera – Blödsinn liefert
        mousePos.z = 0f;

        // Objekt-Position setzen
        return mousePos;
    }
}
