using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.UI;

public class StoreAnzeige : MonoBehaviour
{
    public Transform detailPrefab;
    [Header("UI Root Panel (Fallback)")]
    [Tooltip("Main vertical panel that will contain group panels if not explicitly overridden.")]
    public Transform panel;

    [Header("Custom Substore Panels")]
    [Tooltip("Optional: Map store sub-objects (like hero, stone) to custom display names and panels.")]
    public List<SubstoreEntry> substores = new();

    [Header("Field Label Overrides")]
    [Tooltip("Optional: Override specific field names with custom labels.")]
    public List<EntryLabel> entries = new();

    [Header("Group Panel Layout")]
    public float groupSpacing = 10f;
    public RectOffset groupPadding;
    public TextAnchor groupChildAlignment = TextAnchor.UpperLeft;

    [Header("Field Row Layout")]
    public float fieldSpacing = 5f;
    public float labelMinWidth = 100f;
    public float inputMinWidth = 60f;
    public int fontSize = 1;

    public Store store;

    private List<(Func<string> getter, TMP_InputField input)> bindings = new();

    [Serializable]
    public class SubstoreEntry
    {
        public string name;               // e.g., "hero"
        public string displayName = "";   // optional override
        public Transform panelOverride;   // optional override panel
    }

    [Serializable]
    public class EntryLabel
    {
        public string name;     // e.g., "coinsPerStone"
        public string label;    // e.g., "Coins / Stone"
    }

    void Start()
    {
        this.store = GameStore.Get();  
        groupPadding = new RectOffset(10, 10, 5, 5);
        CreateUIForObject(store);
    }

    void Update()
    {
        foreach (var (getter, input) in bindings)
            input.text = getter();
    }
void CreateUIForObject(object obj)
    {
        Type type = obj.GetType();
        Debug.Log("Makiring");
        foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Public))
        {
        Debug.Log("Makiring2");
            object subObj = field.GetValue(obj);
            string groupName = field.Name;

            // Try to find override for this substore
            SubstoreEntry substoreOverride = substores.Find(s => s.name == groupName);
            string displayName = substoreOverride?.displayName ?? groupName;
            Transform parentPanel = substoreOverride?.panelOverride ?? panel;

            if (parentPanel == null)
            {
                Debug.LogWarning($"[StoreUIBinder] No panel provided for group '{groupName}', and default panel is null.");
                continue;
            }

            // Create group panel
            var groupGO = new GameObject(displayName + "_Panel", typeof(RectTransform));
            groupGO.transform.SetParent(parentPanel);
            var layout = groupGO.AddComponent<VerticalLayoutGroup>();
            layout.spacing = fieldSpacing;
            layout.childControlWidth = true;
            layout.childControlHeight = true;
layout.childAlignment = groupChildAlignment;
            layout.padding = groupPadding;

            var groupTransform = groupGO.transform;

            foreach (var innerField in field.FieldType.GetFields(BindingFlags.Instance | BindingFlags.Public))
            {
                string fieldName = innerField.Name;

                // Check for label override
                EntryLabel labelOverride = entries.Find(e => e.name == fieldName);
                string label = labelOverride?.label ?? fieldName;

                // Create horizontal container
                var containerGO = new GameObject(label + "_Row", typeof(RectTransform));
                containerGO.transform.SetParent(groupTransform);
                var rowLayout = containerGO.AddComponent<HorizontalLayoutGroup>();
                rowLayout.spacing = fieldSpacing;
                rowLayout.childAlignment = TextAnchor.MiddleLeft;
                rowLayout.childControlWidth = false;
                rowLayout.childForceExpandWidth = false;
var container = containerGO.transform;

                // Label
                var labelGO = new GameObject(label + "_Label", typeof(RectTransform), typeof(CanvasRenderer));
                labelGO.transform.SetParent(container, false);
                var labelText = labelGO.AddComponent<TextMeshProUGUI>();
                labelText.text = label;
                labelText.fontSize = fontSize;
                var labelLayout = labelGO.GetComponent<RectTransform>();
                //labelLayout.sizeDelta = new Vector2(labelMinWidth, 30);

                // Input
                var inputGO = new GameObject(label + "_Input", typeof(RectTransform), typeof(CanvasRenderer));
                inputGO.transform.SetParent(container, false);
                var inputField = inputGO.AddComponent<TMP_InputField>();
                var text = inputGO.AddComponent<TextMeshProUGUI>();
                text.fontSize = fontSize;
                inputField.textComponent = text;
                inputField.readOnly = true;

                var inputLayout = inputGO.GetComponent<RectTransform>();
                //inputLayout.sizeDelta = new Vector2(inputMinWidth, 30);

                // Add binding
                var capturedOuter = field;
                var capturedInner = innerField;

                bindings.Add((() =>
                {
                    object outerValue = capturedOuter.GetValue(obj);
                    object innerValue = capturedInner.GetValue(outerValue);
                    return innerValue?.ToString() ?? "null";
                }, inputField));
            }
        }
    }
}
