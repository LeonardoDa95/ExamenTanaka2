using TMPro;
using UnityEngine;

public class Clear_Zone : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform player;

    [SerializeField] private Transform[] spawnsA;
    [SerializeField] private Transform[] spawnsB;

    [SerializeField] private TMP_Text nivelAvisoText;

    private int level = 1;
    private int turns = 0;
    private int enemiesAlive = 0;

    private bool lookingAtA = true;

    void Start()
    {
        MostrarNivel();
    }

    void SpawnWave()
    {
        Transform[] spawns;

        if (lookingAtA)
        {
            spawns = spawnsA;
        }
        else
        {
            spawns = spawnsB;
        }

        int cantidad = 4;

        if (level == 2)
        {
            cantidad = 6;
        }
        else if (level == 3)
        {
            cantidad = 8;
        }

        enemiesAlive = cantidad;

        for (int i = 0; i < cantidad; i++)
        {
            int spawnIndex = i % spawns.Length;

            Vector3 posicion = spawns[spawnIndex].position;

            if (i >= spawns.Length)
            {
                posicion += spawns[spawnIndex].right * 2f;
            }

            Instantiate(enemyPrefab, posicion,
                spawns[spawnIndex].rotation);
        }

    }

    public void EnemyDefeated()
    {
        enemiesAlive--;

        if (enemiesAlive == 0)
        {
            Invoke(nameof(NextWave), 1.5f);
        }
    }

    void NextWave()
    {
        int maxTurns = level + 1;

        if (turns < maxTurns)
        {
            TurnPlayer();

            turns++;

            SpawnWave();
        }
        else
        {
            level++;

            if (level > 3)
            {
                level = 3;

                GameResult resultado = FindFirstObjectByType<GameResult>();

                resultado.MostrarResultado(true);
                return;
            }

            turns = 0;

            if (!lookingAtA)
            {
                TurnPlayer();
            }

            ScoreManager scoreManager = FindFirstObjectByType<ScoreManager>();

            scoreManager.ResetStreak();

            MostrarNivel();
        }
    }

    void TurnPlayer()
    {
        player.Rotate(0, 180, 0);

        lookingAtA = !lookingAtA;

    }

    public int GetLevel()
    {
        return level;
    }

    public int GetEnemiesAlive()
    {
        return enemiesAlive;
    }

    void MostrarNivel()
    {
        nivelAvisoText.text = "NIVEL " + level;

        nivelAvisoText.gameObject.SetActive(true);

        Invoke(nameof(EmpezarNivel), 2f);
    }

    void EmpezarNivel()
    {
        nivelAvisoText.gameObject.SetActive(false);

        SpawnWave();
    }
}