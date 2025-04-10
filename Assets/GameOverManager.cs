using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class GameOverManager : MonoBehaviour
{
    public TMP_Text scoreText;  // Atribua no Inspector o Text que exibirá o score
    public TMP_Text timeText;   // Atribua no Inspector o Text que exibirá o tempo

    void Start()
    {
        scoreText.text = "Score: " + GameOverData.score;
        timeText.text = "Tempo: " + GameOverData.timeAtDeath.ToString("F2") + " s";
    }

    // Método vinculado ao botão de jogar novamente (Retry)
    public void Retry()
    {
        SceneManager.LoadScene("GameScene"); // Nome da sua cena de jogo
    }

    // Método vinculado ao botão de voltar ao menu
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");  // Nome da sua cena de menu principal
    }
}