using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 1f;
    private Renderer backgroundRenderer;

    private void Awake()
    {
        backgroundRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offset = new Vector2(x, 0);
        backgroundRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
