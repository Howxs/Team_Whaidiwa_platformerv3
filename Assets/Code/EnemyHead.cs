using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    public Enemy enemy;          // อ้างอิง Enemy หลัก
    public float bounceForce = 8f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // เด้ง Player ขึ้น
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, bounceForce);
            }

            // สั่ง Enemy ตาย
            enemy.Die();
        }
    }
}