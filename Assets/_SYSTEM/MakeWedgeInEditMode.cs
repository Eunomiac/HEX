using UnityEditor;
using UnityEngine;

public class MakeWedgeInEditMode : MonoBehaviour
{
    public float wedgeRadius = 50f;
    public float centerRadius = 10f;
    public GameObject testObject;

    private float wedgeHeight, centerHeight;

    void Awake ()
    {
        wedgeHeight = Mathf.Sqrt(Mathf.Pow(wedgeRadius, 2) - Mathf.Pow(wedgeRadius / 2, 2));
        centerHeight = Mathf.Sqrt(Mathf.Pow(centerRadius, 2) - Mathf.Pow(centerRadius / 2, 2));
    }

    [ContextMenu("Make Wedge Mesh")]
    void MakeWedgeMesh ()
    {
        wedgeHeight = Mathf.Sqrt(Mathf.Pow(wedgeRadius, 2) - Mathf.Pow(wedgeRadius / 2, 2));
        centerHeight = Mathf.Sqrt(Mathf.Pow(centerRadius, 2) - Mathf.Pow(centerRadius / 2, 2));
        Mesh mesh = new Mesh();
        mesh.name = "Wedge Mesh";

        Vector3[] vertices = new Vector3[4];
        vertices[1] = new Vector3(-wedgeRadius / 2, 0, wedgeHeight);
        vertices[0] = vertices[1] * centerRadius / wedgeRadius;
        vertices[2] = new Vector3(wedgeRadius / 2, 0, wedgeHeight);
        vertices[3] = vertices[2] * centerRadius / wedgeRadius;
        mesh.vertices = vertices;

        Vector2[] uvVertices = new Vector2[4];
        for ( int i = 0; i < uvVertices.Length; i++ )
            uvVertices[i] = new Vector2((vertices[i].x + wedgeRadius / 2) / wedgeRadius, vertices[i].z / wedgeHeight);
        mesh.uv = uvVertices;

        mesh.triangles = new int[] {
            0, 1, 2,
            0, 2, 3
        };

        AssetDatabase.CreateAsset(mesh, "Assets/WedgeMesh.asset");
    }

    [ContextMenu("Make Wedge Particle Mesh")]
    void MakeWedgeParticleMesh ()
    {
        wedgeHeight = Mathf.Sqrt(Mathf.Pow(wedgeRadius, 2) - Mathf.Pow(wedgeRadius / 2, 2));
        centerHeight = Mathf.Sqrt(Mathf.Pow(centerRadius, 2) - Mathf.Pow(centerRadius / 2, 2));
        Mesh mesh = new Mesh();
        mesh.name = "Wedge Particle Mesh";

        Vector3[] vertices = new Vector3[8];
        vertices[2] = new Vector3(-wedgeRadius / 2, 0, wedgeHeight);
        vertices[0] = vertices[2] * centerRadius / wedgeRadius;
        vertices[4] = new Vector3(wedgeRadius / 2, 0, wedgeHeight);
        vertices[6] = vertices[4] * centerRadius / wedgeRadius;
        for ( int i = 1; i < vertices.Length; i += 2 )
            vertices[i] = vertices[i - 1] - Vector3.down * 0.1f;
        mesh.vertices = vertices;

        Vector2[] uvVertices = new Vector2[8];
        for ( int i = 0; i < uvVertices.Length; i++ )
            uvVertices[i] = new Vector2((vertices[i].x + wedgeRadius / 2) / wedgeRadius, vertices[i].z / wedgeHeight);
        mesh.uv = uvVertices;

        mesh.triangles = new int[] {
            0, 1, 2,    2, 1, 3,
            2, 3, 4,    4, 3, 5,
            4, 5, 6,    6, 5, 7,
            6, 7, 0,    0, 7, 1 };

        AssetDatabase.CreateAsset(mesh, "Assets/WedgeParticleMesh.asset");
    }

    [ContextMenu("Make Center Mesh")]
    void MakeCenterMesh ()
    {
        Mesh mesh = new Mesh();
        mesh.name = "Center Mesh";

        GameObject vertexTracer = new GameObject("Vertex Tracer");
        vertexTracer.transform.position = Vector3.zero;
        GameObject thisVertex = new GameObject("Vertex");
        thisVertex.transform.SetParent(vertexTracer.transform, false);
        thisVertex.transform.position = centerRadius * Vector3.RotateTowards(Vector3.forward, Vector3.right, 30f * Mathf.Deg2Rad, 0f);


        Vector3[] vertices = new Vector3[6];
        for ( int i = 0; i < vertices.Length; i++ )
        {
            vertexTracer.transform.Rotate(Vector3.up, 60f);
            vertices[i] = thisVertex.transform.position;
        }

        mesh.vertices = vertices;

        Vector2[] uvVertices = new Vector2[6];
        for ( int i = 0; i < uvVertices.Length; i++ )
        {
            uvVertices[i] = new Vector2((vertices[i].x + centerRadius / 2) / centerRadius, (vertices[i].y + centerRadius / 2) / centerRadius);
            Debug.Log(uvVertices[i].ToString());
        }
        mesh.uv = uvVertices;

        mesh.triangles = new int[] {
            0, 4, 5,
            0, 1, 4,
            1, 3, 4,
            1, 2, 3
        };

        AssetDatabase.CreateAsset(mesh, "Assets/CenterMesh.asset");
        DestroyImmediate(thisVertex);
        DestroyImmediate(vertexTracer);
    }

    [ContextMenu("Make Center Particle Mesh")]
    void MakeCenterParticleMesh ()
    {
        Mesh mesh = new Mesh();
        mesh.name = "Center Particle Mesh";

        GameObject vertexTracer = new GameObject("Vertex Tracer");
        vertexTracer.transform.position = Vector3.zero;
        GameObject thisVertex = new GameObject("Vertex");
        thisVertex.transform.SetParent(vertexTracer.transform, false);
        thisVertex.transform.position = centerRadius * Vector3.RotateTowards(Vector3.forward, Vector3.right, 30f * Mathf.Deg2Rad, 0f);


        Vector3[] vertices = new Vector3[12];
        for ( int i = 0; i < vertices.Length; i += 2 )
        {
            vertexTracer.transform.Rotate(Vector3.up, 60f);
            vertices[i] = thisVertex.transform.position;
        }
        for ( int i = 1; i < vertices.Length; i += 2 )
            vertices[i] = vertices[i - 1] - Vector3.down * 0.1f;
        mesh.vertices = vertices;

        Vector2[] uvVertices = new Vector2[12];
        for ( int i = 0; i < uvVertices.Length; i++ )
            uvVertices[i] = new Vector2((vertices[i].x + centerRadius / 2) / centerRadius, (vertices[i].y + centerRadius / 2) / centerRadius);
        mesh.uv = uvVertices;

        mesh.triangles = new int[] {
            0, 1, 2,    2, 1, 3,
            2, 3, 4,    4, 3, 5,
            4, 5, 6,    6, 5, 7,
            6, 7, 8,    8, 7, 9,
            8, 9, 10,   10, 9, 11,
            10, 11, 0,  0, 11, 1
        };

        AssetDatabase.CreateAsset(mesh, "Assets/CenterParticleMesh.asset");
        DestroyImmediate(thisVertex);
        DestroyImmediate(vertexTracer);
    }

}
