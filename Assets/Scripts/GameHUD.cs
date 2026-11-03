using TMPro;
using UnityEngine;

public class GameHUD : MonoBehaviour
{
    [SerializeField] private TMP_Text vidaText;
    [SerializeField] private TMP_Text puntosText;
    [SerializeField] private TMP_Text nivelText;
    [SerializeField] private TMP_Text rachaText;
    [SerializeField] private TMP_Text enemigosText;

    [SerializeField] private Player_Life playerLife;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private Clear_Zone clearZone;

    void Update()
    {
        vidaText.text = "Vida: " + playerLife.GetHealth();

        puntosText.text = "Puntos: " + scoreManager.score;

        nivelText.text = "Nivel: " + clearZone.GetLevel() + " / 3";

        rachaText.text = "Racha: " + scoreManager.GetStreak();

        enemigosText.text = "Enemigos: " + clearZone.GetEnemiesAlive();
    }
}