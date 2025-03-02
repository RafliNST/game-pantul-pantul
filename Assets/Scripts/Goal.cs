using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour
{
    [SerializeField] float validatePositionTime;
    [SerializeField] Transform _player;
    [SerializeField] playground _playground;
    
    Collider2D _collider;
    Vector2Int playGroundBounds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        playGroundBounds = _playground.GetSpriteArea();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(ValidatePosition(validatePositionTime));

    }

    public void ChangePosition()
    {
        Vector3 newPos = transform.position;
        newPos.x = Random.Range(-playGroundBounds.x / 2, playGroundBounds.x / 2);
        newPos.y = Random.Range(-playGroundBounds.y / 2, playGroundBounds.y / 2);

        transform.position = newPos;
    }
    IEnumerator ValidatePosition(float time)
    {
        yield return new WaitForSeconds(time);

        if (_collider.bounds.Contains(_player.position))
        {
            //_animator.Play("goal-shrink");
            ChangePosition();
        }
    }    
}
