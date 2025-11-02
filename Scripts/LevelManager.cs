using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using Unity.VisualScripting;
using System;

public class LevelManager : MonoBehaviour
{
   private Store store;
   private Register register;
   public GameObject levelUpAnimation;
   public GameObject animationb;
   public Dictionary<string, int> istDict = new();
   public List<LevelEntry> solls = new();

   public TMP_InputField textMeshTaler;
   public int progress;

   void Start()
   {
      this.register = GameRegister.Get();
      this.store = GameStore.Get();
      this.register.levelManager = this;
      this.istDict.Add(this.solls[store.hero.level].name, this.store.hero.progress);
      progress = this.store.hero.progress;
   }

   void Update()
   {

   }

   public void LevelCheckUp(string name)
   {
      int alt = istDict.GetValueOrDefault(name, 0);
      int neu = alt + 1;
      istDict[name] = neu;
      LevelEntry current = this.solls[store.hero.level];
      if (name == current.name)
      {
         if (neu >= current.soll)
         {
            LevelUp();
         }
         else
         {
            this.store.hero.progress = neu;
         }         
      }
      progress = this.store.hero.progress;
   }
   public async void LevelUp()
   {
      this.store.hero.level++;
      this.store.hero.progress = 0;
      animationb = Instantiate(levelUpAnimation, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
      var b = animationb;
      await Task.Delay(2000);
      Destroy(b);
   }
}

[System.Serializable]
public class LevelEntry
{
   public string name;
   public int soll;

}