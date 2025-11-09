using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class BrettScript : ItemScript
{
    public override string ItemType()
    {
        return "Brett";
    }

    public bool isMoving = false;

    public bool isRotating = false;
    public float lastX;
    // Start is called before the first frame update
    public override void Register()
    {
        this.register.bretters.Add(this);
        Debug.Log("brett start" + transform.rotation.z);   
    }


    public void Move()
    {
        isMoving = true;
        GetComponent<BoxCollider2D>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (DetectPress())
            {
                isRotating = true;
                isMoving = false;
            }
            transform.position = GetPosition();
        }
        else if (isRotating)
        {
            if (DetectPress())
            {
                isRotating = false;
                
                GetComponent<BoxCollider2D>().enabled = true;
                this.register.market.BuySthStop();
                this.store.inventory.bretters.Add(new Brett(transform.position.x, transform.position.y, transform.eulerAngles.z));
                Debug.Log("new brett added" + this.store.inventory.bretters.Count);
            }
            else
            {
                var x = GetPosition().x;

                transform.Rotate(0, 0, (lastX - x) * 10);
                lastX = x;

            }
            


        }
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
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Egg") && LevelStats.brett[store.inventory.bretters.Count].toHard == true)
        {
            Destroy(collision.gameObject);
        }
        else
        {
            register.levelManager.LevelCheckUp("brett");
        }
    }
}
