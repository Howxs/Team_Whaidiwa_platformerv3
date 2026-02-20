using UnityEngine;

public class Fish_Bones : MonoBehaviour, IItem
{
    public void Collect()
    {
        Destroy(gameObject);
    }

    
}
