using System.Collections.Generic;
using UnityEngine;

public class Model
{
    internal List<Vector3> vertices;
    internal List<Vector3Int> faces;
    List<Vector2> texture_coordinates;
    List<Vector3Int> texture_index_list;

    // size of the texture image: 1024, 2048, 4096 ... (same layout, just higher resolution)
    public int textureSize = 1024;

    public Model(int textureSize = 1024)
    {
        this.textureSize = textureSize;

        vertices = new List<Vector3>();
        addVertices();

        texture_coordinates = new List<Vector2>();
        addTextureCoordinates();

        faces = new List<Vector3Int>();
        texture_index_list = new List<Vector3Int>();
        addFaces();

        texture_coordinates = convertToUV(textureSize);
    }

    private List<Vector2> convertToUV(float s)
    {
        List<Vector2> hold = new List<Vector2>();
        foreach (Vector2 v in texture_coordinates) { hold.Add(new Vector2(v.x / s, 1 - v.y / s)); }
        return hold;
    }

    // positions are written for a 1024 texture and scaled up to the real texture size
    private void AddUV(float x, float y)
    {
        float scale = textureSize / 1024f;
        texture_coordinates.Add(new Vector2(x * scale, y * scale));
    }

    // takes the vertex numbers from the diagram (starting at 1) and stores them 0-based
    private void AddFace(int a, int b, int c, int ta, int tb, int tc)
    {
        faces.Add(new Vector3Int(a - 1, b - 1, c - 1));
        texture_index_list.Add(new Vector3Int(ta, tb, tc));
    }

    private void addVertices()
    {
        vertices.Add(new Vector3(-5, -5, -1));
        vertices.Add(new Vector3(-2, -5, -1));
        vertices.Add(new Vector3(-1, 3, -1));
        vertices.Add(new Vector3(-1, -1, -1));
        vertices.Add(new Vector3(-1, -3, -1));
        vertices.Add(new Vector3(0, 1, -1));
        vertices.Add(new Vector3(1, 3, -1));
        vertices.Add(new Vector3(1, -1, -1));
        vertices.Add(new Vector3(1, -3, -1));
        vertices.Add(new Vector3(2, -5, -1));
        vertices.Add(new Vector3(5, -5, -1));
        vertices.Add(new Vector3(-5, -5, 1));
        vertices.Add(new Vector3(-2, -5, 1));
        vertices.Add(new Vector3(-1, 3, 1));
        vertices.Add(new Vector3(-1, -1, 1));
        vertices.Add(new Vector3(-1, -3, 1));
        vertices.Add(new Vector3(0, 1, 1));
        vertices.Add(new Vector3(1, 3, 1));
        vertices.Add(new Vector3(1, -1, 1));
        vertices.Add(new Vector3(1, -3, 1));
        vertices.Add(new Vector3(2, -5, 1));
        vertices.Add(new Vector3(5, -5, 1));
    }

    private void addTextureCoordinates()
    {
        // pixel positions written for a 1024 x 1024 texture
        // AddUV scales them to textureSize
        //Front face (y = 1)
        AddUV(32, 400); //0 front v1
        AddUV(170, 400); //1 front v2
        AddUV(216, 32); //2 front v3
        AddUV(216, 216); //3 front v4
        AddUV(216, 308); //4 front v5
        AddUV(262, 124); //5 front v6
        AddUV(308, 32); //6 front v7
        AddUV(308, 216); //7 front v8
        AddUV(308, 308); //8 front v9
        AddUV(354, 400); //9 front v10
        AddUV(492, 400); //10 front v11
        //Back face (y = -1), mirrored so it reads right from underneath
        AddUV(992, 400); //11 back v12
        AddUV(854, 400); //12 back v13
        AddUV(808, 32); //13 back v14
        AddUV(808, 216); //14 back v15
        AddUV(808, 308); //15 back v16
        AddUV(762, 124); //16 back v17
        AddUV(716, 32); //17 back v18
        AddUV(716, 216); //18 back v19
        AddUV(716, 308); //19 back v20
        AddUV(670, 400); //20 back v21
        AddUV(532, 400); //21 back v22
        //Side walls: width = real edge length x 46, height = 2 units x 46 = 92
        AddUV(443, 440); //22 left outer v1
        AddUV(32, 440); //23 left outer v3
        AddUV(32, 532); //24 left outer v14
        AddUV(443, 532); //25 left outer v12
        AddUV(994, 440); //26 top v3
        AddUV(902, 440); //27 top v7
        AddUV(902, 532); //28 top v18
        AddUV(994, 532); //29 top v14
        AddUV(878, 440); //30 right outer v7
        AddUV(467, 440); //31 right outer v11
        AddUV(467, 532); //32 right outer v22
        AddUV(878, 532); //33 right outer v18
        AddUV(170, 572); //34 right leg bottom v11
        AddUV(32, 572); //35 right leg bottom v10
        AddUV(32, 664); //36 right leg bottom v21
        AddUV(170, 664); //37 right leg bottom v22
        AddUV(297, 572); //38 right leg inner v10
        AddUV(194, 572); //39 right leg inner v9
        AddUV(194, 664); //40 right leg inner v20
        AddUV(297, 664); //41 right leg inner v21
        AddUV(413, 572); //42 crossbar underside v9
        AddUV(321, 572); //43 crossbar underside v5
        AddUV(321, 664); //44 crossbar underside v16
        AddUV(413, 664); //45 crossbar underside v20
        AddUV(540, 572); //46 left leg inner v5
        AddUV(437, 572); //47 left leg inner v2
        AddUV(437, 664); //48 left leg inner v13
        AddUV(540, 664); //49 left leg inner v16
        AddUV(702, 572); //50 left leg bottom v2
        AddUV(564, 572); //51 left leg bottom v1
        AddUV(564, 664); //52 left leg bottom v12
        AddUV(702, 664); //53 left leg bottom v13
        AddUV(135, 704); //54 hole left v6
        AddUV(32, 704); //55 hole left v4
        AddUV(32, 796); //56 hole left v15
        AddUV(135, 796); //57 hole left v17
        AddUV(262, 704); //58 hole right v8
        AddUV(159, 704); //59 hole right v6
        AddUV(159, 796); //60 hole right v17
        AddUV(262, 796); //61 hole right v19
        AddUV(378, 704); //62 hole bottom v4
        AddUV(286, 704); //63 hole bottom v8
        AddUV(286, 796); //64 hole bottom v19
        AddUV(378, 796); //65 hole bottom v15
    }

