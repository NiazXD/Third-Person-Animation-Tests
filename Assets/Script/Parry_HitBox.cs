using UnityEngine;

public class Parry_HitBox : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy_Behaviour enemy = other.GetComponentInParent<Enemy_Behaviour>();
            EnemyHealth health = other.GetComponentInParent<EnemyHealth>();

            Player_Behaviour player = GetComponentInParent<Player_Behaviour>();

            if (enemy != null && health != null && player != null)
            {
                if (!enemy.isAttacking)
                    return;
                audioSource.PlayOneShot(clip);
                enemy.GetCountered(); 
                player.SetCounterTarget(enemy, health);
                player.StartCounter();
            }
        }
    }
}
