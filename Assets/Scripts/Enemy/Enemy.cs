using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int gem;
    [SerializeField] protected float speed;
    [SerializeField] protected Transform pointA;
    [SerializeField] protected Transform pointB;

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

        transform.position = Vector3.MoveTowards(transform.position, moveTo, speed * Time.deltaTime);

    }

    
}
