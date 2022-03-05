using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        ServiceLocator.SetAudioProvider(new AudioProvider());
    }
}

