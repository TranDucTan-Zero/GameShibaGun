using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
   
    public TextMeshProUGUI scoreText;
    public GameObject gameOverCanvas; // Kết nối Canvas của Game Over trong Inspector
   
    // Hàm này sẽ được gọi khi máu của nhân vật giảm xuống 0
    public void ShowGameOver(int killedCount)
    {
        // Gán điểm của người chơi vào biến playerScore
        
        // Hiển thị Canvas Game Over
        gameOverCanvas.SetActive(true);
       

        // Cập nhật số điểm hạ gục trên bảng Game Over
        scoreText.text = "" + killedCount;
        Time.timeScale = 0;

    }

    public void Home()
    {
        SceneManager.LoadScene("Menu"); // Chuyển đến màn hình Menu
        Time.timeScale = 1; // Thời gian trôi bình thường

        // Có thể thêm mã khác tại đây nếu cần
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Khởi động lại cùng màn chơi
        Time.timeScale = 1; // Thời gian trôi bình thường

        // Có thể thêm mã khác tại đây nếu cần
    }
}
