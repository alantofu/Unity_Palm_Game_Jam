using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    // Draws a line from "startVertex" var to the current mouse position.
    public Material mat;
    Vector3 startVertex;
    Vector3 mousePos;

    void Start()
    {
        startVertex = Vector3.zero;
    }

    void Update()
    {
        mousePos = Input.mousePosition;
        // Debug.Log(mousePos);
        // Press space to update startVertex
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startVertex = new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, 0);
        }
        
    }

    private void OnDrawGizmos() {
        RenderLines(new Vector3[0]);
    }

    void OnPostRender()
    {
        if (!mat)
        {
            Debug.LogError("Please Assign a material on the inspector");
            return;
        }
        

        GL.PushMatrix();
        mat.SetPass(0);
        GL.LoadOrtho();

        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex(startVertex);
        GL.Vertex(new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, 0));
        GL.End();

        GL.PopMatrix();
    }

    void RenderLines(Vector3[] points)
    {
        GL.Begin(GL.LINES);
        mat.SetPass(0);
        GL.Color(Color.red);
        GL.Vertex3(0, 0, 0);
        GL.Vertex3(1, 1, 1);
        GL.End();
    }
}
