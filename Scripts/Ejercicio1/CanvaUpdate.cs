using TMPro;
using UnityEngine;

public class CanvaUpdate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    public MoveAstronaut astronaut;
    private Chaser[] chasers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        astronaut.OnRockFound += upgrade;
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        chasers = new Chaser[enemyObjects.Length];
        for (int i = 0; i < chasers.Length; i++)
        {
            chasers[i] = enemyObjects[i].GetComponent<Chaser>();
            chasers[i].OnAstronautFound += gameOver;
        }

        scoreText.text = "Puntuación 0";
    }

    void gameOver()
    {
        scoreText.text = "Game Over :(";
    }

    void upgrade(int score)
    {
        scoreText.text = $"Puntuación {score}";
    }

}
