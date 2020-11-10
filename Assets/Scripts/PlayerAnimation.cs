using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator _animatorPlayer;
    private Animator _animatorSword;

    // Start is called before the first frame update
    void Start()
    {
        _animatorPlayer = GetComponentsInChildren<Animator>()[0];
        _animatorSword = GetComponentsInChildren<Animator>()[1];

        if (_animatorPlayer == null)
            Debug.Log("Player Animator not found");

        if (_animatorSword == null)
            Debug.Log("Sword Animator not found");
    }

    public void Move(float move)
    {
        _animatorPlayer.SetFloat("Move", Mathf.Abs(move));

        if (move < 0)
        {
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }
        else if (move > 0)
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
    }

    public void Jump(bool jump)
    {
        _animatorPlayer.SetBool("IsJumping", jump);
    }

    public void Attack ()
    {
        _animatorPlayer.SetTrigger("Attack");
        _animatorSword.SetTrigger("SwordAnimation");
    }

}
