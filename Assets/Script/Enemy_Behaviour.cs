using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Behaviour : MonoBehaviour
{
    public Transform originalPos;
    public Transform walkPoint;
    Animator animator;
    public Collider weaponHitbox;
    Enemy_HurtBox hurtBox;
    int attackNum;

    public bool turn;
    public bool isAttacking;
    public bool doneAttacking;

    public bool isBeingCountered;
    void Start()
    {
        animator = GetComponent<Animator>();
        hurtBox = GetComponent<Enemy_HurtBox>();
    }



    void Update()
    {
        if (doneAttacking)
        {
            transform.position = originalPos.position;

            weaponHitbox.enabled = false;
            isAttacking = false;
            animator.SetBool("IsAttacking", false);

            isBeingCountered = false;
            doneAttacking = false;
            turn = false;

            return;
        }

        if (!turn || isBeingCountered)
        {
            
            return;
        }


        if (transform.position != walkPoint.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, walkPoint.position, 5f * Time.deltaTime);
            animator.SetBool("Walking", transform.position != walkPoint.position);
            return;
        }


       
        if (!isAttacking)
        {
            StartAttack(Random.Range(0,4));
        }

    }

    void StartAttack(int attackNum)
    {
        weaponHitbox.enabled = false;

        Debug.Log(attackNum);
        isAttacking = true;
        animator.SetInteger("AttackIndex", attackNum);
        animator.SetBool("IsAttacking", isAttacking);
    }

    public void EndAttack()
    { 
        if (isBeingCountered)
            return;

        isAttacking = false;
        animator.SetBool("IsAttacking", isAttacking);
        doneAttacking = true;
    }

    public void EnableHitbox()
    {
        weaponHitbox.enabled = true;
        hurtBox.canDamage = true;
    }

    public void DisableHitbox()
    {
        weaponHitbox.enabled = false;
        hurtBox.canDamage = false;
    }

    public void GetCountered()
    {
        weaponHitbox.enabled = false;
        isBeingCountered = true;

        isAttacking = false;
        animator.SetBool("IsAttacking", false);

    }

    public void EndCounter()
    {
        isBeingCountered = false;
        doneAttacking = true;
    }
}
