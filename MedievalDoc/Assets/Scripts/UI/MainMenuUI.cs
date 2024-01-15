using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;

    private void Awake() {
        playButton.onClick.AddListener( () => {
            SceneManager.LoadScene(1); //In build settings mainscene is 1
        });
        optionsButton.onClick.AddListener( () => {
            //click 
        });
        quitButton.onClick.AddListener( () => {
            Application.Quit();
        });

    }

}
