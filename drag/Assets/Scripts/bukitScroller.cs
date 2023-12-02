using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bukitScroller : MonoBehaviour
{
    [Range(-1f, 1f)] // Corrected the Range attribute syntax
    public float scrollSpeed = 0.5f;
    private float offset;
    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material; // Corrected GetComponent syntax
    }

    // Update is called once per frame
    void Update()
    {
        offset += (Time.deltaTime * scrollSpeed) / 10f;
        mat.SetTextureOffset("_MainTex", new Vector2(offset, 0)); // Corrected SetTextureOffset syntax
    }
}
