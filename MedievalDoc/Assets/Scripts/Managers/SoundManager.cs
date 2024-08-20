using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameMusic;
    public AudioClip MenuMusic { get { return menuMusic; } }
    public AudioClip GameMusic { get { return gameMusic; } }
    private void Awake()
    {
        App.Instance.GameplayCore.RegisterSoundManager(this);
    }
    private void OnDestroy()
    {
        App.Instance.GameplayCore.UnregisterSoundManager();
    }

}
