using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameResult : MonoBehaviour
{
    [SerializeField] private GameObject panelResultado;
    [SerializeField] private TMP_Text resultadoTitulo;
    [SerializeField] private TMP_Text puntajeFinalText;
    [SerializeField] private TMP_InputField nombreInput;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private string saveURL = "http://localhost/examen2/save.php";
    [SerializeField] private GameObject panelRanking;
    [SerializeField] private TMP_Text rankingText;
    [SerializeField] private string rankingURL = "http://localhost/examen2/ranking.php";
    [System.Serializable] public class RankingJugador
    {
        public string name;
        public int score;
    }
    [System.Serializable] public class RankingRespuesta
    {
        public RankingJugador[] data;
    }
    private bool guardando = false;
    private bool guardado = false;

    public bool gano;
    public int nivelFinal;

    private bool terminado = false;

    void Start()
    {
        Time.timeScale = 1f;
        panelResultado.SetActive(false);
        panelRanking.SetActive(false);
        nombreInput.characterLimit = 3;
    }

    public void MostrarResultado(bool victoria)
    {
        if (terminado)
        {
            return;
        }

        terminado = true;
        gano = victoria;

        Clear_Zone zona = FindFirstObjectByType<Clear_Zone>();
        nivelFinal = zona.GetLevel();

        panelResultado.SetActive(true);

        if (gano)
        {
            resultadoTitulo.text = "GANASTE";
        }
        else
        {
            resultadoTitulo.text = "PERDISTE";
        }

        puntajeFinalText.text = "Puntuacion: " + scoreManager.score;

        Time.timeScale = 0f;
    }

    public void GuardarResultado()
    {
        if (guardando || guardado)
        {
            return;
        }

        string nombre = nombreInput.text.Trim().ToUpper();

        if (nombre.Length < 1 || nombre.Length > 3)
        {
            return;
        }

        StartCoroutine(EnviarResultado(nombre));
    }

    private IEnumerator EnviarResultado(string nombre)
    {
        guardando = true;

        WWWForm form = new WWWForm();

        form.AddField("name", nombre);
        form.AddField("score", scoreManager.score);
        form.AddField("final", gano ? 1 : 0);
        form.AddField("levels", "level " + nivelFinal);

        using (UnityWebRequest request = UnityWebRequest.Post(saveURL, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success &&
                request.downloadHandler.text.Contains("\"ok\":true"))
            {
                guardado = true;
                StartCoroutine(CargarRanking());
            }
            else
            {
                Debug.LogError("Error al guardar: " + request.downloadHandler.text + " " + request.error);
            }
        }

        guardando = false;
    }

    private IEnumerator CargarRanking()
    {
        panelResultado.SetActive(false);
        panelRanking.SetActive(true);

        rankingText.text = "Cargando ranking...";

        using (UnityWebRequest request = UnityWebRequest.Get(rankingURL))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                RankingRespuesta respuesta =
                    JsonUtility.FromJson<RankingRespuesta>(
                        request.downloadHandler.text
                    );

                rankingText.text = "";

                if (respuesta != null && respuesta.data != null)
                {
                    for (int i = 0; i < respuesta.data.Length; i++)
                    {
                        rankingText.text += (i + 1) + ".   " +
                            respuesta.data[i].name + "   -   " +
                            respuesta.data[i].score + " puntos\n";
                    }
                }
            }
            else
            {
                rankingText.text = "Error al cargar ranking";
                Debug.LogError(request.error);
            }
        }
    }

    public void ReiniciarJuego()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}