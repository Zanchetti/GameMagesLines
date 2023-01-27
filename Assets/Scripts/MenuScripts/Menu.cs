using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void startGame() { 
        SceneManager.LoadScene("Jogo");
    }

    public void quitGame()
    {
        //Editor da unity
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
        //Jogo Compilado
        //Application.Quit();
    }
}
