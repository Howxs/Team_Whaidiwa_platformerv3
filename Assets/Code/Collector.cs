using UnityEngine;
using TMPro;

public class Collector : MonoBehaviour
{
    public TextMeshProUGUI progressText;
    [Header("Progress Setting")]
    public int totalItemsRequired = 10;   // จำนวนของที่ต้องเก็บให้ครบ
    private int currentCollected = 0;

    [Header("Target Object")]
    public GameObject targetObject;       // Obj ที่จะ SetActive(true)

    private void Start()
    {
        if (targetObject != null)
            targetObject.SetActive(false); // เริ่มต้นปิดไว้ก่อน
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IItem item = collision.GetComponent<IItem>();
        if (item != null)
        {
            item.Collect();

            // ถ้าไม่ใช่ CatStar ค่อยนับคะแนน
            if (!collision.CompareTag("CatStar"))
            {
                currentCollected++;
                CheckProgress();
            }
        }
    }

    void CheckProgress()
    {
        float percent = (float)currentCollected / totalItemsRequired * 100f;

        if (progressText != null)
            progressText.text = percent.ToString("0") + "%";

        if (percent >= 100f)
        {
            if (targetObject != null)
                targetObject.SetActive(true);
        }
    }
}
