using UnityEngine;
using UnityEngine.InputSystem;

public class BallControl : MonoBehaviour
{
    [SerializeField] private Transform pointer;
    [SerializeField] private float rotationSpeed, moveSpeed, pointerGap, speedThreshold, stopSmoothness;
    private Rigidbody2D rb;
    [SerializeField] private int _maxBounces;
    private int _currentBounces;
    private bool isMoving = false;

    int CurrentBounces
    {
        get { return _currentBounces; }
        set
        {
            _currentBounces = value;
            if (_currentBounces <= 0)
            {
                SmoothStop(); // Panggil fungsi berhenti dengan mulus
                _currentBounces = _maxBounces;
            }
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        CurrentBounces = _maxBounces;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
            pointer.RotateAround(this.transform.position, Vector3.back, rotationSpeed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.Q))
            pointer.RotateAround(this.transform.position, Vector3.back, -rotationSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space))
        {
            isMoving = true;
            Vector3 direction = (pointer.transform.position - transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed;

            float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot);
        }

        if (isMoving && rb.linearVelocity.magnitude < speedThreshold)
        {
            SmoothStop();
        }
    }

    private void SmoothStop()
    {
        StartCoroutine(SlowDown()); // Jalankan efek slow down
    }

    private System.Collections.IEnumerator SlowDown()
    {
        float startSpeed = rb.linearVelocity.magnitude;
        float elapsedTime = 0f;

        while (rb.linearVelocity.magnitude > 0.01f) // Berhenti jika hampir nol
        {
            elapsedTime += Time.deltaTime;
            float factor = 1 - (elapsedTime / stopSmoothness); // Kurangi kecepatan secara bertahap
            rb.linearVelocity = rb.linearVelocity.normalized * (startSpeed * factor);
            yield return null;
        }

        rb.linearVelocity = Vector2.zero; // Pastikan benar-benar berhenti
        rb.angularVelocity = 0f;
        isMoving = false;

        pointer.transform.position = transform.position + Vector3.up * pointerGap;
        pointer.rotation = Quaternion.identity;
    }
}
