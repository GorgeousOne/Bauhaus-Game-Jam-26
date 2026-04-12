using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GoatPuzzle : MonoBehaviour
{
    public static GoatPuzzle Instance;

    [Header("Grid Settings")]
    public RectTransform gridOrigin;
    public float cellSize = 30f;

    OrganPiece[] organs;

    // Grid map: '#' = valid cell, '.' = empty
    // Edit this to match your background image!
    // GridAnchor pivot (0,1) = top-left corner of this map
    string[] gridMap = {
        "..########",  // Row 0: 2 Freifelder links, dann 8 Zellen
        "..########",  // Row 1
        "##########",  // Row 2: volle Breite
        "##########",  // Row 3
        "##########",  // Row 4
        "########..",  // Row 5
    };

    int maxCols;
    int rowCount;

    HashSet<Vector2Int> validCells = new();
    HashSet<Vector2Int> occupiedCells = new();

    void Awake()
    {
        Instance = this;
        rowCount = gridMap.Length;
        maxCols = gridMap.Max(r => r.Length);
        //first gen cells
        GenerateGrid();

        //then set organs to gr
        organs = GetComponentsInChildren<OrganPiece>();
        foreach (OrganPiece piece in organs)
        {
            piece.DragEnd.AddListener(OnPieceDrop);
            piece.transform.SetParent(gridOrigin, true);
            RectTransform pieceRect = piece.GetComponent<RectTransform>();
            pieceRect.anchorMin = pieceRect.anchorMax = new Vector2(0, 1);
            pieceRect.pivot = new Vector2(0, 1);
        }

    }

    void GenerateGrid()
    {
        for (int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < gridMap[row].Length; col++)
            {
                if (gridMap[row][col] != '#') continue;

                validCells.Add(new Vector2Int(col, row));

                // Visible debug cell (remove or hide later)
                GameObject cell = new GameObject($"Cell_{col}_{row}", typeof(RectTransform));
                cell.transform.SetParent(gridOrigin, false);

                //For testing
                Image img = cell.AddComponent<Image>();
                img.color = new Color(0.5f, 0.5f, 0.5f, 0.7f);


                RectTransform rt = cell.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(cellSize, cellSize);
                rt.anchorMin = rt.anchorMax = new Vector2(0, 1);
                rt.pivot = new Vector2(0, 1);
                rt.anchoredPosition = new Vector2(col * cellSize, -row * cellSize);
            }
        }
    }

    /// <summary>
    /// Converts a world position to grid coordinates (col, row).
    /// </summary>
    Vector2Int ScreenToGrid(Vector2 screenPos, Camera cam)
    {
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            gridOrigin, screenPos, null, out localPos);
        int col = Mathf.FloorToInt(localPos.x / cellSize + 0.5f);
        int row = Mathf.FloorToInt(-localPos.y / cellSize + 0.5f);

        return new Vector2Int(col, row);
    }

    // scale grid pos with cell size
    Vector2 GridToAnchoredPos(int col, int row)
    {
        return new Vector2(col * cellSize, -row * cellSize);
    }


    /// <summary>
    /// Called by OrganPiece.OnEndDrag. Returns true if piece was placed.
    /// </summary>
    public bool TryPlacePiece(OrganPiece piece, Vector2 screenPos, Camera cam)
    {
        Vector2Int gridPos = ScreenToGrid(screenPos, cam);

        // Check all cells this organ would occupy
        List<Vector2Int> newOccupiedCells = new();
        foreach (Vector2Int off in piece.shape)
        {
            Vector2Int cell = gridPos + off;

            if (!validCells.Contains(cell)) {
                return false;
            }
            if (occupiedCells.Contains(cell)) {
                return false;
            }

            newOccupiedCells.Add(cell);
        }

        // Place it
        foreach (Vector2Int c in newOccupiedCells)
        {
            GameObject cell = gridOrigin.transform.Find($"Cell_{c.x}_{c.y}").gameObject;
            Image img = cell.GetComponent<Image>();
            img.color = new Color(1f, 0.5f, 0.5f, 0.7f);
            occupiedCells.Add(c);
        }


        // Snap the piece to the grid
        Vector2 screenSnapPos = GridToAnchoredPos(gridPos.x, gridPos.y);
        RectTransform pieceRect = piece.GetComponent<RectTransform>();
        pieceRect.anchoredPosition = screenSnapPos;

        piece.placedAnchor = gridPos;
        piece.isPlaced = true;
        CheckWin();
        return true;
    }

    /// <summary>
    /// Called when a placed piece gets picked up again.
    /// </summary>
    public void RemovePiece(OrganPiece piece, Vector2Int placedAnchor)
    {
        foreach (Vector2Int off in piece.shape)
        {
            Vector2Int pos = placedAnchor + off;
            occupiedCells.Remove(placedAnchor + off);

            GameObject cell = gridOrigin.transform.Find($"Cell_{pos.x}_{pos.y}").gameObject;
            Image img = cell.GetComponent<Image>();
            img.color = new Color(0.5f, 0.5f, 1.0f, 0.7f);

        }
    }

    void CheckWin()
    {
        if (occupiedCells.Count >= validCells.Count)
        {
            Debug.Log("Puzzle gelöst!");
            Invoke(nameof(Hide), 1.5f);
            // TODO: Win-Event auslösen
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    void OnPieceDrop(OrganPiece piece, Vector2 position, Camera cam)
    {
        if (TryPlacePiece(piece, position, cam))
        {
            // Store where we were placed so we can remove later
            // (anchoredPosition was set by TryPlacePiece)
        }
        else
        {
            // Snap back to original position
            piece.move(new Vector2(cellSize/2, cellSize/2));
        }

    }
}