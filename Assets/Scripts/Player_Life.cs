using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Life : MonoBehaviour
{
    [SerializeField] private int hp = 50;
    [SerializeField] private float damageCooldown = 1f;

    private float nextDamageTime = 0f;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            if (Time.time >= nextDamageTime)
            {
                hp = hp - 5;

                Debug.Log("Vida restante: " + hp);

                ScoreManager scoreManager = FindFirstObjectByType<ScoreManager>();
                scoreManager.ResetStreak();

                nextDamageTime = Time.time + damageCooldown;

                if (hp <= 0)
                {
                    SceneManager.LoadScene("Game");
                }
            }
        }
    }
}