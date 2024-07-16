using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LevelChooser : MonoBehaviour
{
    public UnityEvent<int> OnPlayPressed = new UnityEvent<int>();

    [SerializeField] private List<Button> levelBtnList;

    private void Start()
    {
        int i = 0;
        foreach (var btn in levelBtnList)
        {
            btn.onClick.AddListener( delegate { LoadLevel(i++); } );
        }
    }

    private void LoadLevel(int i)
    {
        //OnPlayPressed.Invoke(i);
        
        App.Instance.GameplayCore.PressPlay();
        App.Instance.GameplayCore.ChangeLevel(i);
        App.Instance.GameplayCore.GameManager.ChoosenLevel = i;
    }
}
