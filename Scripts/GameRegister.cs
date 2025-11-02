using UnityEngine;

public static class GameRegister
{
    private static Register register = new Register();

    public static Register Get()
    {
        Debug.Log("Gameregister Get");
        return register;
    }

    public static void Set(Register newRegister)
    {
        register = newRegister;
    }
}
