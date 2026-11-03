using UnityEngine;

public class Clear_Zone : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform player;

    [SerializeField] private Transform[] spawnsA;
    [SerializeField] private Transform[] spawnsB;

    private int level = 1;
    private int turns = 0;
    private int enemiesAlive = 0;

    private bool lookingAtA = true;

    void Start()
    {
        SpawnWave();
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

        enemiesAlive = spawns.Length;

        for (int i = 0; i < spawns.Length; i++)
        {
            Instantiate(enemyPrefab, spawns[i].position,
                spawns[i].rotation);
        }

        Debug.Log("Nivel: " + level);
        Debug.Log("Enemigos: " + enemiesAlive);
    }

    public void EnemyDefeated()
    {
        enemiesAlive--;

        Debug.Log("Enemigos restantes: " + enemiesAlive);

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
                Debug.Log("GANASTE EL JUEGO");
                return;
            }

            turns = 0;

            if (!lookingAtA)
            {
                TurnPlayer();
            }

            ScoreManager scoreManager =
                FindFirstObjectByType<ScoreManager>();

            scoreManager.ResetStreak();

            Debug.Log("Siguiente nivel: " + level);

            SpawnWave();
        }
    }

    void TurnPlayer()
    {
        player.Rotate(0, 180, 0);

        lookingAtA = !lookingAtA;

        Debug.Log("Jugador giro 180 grados");
    }
}