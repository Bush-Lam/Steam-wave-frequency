using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasUI : MonoBehaviour {
    public Image Background;
    public float scrollSpeed = 0.5F;
    public Renderer rend;
    Vector2 offset = new Vector2(0, 0);
    float start = 0;
    void Start()
    {
        rend = GetComponent<Renderer>();
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        Debug.Log(Screen.width);
    }
    void Update()
    {
        offset.x = start + Time.time * scrollSpeed;
        rend.material.mainTextureOffset = offset;
    }
}
