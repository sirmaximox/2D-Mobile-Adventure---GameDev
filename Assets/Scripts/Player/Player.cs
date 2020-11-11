using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField]
    private float _horizontalInput = 0;
    private bool _jump;
    [SerializeField]
    private float _jumpHeight = 5;
    [SerializeField]
    private float _speed = 2.5f;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float _jumpRayOffset = 0.5f;
    private bool resetJump;
    private BoxCollider2D _playerColl;

    private PlayerAnimation _anim;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<PlayerAnimation>();
        _playerColl = GetComponent<BoxCollider2D>();

        if (_rb == null)
            Debug.Log("Player Rigidbody2D not found");
        if (_anim == null)
            Debug.Log("Player Animation not found");
        if (_playerColl == null)
            Debug.Log("Player Collider not found");
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();

        if (Input.GetMouseButtonDown(0) && IsGrounded())
        {
            _anim.Attack();
        }
    }

    /*void FixedUpdate()
    {
        bool grounded = IsGrounded();
        Debug.Log(grounded);

        _rb.velocity = new Vector2(_horizontalInput, _rb.velocity.y);
        if (_jump && grounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpHeight);
        }
    }*/

    private void Movimiento()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _jump = Input.GetKeyDown(KeyCode.Space);

        if (_jump && IsGrounded())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpHeight);

            _anim.Jump(true);

            StartCoroutine(ResetJump());
                      
        }
        else if (IsGrounded())
        {
            _anim.Jump(false);
        }
        

        _rb.velocity = new Vector2(_horizontalInput * _speed, _rb.velocity.y);
        _anim.Move(_horizontalInput);
    }

    private bool IsGrounded()
    {
        Color rayColor;

        RaycastHit2D hitBox = Physics2D.BoxCast(_playerColl.bounds.center, _playerColl.bounds.size, 0f, Vector2.down, _jumpRayOffset, layerMask);
        //RaycastHit2D hitCenter = Physics2D.Raycast(transform.position, Vector2.down, transform.GetComponent<BoxCollider2D>().bounds.extents.y + _jumpRayOffset, layerMask);

        if (hitBox)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(_playerColl.bounds.center + new Vector3(_playerColl.bounds.extents.x, 0), Vector2.down * (_playerColl.bounds.extents.y + _jumpRayOffset), rayColor);
        Debug.DrawRay(_playerColl.bounds.center - new Vector3(_playerColl.bounds.extents.x, 0), Vector2.down * (_playerColl.bounds.extents.y + _jumpRayOffset), rayColor);
        Debug.DrawRay(_playerColl.bounds.center - new Vector3(_playerColl.bounds.extents.x, _playerColl.bounds.extents.y + _jumpRayOffset), Vector2.right * (_playerColl.bounds.size.x), rayColor);

        Debug.DrawRay(transform.position, Vector3.down, rayColor, transform.GetComponent<BoxCollider2D>().bounds.extents.y + _jumpRayOffset);


        if (hitBox.collider != null)
        {
            if (resetJump == false)
            {
                
                return true;
            }
        }
        
        return false;
        
    }

    private IEnumerator ResetJump()
    {
        resetJump = true;
        yield return new WaitForSeconds(0.1f);
        resetJump = false;
    }
}
