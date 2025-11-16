using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Connect4DisplayScript : MonoBehaviour
{
    public int color;
    public List<Sprite> symbols;
    public Image symbol;
    // Start is called before the first frame update
    void Start()
    {
        ChangeColor(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeColor(int color)
    {
        symbol.sprite = symbols[color];
        this.color = color;
    }
}
