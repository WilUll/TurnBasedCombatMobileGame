using UnityEngine;
public interface IAudioService
{
    public void Unitialize();
    public void Initialize();

    public void PlayOneShot(AudioClip clip);
    public void PlayOneShot(string clipName);

    public void SetProperties(string clipName, float volume, float pitch);
    public void SetProperties(AudioClip clip, float volume, float pitch);
}