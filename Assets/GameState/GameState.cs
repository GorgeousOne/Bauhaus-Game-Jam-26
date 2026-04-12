using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    public bool MaskSolved;
    public bool CandlesSolved;
    public bool GoatSolved;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
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

    public void solvedGoat()
    {
        GoatSolved = true;
        checkCompletion();
    }

    void checkCompletion()
    {
        if (MaskSolved && GoatSolved && CandlesSolved)
        {
            Debug.Log("TODO activate pentagram");
        }
    }
}
