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

    public Color baseColor;

    public static Material lineMaterial;

    public float gridSize = 1f; // distance
    public float width = 20f;
    public float length = 20f;
    public bool showGizmos = true;
    public bool followParentScale = true;

    private void Awake()
    {
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
            lineMaterial = new Material(Shader.Find("Lines/Colored Blended"));
        }
        lineMaterial.hideFlags = HideFlags.HideAndDontSave;
        lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
    }

    public Vector3 getPointOnGrid(Vector3 position)
    {
        int xCount = Mathf.RoundToInt(position.x / gridSize);
        int yCount = Mathf.RoundToInt(position.y / gridSize);
        int zCount = Mathf.RoundToInt(position.z / gridSize);

        Vector3 result = new Vector3(
            (float)xCount * gridSize,
            (float)yCount * gridSize,
            (float)zCount * gridSize
        );

        return result;
    }

    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            DisplayGizmo();
        }
        DisplayGridLines();
    }

    public void DisplayGridLines()
    {
        if (!lineMaterial)
        {
            lineMaterial = new Material(Shader.Find("Lines/Colored Blended"));
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
        }
        GL.PushMatrix();
        lineMaterial.SetPass(0);
        GL.Begin(GL.LINES);

        float gridWidth = -Mathf.FloorToInt(width / 2) - 0.5f;
        float gridLength = -Mathf.FloorToInt(length / 2) - 0.5f;
        // draw grid
        for (float z = 0; z < length; z += gridSize)
        {
            GL.Color(baseColor);
            GL.Vertex3(gridWidth, 0.05f, z + gridLength);
            GL.Vertex3(width + gridWidth, 0.05f, z + gridLength);
        }
        for (float x = 0; x < width; x += gridSize)
        {
            GL.Color(baseColor);
            GL.Vertex3(x + gridWidth, 0.05f, gridLength);
            GL.Vertex3(x + gridWidth, 0.05f, length +gridLength);
        }

        GL.End();
        GL.PopMatrix();
    }

    void DisplayGizmo()
    {
        Gizmos.color = baseColor;
        if (followParentScale)
        {
            width = transform.root.gameObject.transform.localScale.x * 10 + 1;
            length = transform.root.gameObject.transform.localScale.z * 10 + 1;
        }
        for (float x = 0; x < width; x += gridSize)
        {
            for (float z = 0; z < length; z += gridSize)
            {
                var point = getPointOnGrid(new Vector3(x, 0f, z));
                point += transform.position - new Vector3(Mathf.FloorToInt(width / 2), 0, Mathf.FloorToInt(length / 2));

                Gizmos.DrawSphere(point, 0.1f);
            }
        }

    }
}
