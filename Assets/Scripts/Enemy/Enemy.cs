using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int gem;
    [SerializeField] protected float speed;
    [SerializeField] protected float viewDistance = 2;
    [SerializeField] protected Transform pointA;
    [SerializeField] protected Transform pointB;
    protected bool isHit;
    protected GameObject player;
    protected Vector3 facing;

    protected Vector3 moveTo;
    protected Animator anim;

    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            return;
        Movement();
    }

    public virtual void Movement()
    {
        if (moveTo == pointA.position)
        {
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }
        else if (moveTo == pointB.position)
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }

        if (transform.position == pointA.position)
        {

            anim.SetTrigger("Idle");
            moveTo = pointB.position;
        }
        else if (transform.position == pointB.position)
        {
            anim.SetTrigger("Idle");
            moveTo = pointA.position;
        }

        if (isHit == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveTo, speed * Time.deltaTime);
        }

        //chequear la distancia entre el jugador y el enemigo
        //si la distancia es mayor a 2 unidades
        //ishit = false apagar modo combate
        //incombat = false apagar mod combate

        if (transform.rotation.eulerAngles.y == -180)
        {
            facing = Vector3.left;
        }
        else if (transform.rotation.eulerAngles.y == 0)
        {
            facing = Vector3.right;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, facing, 2f, 1 << 10);
        Debug.DrawRay(transform.position, facing, Color.green, 2f);

        
        if ( hit.collider != null || hit.transform.tag != "Player")
        {
            isHit = false;
            anim.SetBool("InCombat", false);
        }

    }

    
}
