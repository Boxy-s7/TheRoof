using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketScript : MonoBehaviour

{
   public GameObject shop;
   public Store store;
   public Register register;


   // Start is called before the first frame update
   void Start()
   {
      this.store = GameStore.Get();
      this.register = GameRegister.Get();
      this.register.market = this;
   }

   // Update is called once per frame
   void Update()
   {

   }
   void OnCollisionEnter2D(Collision2D collision)
   {
      if (collision.gameObject.CompareTag("player"))
      {
         this.shop.SetActive(true);
         this.Sell();
      }



   }
   void OnCollisionExit2D(Collision2D collision)
   {
      if (collision.gameObject.CompareTag("player"))
      {
         this.shop.SetActive(false);
      }



   }
   public void Sell()
   {
      this.store.hero.taler += this.store.net.steinmenge * this.store.stone.talerProStein;
      this.store.net.steinmenge = 0;
      this.register.net.ClearNet();

   }
   public void BuySthStart()
    {
        this.shop.SetActive(false);
        register.player.SetCanMove(false);
        
    }
    public void BuySthStop()
    {
        this.shop.SetActive(true);
        register.player.SetCanMove(true);
        
    }
   
   
}
