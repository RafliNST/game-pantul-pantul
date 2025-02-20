using UnityEngine;

public class obstacleGenerator : MonoBehaviour
{
    public Vector2Int minArea;
    public Vector2Int maxArea;

    SpriteRenderer spriteRenderer;
    Texture2D _texture;
    Sprite _sprite;
    Vector2 pivot;
    Vector2Int validDrawArea;

    [SerializeField] GameObject playGround;

    private void Awake()
    {        
        spriteRenderer = GetComponent<SpriteRenderer>();
        pivot = new Vector2(.5f, .5f);
        validDrawArea = playGround.GetComponent<playground>().GetSpriteArea();
    }

    private void Start()
    {
        int W = Random.Range(minArea.x, maxArea.x);
        int H = Random.Range(minArea.y, maxArea.y);
        _texture = new Texture2D(W, H);

        FillTexture();
        gameObject.AddComponent<BoxCollider2D>();
    }

    void FillTexture()
    {
        _sprite = Sprite.Create(_texture, new Rect(0f, 0f, _texture.width, _texture.height), pivot, 1f);
        spriteRenderer.sprite = _sprite;
    }
}
