using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    bool maskSolved;
    bool candlesSolved;
    bool goatSolved;

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
        maskSolved = true;
        checkCompletion();
    }

    public void solvedCandles()
    {
        candlesSolved = true;
        checkCompletion();

    }

    public void solvedGoat()
    {
        goatSolved = true;
        checkCompletion();
    }

    void checkCompletion()
    {
        if (maskSolved && goatSolved && candlesSolved)
        {
            Debug.Log("TODO activate pentagram");
        }
    }
}
