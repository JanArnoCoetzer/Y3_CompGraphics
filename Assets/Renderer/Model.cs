using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
    private List<Vector3> vertices = new List<Vector3>();
    private List<Vector3Int> faces = new List<Vector3Int>();

    public void add_Vertices() 
    {
        vertices.Add(Vector3.zero);//index starts at 1

        vertices.Add(new Vector3(-5, 1, -5));  
        vertices.Add(new Vector3(-2, 1, -5)); 
        vertices.Add(new Vector3(-1, 1, 3));   
        vertices.Add(new Vector3(-1, 1, -1));  
        vertices.Add(new Vector3(-1, 1, -3));  
        vertices.Add(new Vector3(0, 1, 1));    
        vertices.Add(new Vector3(1, 1, 3));    
        vertices.Add(new Vector3(1, 1, -1));   
        vertices.Add(new Vector3(1, 1, -3));   
        vertices.Add(new Vector3(2, 1, -5));  
        vertices.Add(new Vector3(5, 1, -5));   
        
        vertices.Add(new Vector3(-5, -1, -5)); 
        vertices.Add(new Vector3(-2, -1, -5));
        vertices.Add(new Vector3(-1, -1, 3));  
        vertices.Add(new Vector3(-1, -1, -1)); 
        vertices.Add(new Vector3(-1, -1, -3)); 
        vertices.Add(new Vector3(0, -1, 1));   
        vertices.Add(new Vector3(1, -1, 3));   
        vertices.Add(new Vector3(1, -1, -1));  
        vertices.Add(new Vector3(1, -1, -3));  
        vertices.Add(new Vector3(2, -1, -5));  
        vertices.Add(new Vector3(5, -1, -5));  
    }
    public void add_Faces() 
    {
        faces.Add(Vector3Int.zero);//index starts at 1
        faces.Add(new Vector3Int(1, 4, 3));
        faces.Add(new Vector3Int(1, 5, 4));
        faces.Add(new Vector3Int(1, 2, 5));
        faces.Add(new Vector3Int(3, 6, 7));
        faces.Add(new Vector3Int(6, 8, 7));
        faces.Add(new Vector3Int(3, 4, 6));
        faces.Add(new Vector3Int(4, 5, 8));
        faces.Add(new Vector3Int(5, 9, 8));
        faces.Add(new Vector3Int(7, 8, 11));
        faces.Add(new Vector3Int(8, 9, 11));
        faces.Add(new Vector3Int(9, 10, 11));

        faces.Add(new Vector3Int(12, 14, 15));
        faces.Add(new Vector3Int(12, 15, 16));
        faces.Add(new Vector3Int(12, 16, 13));
        faces.Add(new Vector3Int(14, 18, 17));
        faces.Add(new Vector3Int(17, 18, 19));
        faces.Add(new Vector3Int(14, 17, 15));
        faces.Add(new Vector3Int(15, 19, 16));
        faces.Add(new Vector3Int(16, 19, 20));
        faces.Add(new Vector3Int(18, 22, 19));
        faces.Add(new Vector3Int(19, 22, 20));
        faces.Add(new Vector3Int(20, 22, 21));

        //left face
        faces.Add(new Vector3Int(3, 14, 12));
        faces.Add(new Vector3Int(1, 3, 12));

        //right face
        faces.Add(new Vector3Int(7, 11, 18));
        faces.Add(new Vector3Int(11,22,18));

        //Top Face
        faces.Add(new Vector3Int(3, 7, 14));
        faces.Add(new Vector3Int(7, 18, 14));

        //Bottom Faces
        faces.Add(new Vector3Int(1,13,2));
        faces.Add(new Vector3Int(1, 12, 13));
        faces.Add(new Vector3Int(2, 16, 5));
        faces.Add(new Vector3Int(2, 13, 16));
        faces.Add(new Vector3Int(5, 20, 9));
        faces.Add(new Vector3Int(5, 16, 20));
        faces.Add(new Vector3Int(9, 21, 10));
        faces.Add(new Vector3Int(9, 20, 21));
        faces.Add(new Vector3Int(10, 22, 11));
        faces.Add(new Vector3Int(10, 21, 22));

        faces.Add(new Vector3Int(4, 17, 7));
        faces.Add(new Vector3Int(4, 15, 17));
        faces.Add(new Vector3Int(7, 18, 8));
        //faces.Add(new Vector3Int(7, 17, 18));

   
    }


    public GameObject CreateUnityGameObject()
    {

        Mesh mesh = new Mesh();
        GameObject newGO = new GameObject();

        MeshFilter mesh_filter = newGO.AddComponent<MeshFilter>();
        MeshRenderer mesh_renderer = newGO.AddComponent<MeshRenderer>();

        List<Vector3> coords = new List<Vector3>();
        List<int> dummy_indices = new List<int>();
        /*List<Vector2> text_coords = new List<Vector2>();
        List<Vector3> normalz = new List<Vector3>();*/
        //fghfg
        for (int i = 0; i < faces.Count; i++)
        {
            Debug.Log(i);
            //Vector3 normal_for_face = normals[i];

            //normal_for_face = new Vector3(normal_for_face.x, normal_for_face.y, -normal_for_face.z);

            coords.Add(vertices[faces[i].x]); dummy_indices.Add(i * 3); //text_coords.Add(texture_coordinates[texture_index_list[i].x]); normalz.Add(normal_for_face);

            coords.Add(vertices[faces[i].y]); dummy_indices.Add(i * 3 + 2); //text_coords.Add(texture_coordinates[texture_index_list[i].y]); normalz.Add(normal_for_face);

            coords.Add(vertices[faces[i].z]); dummy_indices.Add(i * 3 + 1); //text_coords.Add(texture_coordinates[texture_index_list[i].z]); normalz.Add(normal_for_face);
        }

        mesh.vertices = coords.ToArray();
        mesh.triangles = dummy_indices.ToArray();
        /*mesh.uv = text_coords.ToArray();
        mesh.normals = normalz.ToArray();*/
        mesh_filter.mesh = mesh;

        return newGO;
    }
}
