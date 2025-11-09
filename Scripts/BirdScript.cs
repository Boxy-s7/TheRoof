using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using System.Threading.Tasks;

public class BirdScript : MonoBehaviour
{
    public float frame;
    private SpriteRenderer spriteRenderer;
    public List<Sprite> fly = new List<Sprite>();
    public List<Sprite> eat = new List<Sprite>();
    public int spawnSite;
    public Store store;
    public Register register;
    public String mode = "Find food";
    public Vector3 target;
    public float speedFactor = 1;
    public float pickFactor;
    public GameObject eggPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.store = GameStore.Get();
        register = GameRegister.Get();
        Debug.Log(register.nest.nestPos);
        this.register.bird = this;
        if (gameObject.transform.position.x > 0)
        {
            spawnSite = -1;
        }
        else
        {
            spawnSite = 1;
            transform.Rotate(new Vector3(0, 180, 0));
        }
        target = new(register.nest.nestPos.x + spawnSite * pickFactor, register.nest.nestPos.y, register.nest.nestPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (mode == "Find food" || mode == "Fly home")
        {
            spriteRenderer.sprite= fly[(int)Mathf.Round(frame / 80)];
            frame++;
            if (frame >= 500)
            {
                frame = 0;
            }
        }
        if (mode == "Find food")
        {
            UpdateFindfood();
        }

        if (mode == "Eating")
        {
            spriteRenderer.sprite = eat[(int)Mathf.Round(frame / 80)];
            frame++;
            if (frame >= 160)
            {
                frame = 0;
            }
        }
        if (mode == "Fly home")
        {
            transform.position = Vector3.MoveTowards(
                    transform.position,
                    target,
                    speedFactor * Time.deltaTime);
            if (gameObject.transform.position == target)
            {
                DestroyImmediate(gameObject);
            }
        }
        
    }
    private void UpdateFindfood()
    {
        transform.position = Vector3.MoveTowards(
                    transform.position,
                    target,
                    speedFactor * Time.deltaTime);
        if (gameObject.transform.position == target)
        {
            Eat();
            mode = "Eating";
            frame = 0;
        }
    }
    public async void Eat()
    {
        await Task.Delay(this.store.inventory.nest.taste * 1000);
        mode = "Fly home";
        target = new Vector3(10 * spawnSite, UnityEngine.Random.Range(5f, 0f));
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Stone"))
        {
            Instantiate(eggPrefab, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}
