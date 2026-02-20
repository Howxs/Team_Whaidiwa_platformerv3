using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public HealthUI healthUI;
    
    private SpriteRenderer spriteRenderer;

    public static event Action OnplayedDied;

    public GameObject GameOver;

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
            GameOverScreen();
        }
    }
    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
    void GameOverScreen()
    {
        GameOver.SetActive(true);
        Time.timeScale = 0f;
    }
    public void RestartGame()
    {
        // *สำคัญที่สุด* ต้องคืนค่าเวลาเป็น 1 ก่อนโหลดฉากใหม่
        Time.timeScale = 1f;

        // โหลด Scene ปัจจุบันใหม่ (รีเซ็ตเกม)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ฟังก์ชันสำหรับปุ่ม Menu (กลับหน้า Title)
    public void BackToTitle()
    {
        Time.timeScale = 1f; // *สำคัญ* คืนค่าเวลา
        SceneManager.LoadScene("TitleScreen"); // ใส่ชื่อ Scene หน้า Title ของคุณ
    }
}
