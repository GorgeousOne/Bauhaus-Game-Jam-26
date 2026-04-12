using UnityEngine;
using UnityEngine.Video;

public class OutroHandler : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject videorawimage;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake() {
        videoPlayer = GetComponent<VideoPlayer>();
        
videoPlayer.loopPointReached += OnMovieFinished;
    }
    // Update is called once per frame

        // Source - https://stackoverflow.com/a/68048325
// Posted by amitklein, modified by community. See post 'Timeline' for change history
// Retrieved 2026-04-12, License - CC BY-SA 4.0


//the action on finish
void OnMovieFinished(UnityEngine.Video.VideoPlayer vp)
{
    Destroy(videorawimage);
    Destroy(this);
}



}
