using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawHexTile : MonoBehaviour
{
    public Material mat;

    
    public float height = 0.2f;

    private float radius = 1 / (2 * Mathf.Cos( Mathf.PI / 6));
    private int vertexNB = 6;
    // Start is called before the first frame update
    void Start()
    {
        Draw();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void Draw()
    {
        Vector3[] vertices = new Vector3[(vertexNB + 1) * 2]; // (6 vertices + 1 center) * 2 

        int[] triangles = new int[vertexNB * 2 * 2 * 3];

        
        if (gameObject.GetComponent<MeshFilter>() == null)
        {
            gameObject.AddComponent<MeshFilter>();          // Creation d'un composant MeshFilter qui peut ensuite être visualisé
            gameObject.AddComponent<MeshRenderer>();
        }

        float phi = 2 * (Mathf.PI) / vertexNB;
        float teta = 0.0f;

        // populate vertices
        int verticeCount = 0;
        while(verticeCount < vertices.Length - 2)
        {
            float x = radius * Mathf.Cos(teta);
            float z = radius * Mathf.Sin(teta);

            vertices[verticeCount++] = new Vector3(x, 0.0f, z);
            vertices[verticeCount++] = new Vector3(x, height, z);
            teta += phi;
        }


        // add center vertices
        vertices[verticeCount++] = new Vector3(0.0f, 0.0f, 0.0f);
        vertices[verticeCount++] = new Vector3(0.0f, height, 0.0f);

        // populate triangles

        int triangleCount = 0;
        // top
        for (int i = 0; i < (vertexNB * 2) - 2; i += 2)
        {
            triangles[triangleCount++] = i;
            triangles[triangleCount++] = i + 2;
            triangles[triangleCount++] = vertices.Length - 2;

        }

        triangles[triangleCount++] = vertices.Length - 4;
        triangles[triangleCount++] = 0;
        triangles[triangleCount++] = vertices.Length - 2;


        // bottom 
        for (int i = 1; i < (vertexNB * 2) - 2; i += 2)
        {
            triangles[triangleCount++] = i;
            triangles[triangleCount++] = vertices.Length - 1;
            triangles[triangleCount++] = i + 2;


        }

        triangles[triangleCount++] = vertices.Length - 3;
        triangles[triangleCount++] = vertices.Length - 1;
        triangles[triangleCount++] = 1;


        // sides
        int vertexA = 0;
        int vertexB = 0;
        int vertexC = 0;
        int vertexD = 0;

        for (int i = 0; i < (vertexNB * 2) - 2; i += 2)
        {
            vertexA = i;
            vertexB = vertexA + 1;
            vertexC = vertexB + 1;
            vertexD = vertexC + 1;

            triangles[triangleCount++] = vertexA;
            triangles[triangleCount++] = vertexB;
            triangles[triangleCount++] = vertexD;

            triangles[triangleCount++] = vertexA;
            triangles[triangleCount++] = vertexD;
            triangles[triangleCount++] = vertexC;

        }
        vertexA = (vertexNB * 2) - 2;
        vertexB = vertexA + 1;
        vertexC = 0;
        vertexD = vertexC + 1;

        triangles[triangleCount++] = vertexA;
        triangles[triangleCount++] = vertexB;
        triangles[triangleCount++] = vertexD;

        triangles[triangleCount++] = vertexA;
        triangles[triangleCount++] = vertexD;
        triangles[triangleCount++] = vertexC;

        Mesh msh = new Mesh();                          // Création et remplissage du Mesh

        msh.vertices = vertices;
        msh.triangles = triangles;
        msh.RecalculateNormals();


        gameObject.GetComponent<MeshFilter>().mesh = msh;           // Remplissage du Mesh et ajout du matériel
        gameObject.GetComponent<MeshRenderer>().material = mat;

        // Add a MeshCollider component to the GameObject
        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        if (meshCollider == null)
        {
            meshCollider = gameObject.AddComponent<MeshCollider>();
        }

        // Set the mesh of the MeshCollider to be the same as the rendered mesh
        meshCollider.sharedMesh = msh;
    }
}
