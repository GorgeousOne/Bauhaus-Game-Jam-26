using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    public bool MaskSolved;
    public bool CandlesSolved;
    public bool GoatSolved;
    public UnityEvent AllPuzzlesSolvedEvent;


    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Invoke(nameof(Cheat), 2f);
    }

    public void solveMask()
    {
        MaskSolved = true;
        checkCompletion();
    }

    public void solvedCandles()
    {
        CandlesSolved = true;
        checkCompletion();
    }

    void Cheat()
    {
        // setComplete();
        checkCompletion();
    }

    public void solvedGoat()
    {
        GoatSolved = true;
        checkCompletion();
    }

    void checkCompletion()
    {
        if (MaskSolved && GoatSolved && CandlesSolved)
        {
            setComplete();
        }
    }

    void setComplete()
    {
        DialogueRunner runner = GameObject.FindWithTag("Yarn").GetComponent<DialogueRunner>();
        InMemoryVariableStorage varStorage = runner.VariableStorage as InMemoryVariableStorage;
        varStorage.SetValue("$ritualChecks", 3);
        AllPuzzlesSolvedEvent.Invoke();
        Debug.Log("TODO activate pentagram");
    }

    public void Restart()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
