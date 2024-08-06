using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MainMenuUI : MonoBehaviour {

    public UnityEvent OnPlayPressed = new UnityEvent();

    [SerializeField] private GameObject levelMenu;
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;

    private void Awake() {
        playButton.onClick.AddListener(HandlePlayPressed);
        optionsButton.onClick.AddListener( () => {
            //click 
        });
        quitButton.onClick.AddListener( () => {
            Application.Quit();
        });

    }


    private void HandlePlayPressed()
    {
        levelMenu.SetActive(true);
        // OnPlayPressed.Invoke();
        // App.Instance.GameplayCore.PressPlay();
    }
}
