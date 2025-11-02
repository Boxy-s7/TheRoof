using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ShopButtonHandler : MonoBehaviour
{
    public Store store;
    public List<GameObject> heroBackgrounds = new List<GameObject>();
    public List<GameObject> stoneBackgrounds = new List<GameObject>();
    public List<GameObject> netBackgrounds = new List<GameObject>();
    public List<GameObject> inventoryBackgrounds = new List<GameObject>();

    Color toHighColor = new Color(1f, 0f, 0f, 1f);
    
    Color curentColor = new Color(0f, 1f, 0f, 0.6f);
    Color nextColor = new Color(1f, 0.8f, 0f, 1f);
    Color toLowColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    private Outline outline;

    Register register;


    void Start()
    {



        store = GameStore.Get();
        register = GameRegister.Get();
        Debug.Log("hier auch");
        register.shopButtons = this;
        this.UpdateLevel(this.store.stone.level, stoneBackgrounds);
        this.UpdateLevel(this.store.hero.level, heroBackgrounds);
        this.UpdateLevel(this.store.net.level, netBackgrounds);

    }

    void OnEnable()
    {
        Debug.Log("ShopButtonHandler OnEnable");
        UpdateLevelNet(this.store.net.level);
        UpdateLevelStone(this.store.stone.level);
        UpdateLevelHero(this.store.hero.level);
    }
    void Update()
    {

    }

    public void UpdateLevelHero(int level)
    {
        this.UpdateLevel(level, heroBackgrounds);
    }
    
    public void UpdateLevelNet(int level)
    {
        this.UpdateLevel(level, netBackgrounds);
    }
    
    public void UpdateLevelStone(int level)
    {
        this.UpdateLevel(level, stoneBackgrounds);
    }

    private void UpdateLevel(int level, List<GameObject> backgrounds)
    {
        for (int i = 0; i < backgrounds.Count; i++)
        {
            int imageLevel = i + 1;
            Image image = backgrounds[i].GetComponent<Image>();
            if (imageLevel < level)
            {
                image.color = toLowColor;
            }
            else if (imageLevel == level)
            {
                image.color = curentColor;
            }
            else if (imageLevel - 1 == level)
            {
                image.color = nextColor;
            
            }
            else
            {
                image.color = toHighColor;
            }
        }
    }
}