using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=VBZFYGWvm4A
// https://www.youtube.com/watch?v=v7o7cjFqvrY
// https://www.youtube.com/watch?v=rnqF6S7PfFA
// https://www.youtube.com/watch?v=eUFwxK9Z9aw
public class Grid_1 : MonoBehaviour
{
    [SerializeField]
    public float size = 1f; // distance
    public float width = 20f;
    public bool turnOn = true;
    public Vector3 getPointOnGrid(Vector3 position)
    {
        // position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size
        );

        // result += transform.position;

        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (turnOn)
        {
            for (float x = 0; x < width; x += size)
            {
                for (float z = 0; z < width; z += size)
                {
                    var point = getPointOnGrid(new Vector3(x, 0f, z));
                    point += transform.position;
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
