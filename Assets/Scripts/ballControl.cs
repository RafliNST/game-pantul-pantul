using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class BallControl : MonoBehaviour
{
    [SerializeField] private Transform pointer;
    [SerializeField] private float rotationSpeed, maxSpeed, pointerGap, speedThreshold, stopSmoothness;
    [SerializeField]
    [Range(1f, 15f)]
    float speedForceRatio;
    float speedForce;

    private Rigidbody2D rb;
    private int _currentBounces;

    private bool isMoving = false;
    bool _IsMoving
    {
        get { return isMoving; }
        set
        {
            _IsMoving = value;
            ballBounced.Invoke(CurrentBounces);
            CurrentBounces = 0;
        }
    }


    public UnityEvent<float> speedForceChange;
    public UnityEvent ballReleased;
    public UnityEvent<int> ballBounced;

    private int CurrentBounces
    {
        get { return _currentBounces; }
        set
        {
            _currentBounces = value;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        CurrentBounces = 0;
        _IsMoving = isMoving;
    }

    private void Update()
    {
        HandleRotation();
        HandlePowerCharge();
        HandleMovement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CurrentBounces++;
        ballBounced.Invoke(CurrentBounces);
    }

    private void HandleRotation()
    {
        if (Input.GetKey(KeyCode.E))
            pointer.RotateAround(transform.position, Vector3.back, rotationSpeed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.Q))
            pointer.RotateAround(transform.position, Vector3.back, -rotationSpeed * Time.deltaTime);
    }

    private void HandlePowerCharge()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            speedForce += speedForceRatio * Time.deltaTime;
            speedForceChange.Invoke(speedForce);
        }
    }

    private void HandleMovement()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            LaunchBall();
        }

        if (_IsMoving && rb.linearVelocity.magnitude < speedThreshold)
        {
            SmoothStop();
        }
    }

    private void LaunchBall()
    {
        _IsMoving = true;
        speedForce = Mathf.Clamp(speedForce, 0f, maxSpeed);
        Vector3 direction = (pointer.transform.position - transform.position).normalized;
        rb.linearVelocity = direction * speedForce;

        float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
        
        ballReleased.Invoke();
    }

    private void SmoothStop()
    {
        StartCoroutine(SlowDown());
    }

    private IEnumerator SlowDown()
    {
        float startSpeed = rb.linearVelocity.magnitude;
        float elapsedTime = 0f;

        while (rb.linearVelocity.magnitude > 0.01f)
        {
            elapsedTime += Time.deltaTime;
            float factor = 1 - (elapsedTime / stopSmoothness);
            rb.linearVelocity = rb.linearVelocity.normalized * (startSpeed * factor);
            yield return null;
        }

        ResetBall();
    }

    private void ResetBall()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        _IsMoving = false;
        speedForce = 0f;
        ResetPointer();
    }

    public bool IsMoving()
    {
        Debug.Log(_IsMoving);
        return _IsMoving;
    }

    private void ResetPointer()
    {
        pointer.transform.position = transform.position + Vector3.up * pointerGap;
        pointer.rotation = Quaternion.identity;
    }

    public float GetMaxSpeed()
    {
        return maxSpeed;
    }
}
