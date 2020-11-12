using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool canAttack = true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("I hit a: " + other.name);
        IDamageable hit = other.GetComponent<IDamageable>();

        if (hit != null)
        {
            if (canAttack)
            {
                hit.Damage();
                StartCoroutine(AttackCD());
            }
        }
    }

    private IEnumerator AttackCD()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.5f);
        canAttack = true;
    }
}
