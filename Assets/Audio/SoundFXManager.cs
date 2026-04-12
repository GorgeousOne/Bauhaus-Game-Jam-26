using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    [SerializeField] private AudioSource soundFXObject;
    [SerializeField] private AudioClip soundClick;
    [SerializeField] private AudioClip soundRight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null){
            instance = this;
        }
    }

    // Update is called once per frame
    public void PlayClickSound(Transform spawnTransform)
    {
        PlaySound(soundClick, spawnTransform);

    }

    public void PlayRightSound(Transform spawnTransform)
    {
        PlaySound(soundRight, spawnTransform);

    }

    private void PlaySound(AudioClip audioClip, Transform spawnTransform0)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform0.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);

    }
}
