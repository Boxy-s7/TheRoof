using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class EimerScript : ItemScript
{
    public GameObject BrokenEggPrefab;
    public bool isMoving = false;
    public float eimerX = 0.3492f;

    public bool isRotating = false;
    public float lastX;


    // Start is called before the first frame update
    public override string ItemType()
    {
        return "Eimer";
    }

    public override void Register()
    {
        this.register.eimers.Add(this);
        UpgradeEimer();

    }

    public void UpgradeEimer()
    {
        gameObject.transform.localScale = new Vector3(store.inventory.eimers.size * eimerX, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        

    }



    void Update()
    {
        if (isMoving)
        {
            if (DetectPress())
            {
                isMoving = false;
                GetComponent<BoxCollider2D>().enabled = true;
                this.register.market.BuySthStop();
                this.store.inventory.eimers.eimers.Add(new Eimer(transform.position.x, transform.position.y));
                Debug.Log("new eimer added" + this.store.inventory.eimers.count);
            }
            transform.position = new Vector3(GetPosition().x, transform.position.y, 0);
        }

    }
    
    public void Move()
    {
        this.isMoving = true;
        GetComponent<BoxCollider2D>().enabled = false;
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Stone"))
        {
            store.hero.taler += store.stone.talerProStein;
            collision.transform.SetParent(this.transform);
            register.levelManager.LevelCheckUp("eimer");
    
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        }

    }
}
