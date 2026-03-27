using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

// primitive line-voiceover handler to play audios connected to lined Ids without localization
public class YarnAudioPlayer : DialoguePresenterBase
{
    [System.Serializable]
    public class AudioEntry
    {
        public string lineId;
        public AudioClip clip;
    }

    public List<AudioEntry> lineAudios = new();
    private Dictionary<string, AudioClip> audioMap;
    private AudioSource audioSource;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioMap = new Dictionary<string, AudioClip>();
        foreach (var e in lineAudios)
        {
            audioMap.Add(e.lineId, e.clip);
        }
    }

    public override YarnTask RunLineAsync(LocalizedLine line, LineCancellationToken token)
    {
        string lineId = line.TextID.Split(':')[1];
        if (audioMap.TryGetValue(lineId, out var clip))
        {
            audioSource.PlayOneShot(clip);
        } else
        {

        }
        return YarnTask.CompletedTask;
    }

    public override YarnTask OnDialogueCompleteAsync()
    {
        return YarnTask.CompletedTask;
    }

     public override YarnTask OnDialogueStartedAsync()
    {
        return YarnTask.CompletedTask;
    }
}