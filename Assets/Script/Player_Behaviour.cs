using UnityEngine;

public class Player_Behaviour : MonoBehaviour
{
    public Collider hitBox;
    public Collider parryHitbox;
    Animator animator;
    public bool doneCountering;

    public float counterDamage = 20f;

    Enemy_Behaviour enemy;
    EnemyHealth enemyHealth;

    void Start()
    {
        hitBox = GetComponent<Collider>();
        animator= GetComponent<Animator>();
    }

    public void SetCounterTarget(Enemy_Behaviour enemyB, EnemyHealth enemyH)
    {
        enemy = enemyB;
        enemyHealth = enemyH;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemy = null;
            enemyHealth = null;
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Parry");
        }
    }

    public void EnableParryHitBox()
    {
        hitBox.enabled = false;
        parryHitbox.enabled = true;
    }

    public void DisableParryHitBox()
    {
        hitBox.enabled = true;
        parryHitbox.enabled = false;
    }

    public void StartCounter()
    {
        parryHitbox.enabled = false;
        animator.SetTrigger("Countered");
    }

    public void StartCounterAttack()
    {
        animator.SetTrigger("Counter Attack");
    }

    public void EndCounterAttack()
    {
        enemyHealth.DoDamage(counterDamage);
        enemy.EndCounter();
        enemyHealth = null;
        enemy = null;
    }
}
