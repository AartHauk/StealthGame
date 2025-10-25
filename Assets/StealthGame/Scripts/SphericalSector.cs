using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphericalSector
{

    public static Mesh GenerateMesh(int azimuth, int altitude, float radius, int resolution)
    {
        if (radius < 0) radius = 0;

        if (resolution < 1) resolution = 1;

        if (altitude < 0) altitude = 0;
        else if (altitude > 180) altitude = 180;

        if (azimuth < 0) azimuth = 0;
        else if (azimuth > 360) azimuth = 360;

        int AltH = 90 - altitude / 2;
        int AziL = 0 - azimuth / 2;

        Vector3[] vertices = new Vector3[(resolution + 1) * (resolution + 1) + 1];
        int[] triangles = new int[resolution * resolution * 6 + resolution * 3 * 4];

        float tStep = 1f * altitude / resolution;
        float pStep = 1f * azimuth / resolution;
        int vIndex = 0;
        for (int j = 0; j <= resolution; j++)
        {
            for (int i = 0; i <= resolution; i++)
            {
                float theta = (AltH + i * tStep) * Mathf.Deg2Rad;
                float phi = (AziL + j * pStep) * Mathf.Deg2Rad;

                float x = radius * Mathf.Cos(phi) * Mathf.Sin(theta);
                float z = radius * Mathf.Sin(phi) * Mathf.Sin(theta);
                float y = radius * Mathf.Cos(theta);

                vertices[vIndex] = new Vector3(x, y, z);
                vIndex++;
            }
        }
        vertices[vIndex] = new Vector3(0, 0, 0);
         
        for (int j = 0; j < resolution; j++)
        {
            for (int i = 0; i < resolution; i++)
            {
                int ul = i + (resolution + 1) * j;
                int ur = i + (resolution + 1) * j + 1;
                int ll = i + (resolution + 1) * (j + 1);
                int lr = i + (resolution + 1) * (j + 1) + 1;

                int tIndex = (i + resolution * j) * 6;
                triangles[tIndex] = ur;
                triangles[tIndex + 1] = ul;
                triangles[tIndex + 2] = lr;

                triangles[tIndex + 3] = lr;
                triangles[tIndex + 4] = ul;
                triangles[tIndex + 5] = ll;
            }
        }

        for (int i = 0; i < resolution; i++)
        {
            int tIndex = resolution * resolution * 6 + i * 3;

            triangles[tIndex] = i;
            triangles[tIndex + 1] = i + 1;
            triangles[tIndex + 2] = vIndex;
        }

        for (int i = 0; i < resolution; i++)
        {
            int tIndex = resolution * resolution * 6 + i * 3 + resolution * 3;

            triangles[tIndex] = i * (resolution + 1) + resolution;
            triangles[tIndex + 1] = (i + 1) * (resolution + 1) + resolution;
            triangles[tIndex + 2] = vIndex;
        }

        for (int i = 0; i < resolution; i++)
        {
            int tIndex = resolution * resolution * 6 + i * 3 + resolution * 3 * 2 ;

            triangles[tIndex] = resolution * (resolution + 1) + i + 1;
            triangles[tIndex + 1] = resolution * (resolution + 1) + i;
            triangles[tIndex + 2] = vIndex;
        }

        
        for (int i = 0; i < resolution; i++)
        {
            int tIndex = resolution * resolution * 6 + i * 3 + resolution * 3 * 3 ;

            triangles[tIndex] = (i+1) * (resolution + 1);
            triangles[tIndex + 1] = i * (resolution + 1);
            triangles[tIndex + 2] = vIndex;
        }

        Mesh mesh = new Mesh();
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
}
