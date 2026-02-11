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

    public float parryCooldown = 1.0f;
    private float parryTimer = 0f;
    private bool canParry = true;

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
        if (!canParry)
        {
            parryTimer -= Time.deltaTime;

            if (parryTimer <= 0f)
                canParry = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && canParry)
        {
            animator.SetTrigger("Parry");

            canParry = false;
            parryTimer = parryCooldown;
        }
    }

    public void EnableParryHitBox()
    {
        hitBox.enabled = false;
        parryHitbox.enabled = true;
    }

    public void DisableParryHitBox()
    {
        parryHitbox.enabled = false;
    }

    public void EnablePlayerHitbox()
    {
        hitBox.enabled = true;
    }

    public void StartCounter()
    {
        parryHitbox.enabled = false;
        hitBox.enabled = true;
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
