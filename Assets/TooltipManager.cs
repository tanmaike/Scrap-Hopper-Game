using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipManager : MonoBehaviour {

    public static TooltipManager _instance;

    public TextMeshProUGUI textComponent;

    private void Awake() {
        if (_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this; 
    }

    void Start() {
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    void Update() {
        transform.position = Input.mousePosition;
    }

    public void setAndShowTooltip(string message) {
        gameObject.SetActive(true);
        textComponent.text = message;
    }

    public void hideTooltip() {
        gameObject.SetActive(false);
        textComponent.text = string.Empty;
    }
}
