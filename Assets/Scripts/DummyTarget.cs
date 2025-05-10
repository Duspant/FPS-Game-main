using UnityEngine;

public class DummyTarget : MonoBehaviour
{
    public float health = 50f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
            Destroy(gameObject);
    }
}
