using System;
using UnityEngine;

public class playground : MonoBehaviour
{
    public int xSize, ySize;
    public float areaBorderSize;
    public Color32 baseColor;

    Texture2D myTexture;
    Sprite sprite;
    SpriteRenderer spriteRenderer;
    public Vector2 pivot;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();        
    }

    private void Start()
    {
        myTexture = new Texture2D(xSize, ySize);

        FillColor();
    }

    Color32[] InvertColor()
    {
        Color32[] newColor = new Color32[xSize * ySize];

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                byte red = (byte)(255 * x / (xSize - 1)); // Dari 0 ke 255 berdasarkan posisi x
                newColor[y * xSize + x] = new Color32(red, 55, 75, 255);
            }
        }

        return newColor;
    }

    void FillColor()
    {
        myTexture.SetPixels32(InvertColor());
        myTexture.Apply();

        sprite = Sprite.Create(myTexture, new Rect(0f, 0f, myTexture.width, myTexture.height), pivot, 1f);
        spriteRenderer.sprite = sprite;
    }
}
