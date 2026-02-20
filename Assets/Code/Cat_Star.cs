using UnityEngine;

public class Cat_Star : MonoBehaviour, IItem
{
    public void Collect()
    {
        Destroy(gameObject);
    }


}
