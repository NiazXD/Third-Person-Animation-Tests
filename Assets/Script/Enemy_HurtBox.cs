using UnityEngine;

public class Enemy_HurtBox : MonoBehaviour
{
    public bool canDamage = false;
    public float damage = 25f;
    private void OnTriggerEnter(Collider other)
    {
        if (!canDamage)
            return;

        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHp = other.GetComponent<PlayerHealth>();
            if (playerHp != null)
            {
                playerHp.DoDamage(damage);
                canDamage = false; 
            }
        }
    }
}
