using Unity.VisualScripting;
using UnityEngine;

public static class GameStore
{
    private static Store store;

    public static Store Get()
    {
        return store;
    }

    public static void Set(Store newStore)
    {
        store = newStore;
    }

    public static Store Init()
    {
        store.hero.level = 0;
        store.hero.taler = 100;
        store.hero.progress = 0;
        store.stone.talerProStein = 1;
        store.net.steinmenge = 0;
        store.net.maxSteinmenge = 3;
        store.stone.level = 0;
        store.net.level = 0;
        store.inventory.bretters.Clear();
        store.inventory.eimers.eimers.Clear();
        store.inventory.eimers.count = 0;
        store.inventory.eimers.size = 0;
        store.inventory.eimers.level = 0;
        store.inventory.zombie.zombieLevel = 0;
        store.inventory.drone.dronenLevel = 0;
        store.inventory.drone.speed = 0;
        store.inventory.drone.smartnis = 0;
        store.inventory.nest.nestLevel = 0;
        store.inventory.nest.taste = 0;
        store.inventory.nest.scent = 0;
        store.inventory.nest.bird = 0;
        store.inventory.nest.positionX = 0;
        store.inventory.nest.positionY = 0;
        store.storeSettings.musicVolume = 0;
        store.storeSettings.effectVolume = 0;
        return store;
    }
}
