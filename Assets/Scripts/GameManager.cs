using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gamePaused = false;
    bool gameOver = false;
    public bool lento = false;
    public static int nivelJugado = 0;
    [SerializeField] Spaceship player;
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject menu;
    [SerializeField] int numEnemies;
    [SerializeField] int nivel;
    float usos = 3;
    float duracionTiempoLento = 2;
    float slow = 0;

    // Start is called before the first frame update
    void Start()
    {
        pauseUI.SetActive(false);
        gameOverUI.SetActive(false);
        menu.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && gameOver == false)
        {
            PauseGame();
        }
        if (Input.GetKeyDown(KeyCode.Q) && gameOver == false && Time.time >=
            slow && lento == false && usos > 0)
        {
            TiempoLento();
            slow = Time.time + duracionTiempoLento;
            usos = usos - 1;
        }
        if (gameOver == false && Time.time >= slow)
        {
            TiempoNormal();
        }
    }

    public void StartGame()
    {
        nivelActual();
        siguienteNivel(nivelJugado, 0);
    }

    void PauseGame()
    {
        gamePaused = gamePaused ? false : true;

        player.gamePaused = gamePaused;

        pauseUI.SetActive(gamePaused);

        Time.timeScale = gamePaused ? 0 : 1;
    }
    public void TiempoLento()
    {
        lento = lento ? false : true;
        player.lento = lento;
        Time.timeScale = lento ? 0.5f : 1;
    }
    public void TiempoNormal()
    {
        lento = false;

        Time.timeScale = 1;
    }

    public void ReducirNumEnemigos()
    {
        numEnemies = numEnemies - 1;
        if (numEnemies < 1)
        {
            nivelJugado = nivelActual();

            if (nivelJugado < 3)
            {
                siguienteNivel(nivelJugado, 4);
            }
            else
            {
                Ganar();
            }
        }
    }

    public int nivelActual()
    {
        Scene escenaActual = SceneManager.GetActiveScene();
        int nivel = escenaActual.buildIndex;
        return nivel;
    }

    public void cargarSiguiente()
    {
        siguienteNivel(nivelJugado, 0);
    }

    public void siguienteNivel(int nivel, int siguiente)
    {
        if (nivel == -1)
        {
            SceneManager.LoadScene(nivel + 1);
            Time.timeScale = 1;
        }
        else if (siguiente == 4)
        {
            SceneManager.LoadScene(siguiente);
            Time.timeScale = 1;
        }
        else
        {
            SceneManager.LoadScene(nivel + 1);
            Time.timeScale = 1;
        }
    }

    public void menuInicial()
    {
        nivelJugado = 0;
        siguienteNivel(-1, 0);
    }

    void Ganar()
    {
        gameOver = true;
        Time.timeScale = 0;
        player.gamePaused = true;
        gameOverUI.SetActive(true);
    }
    
}
