using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=VBZFYGWvm4A
// https://www.youtube.com/watch?v=v7o7cjFqvrY
// https://www.youtube.com/watch?v=eUFwxK9Z9aw
// https://www.youtube.com/watch?v=s926MfazI50
public class GridSystem : MonoBehaviour
{
    private static GridSystem _instance;
    public static GridSystem Instance { get { return _instance; } } // a singleton

    // 2D array to store gameObject reference
    public GameObject[,] objectOnGrid;

    public Color baseColor;

    public static Material lineMaterial;

    public float gridSize = 1f; // distance
    public int width = 20; // x
    public int length = 20; // z
    public float gridLineHeight = 1; // y
    public bool followParentScale = true;

    private void Awake()
    {
        // singleton condition
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        // https://answers.unity.com/questions/672007/how-do-i-change-gl-lines-color.html
        // https://answers.unity.com/questions/987078/materialstring-is-obsolote.html
        // https://stackoverflow.com/questions/39709867/trying-to-create-a-material-from-string-this-is-no-longer-supported
        // if (!lineMaterial)
        // {
        //     lineMaterial = new Material(Shader.Find("Lines/Colored Blended"));
        //     lineMaterial.hideFlags = HideFlags.HideAndDontSave;
        //     lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;

        // }
        // lineMaterial.hideFlags = HideFlags.HideAndDontSave;
        // lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;

        // initialize objectOnGrid 2D array
        objectOnGrid = new GameObject[width - 2, length - 2];
    }

    public Vector3 getPointOnGrid(Vector3 position)
    {
        Vector3 snapPos = Vector3.zero;
        snapPos.x = Mathf.RoundToInt(position.x / gridSize) * gridSize;
        // y always zero on grid
        snapPos.z = Mathf.RoundToInt(position.z / gridSize) * gridSize;

        return snapPos;
    }

    private void Update()
    {
        if (followParentScale)
        {
            width = Mathf.FloorToInt(transform.root.gameObject.transform.localScale.x * 10 + 1);
            length = Mathf.FloorToInt(transform.root.gameObject.transform.localScale.z * 10 + 1);
        }
    }

    public Vector3 getPositionByGridPoint(int x, int z)
    {
        x -= (width - 1) / 2 - 1;
        z -= (length - 1) / 2 - 1;
        Vector3 tempPosition = new Vector3(x, 0, z);
        return tempPosition;
    }

    public Vector2Int getGridPointByPosition(Vector3 worldPosition)
    {
        Vector2Int tempPosition = new Vector2Int(Mathf.RoundToInt(worldPosition.x),
                                                    Mathf.RoundToInt(worldPosition.z));
        tempPosition.x += (width - 1) / 2 - 1;
        tempPosition.y += (length - 1) / 2 - 1;
        return tempPosition;
    }

    public Vector3 getRoundedPosition(Vector3 worldPosition)
    {
        return new Vector3(Mathf.RoundToInt(worldPosition.x),
                            0,
                            Mathf.RoundToInt(worldPosition.z));
    }

    // private void OnDrawGizmos()
    // {
    //     DisplayGridLines();
    // }

    // public void DisplayGridLines()
    // {
    //     if (!lineMaterial)
    //     {
    //         lineMaterial = new Material(Shader.Find("Lines/Colored Blended"));
    //         lineMaterial.hideFlags = HideFlags.HideAndDontSave;
    //         lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
    //     }
    //     GL.PushMatrix();
    //     lineMaterial.SetPass(0);
    //     GL.Begin(GL.LINES);

    //     float gridWidth = -Mathf.FloorToInt(width / 2) - 0.5f;
    //     float gridLength = -Mathf.FloorToInt(length / 2) - 0.5f;
    //     // draw grid
    //     for (float z = 0; z < length; z += gridSize)
    //     {
    //         GL.Color(baseColor);
    //         GL.Vertex3(gridWidth, gridLineHeight, z + gridLength);
    //         GL.Vertex3(width + gridWidth, gridLineHeight, z + gridLength);
    //     }
    //     for (float x = 0; x < width; x += gridSize)
    //     {
    //         GL.Color(baseColor);
    //         GL.Vertex3(x + gridWidth, gridLineHeight, gridLength);
    //         GL.Vertex3(x + gridWidth, gridLineHeight, length + gridLength);
    //     }

    //     GL.End();
    //     GL.PopMatrix();
    // }


}
