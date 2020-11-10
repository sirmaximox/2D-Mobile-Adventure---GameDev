using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    private float _horizontalInput = 0;
    private bool _jump;
    [SerializeField]
    private float _jumpHeight = 10;
    [SerializeField]
    private float _speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _jump = Input.GetKeyDown(KeyCode.Space);
    }

    void FixedUpdate()
    {
        bool grounded = IsGrounded();
        Debug.Log(grounded);

        _rb.velocity = new Vector2(_horizontalInput, _rb.velocity.y);
        if (_jump && grounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpHeight);
        }
    }

    private bool IsGrounded()
    {
        /*RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, transform.GetComponent<BoxCollider2D>().bounds.extents.y);
        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
        */
        
        return Physics2D.Raycast(transform.position, Vector2.down, transform.GetComponent<BoxCollider2D>().bounds.extents.y + 0.1f, 1 << 8);
    }
}
