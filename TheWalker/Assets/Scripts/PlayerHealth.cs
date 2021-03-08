using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float invincibilityTimeAfterHit = 3f;
    public float invinsibilityFlashDelay = 0.2f;

    public bool isInvicible = false;

    public SpriteRenderer graphics;
    public HealthBar healthBar;


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isInvicible)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            isInvicible = true;
            StartCoroutine(InvicibilityFlash());
            StartCoroutine(HandleInvicibilityDelay());
        }

    }
    public IEnumerator InvicibilityFlash()
    {
        while (isInvicible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds (invinsibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds (invinsibilityFlashDelay);
        }
    }
    public IEnumerator HandleInvicibilityDelay()
    { 
        yield return new WaitForSeconds(invincibilityTimeAfterHit);
        isInvicible = false;
    }
}
