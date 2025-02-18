using UnityEngine;
using UnityEngine.InputSystem;

public class ballControl : MonoBehaviour
{
    [SerializeField] private Transform pointer;
    [SerializeField] private float rotationSpeed, moveSpeed, pointerGap, speedTreshold;
    private Rigidbody2D rb;
    [SerializeField] private int _maxBounces;
    private int _currentBounces;

    bool IsMoving = false;
    int CurrentBounces
    {
        get { return _currentBounces; }
        set
        {
            _currentBounces = value;

             // Jika bounce habis, reset posisi dan kembalikan nilai awal
            if (_currentBounces <= 0)
            {
                rb.linearVelocity = Vector2.zero; // Berhentikan object
                pointer.transform.position = transform.position + Vector3.up * pointerGap;
                pointer.rotation = Quaternion.identity;

                // Reset ke nilai awal
                _currentBounces = _maxBounces;
                rb.angularVelocity = 0f;
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
        {
            pointer.RotateAround(this.transform.position, Vector3.back, rotationSpeed * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.Q))
        {
            pointer.RotateAround(this.transform.position, Vector3.back, -rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            IsMoving = true;
            Vector3 direction = pointer.transform.position - transform.position;
            Vector3 rotation = transform.position - pointer.transform.position;
            rb.linearVelocity = new Vector2 (direction.x, direction.y).normalized * moveSpeed;

            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot);
        }

        if (IsMoving && rb.linearVelocity.magnitude < speedTreshold)
        {
            rb.linearVelocity = Vector2.zero; // Berhentikan object
            pointer.transform.position = transform.position + Vector3.up * pointerGap;
            pointer.rotation = Quaternion.identity;

            // Reset ke nilai awal
            _currentBounces = _maxBounces;
            rb.angularVelocity = 0f;
            IsMoving = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //CurrentBounces--;
    }
}
