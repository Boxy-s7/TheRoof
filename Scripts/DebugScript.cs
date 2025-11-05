using UnityEngine;
using TMPro;

public class InputHandler : MonoBehaviour
{
    public TMP_InputField inputField;  // Eingabe-Feld aus dem Inspector
    public Store store;                // dein Store-Objekt

    void Start()
    {
        // Event abonnieren
        store = GameStore.Get();
        inputField.onEndEdit.AddListener(OnInputChanged);
    }

    void OnInputChanged(string input)
    {
        // Beispiel: Eingabe in int umwandeln und den Stein-Level setzen
        if (int.TryParse(input, out int value))
        {
            store.hero.level = value;
            store.hero.taler = value * 1000;
        }
        else
        {
            Debug.LogWarning("Eingabe ist keine Zahl!");
        }
    }
}