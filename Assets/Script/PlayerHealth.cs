using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBar;
    Animator animator;

    public AudioSource audioSource;
    public AudioClip clip;
    void Start()
    {
        currentHealth = maxHealth;
        animator=GetComponent<Animator>();
    }

    void Update()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void DoDamage(float damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        audioSource.PlayOneShot(clip);
    }
}
