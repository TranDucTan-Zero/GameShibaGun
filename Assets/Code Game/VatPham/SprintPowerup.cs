using UnityEngine;

public class SprintPowerup : MonoBehaviour
{
    public float sprintBoost = 2.0f;
    public float sprintDuration = 5.0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.ApplySprintBoost(sprintBoost, sprintDuration);
                // Hủy đối tượng vật phẩm tốc hành tại đây hoặc vô hiệu hóa nó.
                Destroy(gameObject);
            }
        }
    }
}
