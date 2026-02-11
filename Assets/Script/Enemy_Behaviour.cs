using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Behaviour : MonoBehaviour
{
    public Transform originalPos;
    public Transform walkPoint;
    Animator animator;
    public Collider weaponHitbox;

    public bool turn;
    public bool isAttacking;
    public bool doneAttacking;

    public bool isBeingCountered;
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (doneAttacking)
        {
            transform.position = originalPos.position;

            isBeingCountered = false;
            doneAttacking = false;
            turn = false;

            return;
        }

        if (!turn || isBeingCountered)
        {
            Debug.Log("Enemy state inside enemy script: " + isBeingCountered);
            return;
        }


        if (transform.position != walkPoint.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, walkPoint.position, 3f * Time.deltaTime);
            animator.SetBool("Walking", transform.position != walkPoint.position);
            return;
        }


       
        if (!isAttacking)
        {
            StartAttack();
        }

    }

    void StartAttack()
    {
        isAttacking = true;
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
    }

    public void DisableHitbox()
    {
        weaponHitbox.enabled = false;
    }

    public void GetCountered()
    {
        isBeingCountered = true;

        isAttacking = false;
        animator.SetBool("IsAttacking", false);

        weaponHitbox.enabled = false;
    }

    public void EndCounter()
    {
        isBeingCountered = false;
        weaponHitbox.enabled = true;
        doneAttacking = true;
    }
}
