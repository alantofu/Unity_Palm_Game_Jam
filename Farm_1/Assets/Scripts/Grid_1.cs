using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=VBZFYGWvm4A
// https://www.youtube.com/watch?v=v7o7cjFqvrY
// https://www.youtube.com/watch?v=eUFwxK9Z9aw
public class Grid_1 : MonoBehaviour
{
    [SerializeField]
    public float gridSize = 1f; // distance
    public float width = 20f;
    public float length = 20f;
    public bool show = true;
    public bool followParentScale = true;
    public Vector3 getPointOnGrid(Vector3 position)
    {
        // position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / gridSize);
        int yCount = Mathf.RoundToInt(position.y / gridSize);
        int zCount = Mathf.RoundToInt(position.z / gridSize);

        Vector3 result = new Vector3(
            (float)xCount * gridSize,
            (float)yCount * gridSize,
            (float)zCount * gridSize
        );

        // result += transform.position;

        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (show)
        {
            if(followParentScale) {
                width = transform.root.gameObject.transform.localScale.x * 10 + 1;
                length = transform.root.gameObject.transform.localScale.z * 10 + 1;
            }
            for (float x = 0; x < width; x += gridSize)
            {
                for (float z = 0; z < length; z += gridSize)
                {
                    var point = getPointOnGrid(new Vector3(x, 0f, z));
                    point += transform.position - new Vector3(Mathf.FloorToInt(width / 2), 0, Mathf.FloorToInt(length / 2));
                    // Debug.Log(point);
                    Gizmos.DrawSphere(point, 0.1f);
                }
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
