using UnityEngine;

public class Fish_Bones : MonoBehaviour, IItem
{
    Renderer rend;
    void Start ()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }
    public void Collect()
    {
        AudioSource audio = GetComponent<AudioSource>();

        // เล่นเสียงที่ตำแหน่งของไอเทม (เสียงจะเล่นจนจบแม้ไอเทมจะถูก Destroy)
        if (audio != null && audio.clip != null)
        {
            AudioSource.PlayClipAtPoint(audio.clip, transform.position);
        }

        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // เช็คว่า Object ที่มาชนมี Tag ว่า "Player" หรือไม่
        if (collision.CompareTag("Player"))
        {
            Collect();
        }
    }

}
