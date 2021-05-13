using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour
{


    //using tutorial from here https://www.youtube.com/watch?v=9RSmVJGOmxg

    // Start is called before the first frame update

    public Material scrollableMaterial;
    public Vector2 direction = new Vector2(1, 0);
    public float speed = 1.0f;

    private Vector2 currentOffset;

    void Start()
    {
        currentOffset = scrollableMaterial.GetTextureOffset("_MainTex");    
    }

    // Update is called once per frame
    void Update()
    {
        currentOffset += direction * speed * Time.deltaTime;
        scrollableMaterial.SetTextureOffset("_MainTex", currentOffset);
        
    }
}
