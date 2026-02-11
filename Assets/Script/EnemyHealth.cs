using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;

    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBar;
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void DoDamage(float damage)
    {
        audioSource.PlayOneShot(clip);
        currentHealth -= damage;
    }
}
