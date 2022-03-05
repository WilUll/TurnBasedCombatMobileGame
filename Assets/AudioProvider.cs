using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class AudioProvider : IAudioService
{
    private const int sourceCount = 25;
    List<AudioClip> audioClips;
    List<AudioSource> audioSources;
    Dictionary<string, AudioClip> audioLibrary;
    AudioMixer audioMixer;
    AudioMixerGroup[] group;
    string path = "SFX/";

    public void Initialize()
    {
        audioMixer = Resources.Load(path + "Master") as AudioMixer;
        group = audioMixer.FindMatchingGroups("Master");


        audioSources = new List<AudioSource>();
        for (int i = 0; i < sourceCount; i++)
        {
            AudioSource newSource = new GameObject("(Created at runtime) audioSource").AddComponent<AudioSource>();
            newSource.outputAudioMixerGroup = group[0];
            audioSources.Add(newSource);
        }

        audioClips = new List<AudioClip>();
        audioClips.AddRange(Resources.LoadAll<AudioClip>(path));

        audioLibrary = new Dictionary<string, AudioClip>();
        foreach (var clip in audioClips)
        {
            audioLibrary.Add(clip.name.ToLower(), clip);
        }
    }

    public AudioClip GetFromLibrary(string clipName)
    {
        if (audioLibrary.ContainsKey(clipName.ToLower()))
        {
            return audioLibrary[clipName.ToLower()];
        }
        else
        {
            return audioLibrary["error"];
        }
    }

    AudioSource FindPlayingAudioSource(AudioClip playing)
    {
        foreach (var source in audioSources)
        {
            if (source.isPlaying == true && source.clip == playing) return source;
        }

        return audioSources.Where(x => x.loop == false).ToList()[0];
    }

    AudioSource GetAvaliableAudioSource()
    {
        foreach (var source in audioSources)
        {
            if (source.isPlaying == false) return source;
        }

        return audioSources.Where(x => x.loop == false).ToList()[0];
    }

    public void PlayOneShot(AudioClip clip)
    {
        GetAvaliableAudioSource().PlayOneShot(clip);
    }

    public void PlayOneShot(string clipName)
    {
        PlayOneShot(GetFromLibrary(clipName));
    }

    public void SetProperties(AudioClip clip, float volume, float pitch)
    {
        AudioSource source = FindPlayingAudioSource(clip);
        source.volume = volume;
        source.pitch = pitch;
    }

    public void SetProperties(string clipName, float volume, float pitch)
    {
        SetProperties(GetFromLibrary(clipName.ToLower()), volume, pitch);
    }

    public void Unitialize()
    {
        for (int i = 0; i < sourceCount; i++)
        {
            MonoBehaviour.Destroy(audioSources[i]);
        }
    }
}
