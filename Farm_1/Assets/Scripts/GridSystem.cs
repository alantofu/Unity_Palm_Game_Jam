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

    public Material lineMaterial;

    public float gridSize = 1f; // distance
    public float lineThickness = 0.05f;
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
        if (!lineMaterial)
        {
            // lineMaterial = new Material(Shader.Find("Lines/Colored Blended"));
            lineMaterial = Resources.Load("Materials/Gridline Shader Mat", typeof(Material)) as Material;
            // lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
        }
        lineMaterial.hideFlags = HideFlags.None;
        lineMaterial.shader.hideFlags = HideFlags.None;

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

    private void OnDrawGizmos()
    {
        DisplayGridLines();
    }

    public void DisplayGridLines()
    {
        if (!lineMaterial)
        {
            // lineMaterial = new Material(Shader.Find("Lines/Colored Blended"));
            // lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
            lineMaterial.hideFlags = HideFlags.None;
            lineMaterial.shader.hideFlags = HideFlags.None;
            return;
        }

        // offset values (negative value)
        float startX = -Mathf.FloorToInt(width / 2) - 0.5f;
        float startZ = -Mathf.FloorToInt(length / 2) - 0.5f;

        GL.PushMatrix();
        lineMaterial.SetPass(0);
        // GL.Begin(GL.LINES);
        GL.Begin(GL.QUADS);

        // horizontal line
        for (float z = 0; z < length; z += gridSize)
        {
            GL.Color(baseColor);
            GL.Vertex3(startX, gridLineHeight, z + startZ);
            GL.Vertex3(startX, gridLineHeight, z + startZ + lineThickness);
            GL.Vertex3(width + startX, gridLineHeight, z + startZ + lineThickness);
            GL.Vertex3(width + startX, gridLineHeight, z + startZ);
        }
        // vertical line
        for (float x = 0; x < width; x += gridSize)
        {
            GL.Color(baseColor);
            GL.Vertex3(x + startX, gridLineHeight, startZ);
            GL.Vertex3(x + startX + lineThickness, gridLineHeight, startZ);
            GL.Vertex3(x + startX + lineThickness, gridLineHeight, length + startZ);
            GL.Vertex3(x + startX, gridLineHeight, length + startZ);
        }


        // draw grid
        // horizontal line
        // for (float z = 0; z < length; z += gridSize)
        // {
        //     GL.Color(baseColor);
        //     GL.Vertex3(startZ, gridLineHeight, z + startX);
        //     GL.Vertex3(width + startZ, gridLineHeight, z + startX);
        // }
        // // vertical line
        // for (float x = 0; x < width; x += gridSize)
        // {
        //     GL.Color(baseColor);
        //     GL.Vertex3(x + startZ, gridLineHeight, startX);
        //     GL.Vertex3(x + startZ, gridLineHeight, length + startX);
        // }

        GL.End();
        GL.PopMatrix();
    }


}
