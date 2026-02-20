using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public Sprite deadSprite;
    public float destroyDelay = 1f;

    private SpriteRenderer sr;
    private Collider2D col;
    private bool isDead = false;
    public Transform player;
    public float chaseSpeed = 2f;
    public float jumpForce = 2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool shouldJump;
    private bool isFacingRight = true;

    public int damage = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (player == null) return;

        // ตรวจพื้น
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);

        // หาทิศทางไปหาผู้เล่น
        float direction = Mathf.Sign(player.position.x - transform.position.x);

        // Flip ตามทิศทาง
        Flip(direction);

        if (isGrounded)
        {
            // วิ่งไล่ผู้เล่น
            rb.linearVelocity = new Vector2(direction * chaseSpeed, rb.linearVelocity.y);

            // ตรวจพื้นด้านหน้า
            RaycastHit2D groundInFront = Physics2D.Raycast(
                transform.position,
                new Vector2(direction, 0),
                2f,
                groundLayer
            );

            // ตรวจช่องว่างข้างหน้า
            RaycastHit2D gapAhead = Physics2D.Raycast(
                transform.position + new Vector3(direction, 0, 0),
                Vector2.down,
                2f,
                groundLayer
            );

            // ตรวจแพลตฟอร์มด้านบน
            RaycastHit2D platformAbove = Physics2D.Raycast(
                transform.position,
                Vector2.up,
                3f,
                groundLayer
            );

            // ตรวจผู้เล่นอยู่ด้านบน
            bool isPlayerAbove = Physics2D.Raycast(
                transform.position,
                Vector2.up,
                3f,
                1 << player.gameObject.layer
            );

            if (!groundInFront.collider && !gapAhead.collider)
            {
                shouldJump = true;
            }
            else if (isPlayerAbove && platformAbove.collider)
            {
                shouldJump = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (isGrounded && shouldJump)
        {
            shouldJump = false;

            float direction = Mathf.Sign(player.position.x - transform.position.x);

            rb.AddForce(
                new Vector2(direction * jumpForce, jumpForce),
                ForceMode2D.Impulse
            );
        }
    }

    private void Flip(float direction)
    {
        if (direction == 0) return;

        if (isFacingRight && direction < 0 || !isFacingRight && direction > 0)
        {
            isFacingRight = !isFacingRight;

            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }
    public void Die()
    {
        {
            if (isDead) return;
            isDead = true;

            // หยุดการเคลื่อนที่
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;

            // ปิด collider เพื่อไม่ให้ชนอีก
            if (col != null)
                col.enabled = false;

            // เปลี่ยน sprite
            if (deadSprite != null)
                sr.sprite = deadSprite;

            // ทำลายหลังหน่วงเวลา
            Destroy(gameObject, destroyDelay);
        }
    }
}