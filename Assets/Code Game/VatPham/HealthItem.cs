using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public int healAmount = 20; // Số lượng máu được hồi phục khi ăn

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Kiểm tra nếu đối tượng va chạm là Player
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                // Gọi hàm hồi máu trên Player
                playerHealth.Heal(healAmount);

                // Hủy đối tượng vật phẩm hồi máu
                Destroy(gameObject);
            }
        }
    }
}
