using UnityEngine;

public class Enemy_HurtBox : MonoBehaviour
{
    public float damage = 25f;
    Enemy_Behaviour enemy;

    void Start()
    {
        enemy = GetComponentInParent<Enemy_Behaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(enemy.isBeingCountered)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is Damaged");
            PlayerHealth playerHp = other.GetComponent<PlayerHealth>();
            if (playerHp != null)
            {
                playerHp.DoDamage(damage);
            }
        }
    }

}