    private void addFaces()
    {
        // AddFace(vertex a, b, c using the diagram numbers, texture index a, b, c)
        //Front faces
        AddFace(1, 4, 3, 0, 3, 2); //1
        AddFace(1, 5, 4, 0, 4, 3); //2
        AddFace(1, 2, 5, 0, 1, 4); //3
        AddFace(3, 6, 7, 2, 5, 6); //4
        AddFace(6, 8, 7, 5, 7, 6); //5
        AddFace(3, 4, 6, 2, 3, 5); //6
        AddFace(4, 5, 8, 3, 4, 7); //7
        AddFace(5, 9, 8, 4, 8, 7); //8
        AddFace(7, 8, 11, 6, 7, 10); //9
        AddFace(8, 9, 11, 7, 8, 10); //10
        AddFace(9, 10, 11, 8, 9, 10); //11

        //Back faces
        AddFace(12, 14, 15, 11, 13, 14); //12
        AddFace(12, 15, 16, 11, 14, 15); //13
        AddFace(12, 16, 13, 11, 15, 12); //14
        AddFace(14, 18, 17, 13, 17, 16); //15
        AddFace(17, 18, 19, 16, 17, 18); //16
        AddFace(14, 17, 15, 13, 16, 14); //17
        AddFace(15, 19, 16, 14, 18, 15); //18
        AddFace(16, 19, 20, 15, 18, 19); //19
        AddFace(18, 22, 19, 17, 21, 18); //20
        AddFace(19, 22, 20, 18, 21, 19); //21
        AddFace(20, 22, 21, 19, 21, 20); //22

        //Left Face
        AddFace(3, 14, 12, 23, 24, 25); //23
        AddFace(1, 3, 12, 22, 23, 25); //24

        //Right Face
        AddFace(7, 11, 18, 30, 31, 33); //25
        AddFace(11, 22, 18, 31, 32, 33); //26

        //Top Face
        AddFace(3, 7, 14, 26, 27, 29); //27
        AddFace(7, 18, 14, 27, 28, 29); //28

        //Bottom Faces
        AddFace(1, 13, 2, 51, 53, 50); //29
        AddFace(1, 12, 13, 51, 52, 53); //30
        AddFace(2, 16, 5, 47, 49, 46); //31
        AddFace(2, 13, 16, 47, 48, 49); //32
        AddFace(5, 20, 9, 43, 45, 42); //33
        AddFace(5, 16, 20, 43, 44, 45); //34
        AddFace(9, 21, 10, 39, 41, 38); //35
        AddFace(9, 20, 21, 39, 40, 41); //36
        AddFace(10, 22, 11, 35, 37, 34); //37
        AddFace(10, 21, 22, 35, 36, 37); //38

        //Inside Top Faces
        AddFace(4, 17, 6, 55, 57, 54); //39
        AddFace(4, 15, 17, 55, 56, 57); //40
        AddFace(8, 6, 17, 58, 59, 60); //41
        AddFace(8, 17, 19, 58, 60, 61); //42

        //Inside Bottom Faces
        AddFace(15, 4, 8, 65, 62, 63); //43
        AddFace(8, 19, 15, 63, 64, 65); //44
    }

    public GameObject CreateUnityGameObject(Material material)
    {
        Mesh mesh = new Mesh();
        GameObject newGO = new GameObject("Model");

        MeshFilter mesh_filter = newGO.AddComponent<MeshFilter>();
        MeshRenderer mesh_renderer = newGO.AddComponent<MeshRenderer>();
        mesh_renderer.material = material;

        List<Vector3> coords = new List<Vector3>();
        List<int> dummy_indices = new List<int>();
        List<Vector2> text_coords = new List<Vector2>();

        for (int i = 0; i < faces.Count; i++)
        {
            coords.Add(vertices[faces[i].x]); dummy_indices.Add(i * 3); text_coords.Add(texture_coordinates[texture_index_list[i].x]);

            coords.Add(vertices[faces[i].y]); dummy_indices.Add(i * 3 + 2); text_coords.Add(texture_coordinates[texture_index_list[i].y]);

            coords.Add(vertices[faces[i].z]); dummy_indices.Add(i * 3 + 1); text_coords.Add(texture_coordinates[texture_index_list[i].z]);
        }

        mesh.vertices = coords.ToArray();
        mesh.triangles = dummy_indices.ToArray();
        mesh.uv = text_coords.ToArray();
        mesh.RecalculateNormals(); // flat normals, one per face, since every triangle has its own 3 vertices
        mesh_filter.mesh = mesh;

        return newGO;
    }
}