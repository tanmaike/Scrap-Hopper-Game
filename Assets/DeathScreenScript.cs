using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenScript : MonoBehaviour {

    public void RestartGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void ReturnToMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }
}

