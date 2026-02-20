using System.Collections;
using UnityEngine;

class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public HealthUI healthUI;
    
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthUI.SetMaxHearts(maxHealth);

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            TakeDamage(enemy.damage);
        }
        Trap trap = collision.gameObject.GetComponent<Trap>();
        if (trap && trap.damage > 0)
        {
            TakeDamage(trap.damage);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthUI.UpdateHearts(currentHealth);

        //Flash Red
        StartCoroutine(FlashRed());

        if (currentHealth <= 0)
        {
            //player dead! -- call game over, animation, etc
        }
    }
    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
}
