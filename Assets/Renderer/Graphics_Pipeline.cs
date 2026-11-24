using System.Collections.Generic;
using UnityEngine;

public class Graphics_Pipeline : MonoBehaviour
{
    // drag your material (with the texture as its Base Map) in here in the Inspector
    public Material modelMaterial;

    // size of texture image: 1024, 2048, 4096 
    public int textureSize = 1024;

    float rotationAngle = 30f;
    Vector3 rotationAxis = new Vector3(0.935674292f, 0.249513145f, 0.249513145f);
    Vector3 scaleBy = new Vector3(3, 1, 4);
    Vector3 translateBy = new Vector3(-5, 4, 5);

    Vector3 cameraPosition = new Vector3(17, 7, 54);
    Vector3 cameraTarget = new Vector3(4, 3, 4);
    Vector3 cameraUp = new Vector3(0.30656967f, 0.245255736f, 0.919709009f);

    float fieldOfView = 60f;
    float aspectRatio = 16f / 9f;
    float nearPlane = 0.3f;
    float farPlane = 1000f;
    // -------------------------------------------------------------

    void Start()
    {
        Model myModel = new Model(textureSize);
        myModel.CreateUnityGameObject(modelMaterial);

        //Vertices
        List<Vector4> verts = Homog(myModel.vertices);
        Display("Vertices", verts);

        //Transformation - Rotation
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(rotationAngle, rotationAxis.normalized), Vector3.one);
        Display("Rotation Matrix", rotationMatrix);

        List<Vector4> imageAfterRotation = MatrixTransform(rotationMatrix, verts);
        Display("Image After Rotation", imageAfterRotation);

        //Transformation - Scale
        Matrix4x4 scaleMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scaleBy);
        Display("Scale Matrix", scaleMatrix);

        List<Vector4> imageAfterScale = MatrixTransform(scaleMatrix, imageAfterRotation);
        Display("Image After Scale", imageAfterScale);

        //Transformation - Translation
        Matrix4x4 translationMatrix = Matrix4x4.TRS(translateBy, Quaternion.identity, Vector3.one);
        Display("Translation Matrix", translationMatrix);

        List<Vector4> imageAfterTranslation = MatrixTransform(translationMatrix, imageAfterScale);
        Display("Image After Translation", imageAfterTranslation);

        //Single Matrix of Transformations 
        Matrix4x4 singleMoT = translationMatrix * scaleMatrix * rotationMatrix;
        Display("Single Matrix of Transformations", singleMoT);

        List<Vector4> imageAfterSingleMoT = MatrixTransform(singleMoT, verts);
        Display("Image After Single MoT (should match Image After Translation)", imageAfterSingleMoT);

        //View Matrix
        Matrix4x4 viewMatrix = Matrix4x4.LookAt(cameraPosition, cameraTarget, cameraUp.normalized).inverse;
        Display("View Matrix", viewMatrix);

        List<Vector4> imageAfterView = MatrixTransform(viewMatrix, imageAfterTranslation);
        Display("Image After View", imageAfterView);

        //Projection Matrix
        Matrix4x4 projectionMatrix = Matrix4x4.Perspective(fieldOfView, aspectRatio, nearPlane, farPlane);
        Display("Projection Matrix", projectionMatrix);

        List<Vector4> imageAfterProjection = MatrixTransform(projectionMatrix, imageAfterView);
        Display("Image After Projection", imageAfterProjection);

        //Single Matrix for Everything
        Matrix4x4 everythingMatrix = projectionMatrix * viewMatrix * singleMoT;
        Display("Single Matrix for Everything", everythingMatrix);

        List<Vector4> finalImage = MatrixTransform(everythingMatrix, verts);
        Display("Final Image (should match Image After Projection)", finalImage);

        //Perspective divide 
        List<Vector2> projected = PerspectiveDivide(finalImage);
        Display("Projected 2D Points", projected);

        //Line Clipping tests
        TestClip(new Vector2(0.2f, 0.3f), new Vector2(0.5f, -0.4f));  // fully inside 
        TestClip(new Vector2(2f, 2f), new Vector2(3f, 1.5f));         // fully outside one side 
        TestClip(new Vector2(-2f, 0f), new Vector2(2f, 0f));          // horizontal
        TestClip(new Vector2(0f, 3f), new Vector2(0f, -3f));          // vertical 
        TestClip(new Vector2(2.4f, 2f), new Vector2(-0.9f, -1.5f));   // diagonal
        TestClip(new Vector2(-1.5f, 1.8f), new Vector2(1.8f, 1.5f));  // passes outside the corner 

        //Clip and draw every edge of the model
        int lineCount = 0, pixelCount = 0;
        foreach (Vector3Int face in myModel.faces)
        {
            foreach (Vector2Int edge in new[] { new Vector2Int(face.x, face.y), new Vector2Int(face.y, face.z), new Vector2Int(face.z, face.x) })
            {
                Vector2 start = projected[edge.x];
                Vector2 end = projected[edge.y];

                if (LineClip(ref start, ref end))
                {
                    List<Vector2Int> pixels = Bresenham(ToPixel(start, 1024, 1024), ToPixel(end, 1024, 1024));
                    lineCount++;
                    pixelCount += pixels.Count;
                }
            }
        }
        print($"Drew {lineCount} clipped edges using {pixelCount} pixels");

        //test in each direction
        print("Bresenham (0,0) to (6,2): " + string.Join(" ", Bresenham(new Vector2Int(0, 0), new Vector2Int(6, 2))));
        print("Bresenham (0,0) to (2,6): " + string.Join(" ", Bresenham(new Vector2Int(0, 0), new Vector2Int(2, 6))));
        print("Bresenham (6,2) to (0,0): " + string.Join(" ", Bresenham(new Vector2Int(6, 2), new Vector2Int(0, 0))));
        print("Bresenham (0,0) to (6,-2): " + string.Join(" ", Bresenham(new Vector2Int(0, 0), new Vector2Int(6, -2))));
    }

    void Update()
    {

    }

    // ---------------- Matrix helpers ----------------

    private List<Vector4> Homog(List<Vector3> vertices)
    {
        List<Vector4> result = new List<Vector4>();
        foreach (Vector3 v in vertices)
            result.Add(new Vector4(v.x, v.y, v.z, 1f));
        return result;
    }

    private List<Vector4> MatrixTransform(Matrix4x4 matrix, List<Vector4> verts)
    {
        List<Vector4> hold = new List<Vector4>();
        foreach (Vector4 v in verts)
            hold.Add(matrix * v);
        return hold;
    }

    private List<Vector2> PerspectiveDivide(List<Vector4> verts)
    {
        // Unity's projection matrix puts -z into w, so dividing by w does x / -z and y / -z
        List<Vector2> hold = new List<Vector2>();
        foreach (Vector4 v in verts)
            hold.Add(new Vector2(v.x / v.w, v.y / v.w));
        return hold;
    }

    private void Display(string name, Matrix4x4 matrix)
    {
        print($"===== {name} =====");
        for (int i = 0; i < 4; i++)
            print(matrix.GetRow(i));
    }

    private void Display(string name, List<Vector4> verts)
    {
        print($"===== {name} =====");
        foreach (Vector4 v in verts)
            print(v);
    }

    private void Display(string name, List<Vector2> points)
    {
        print($"===== {name} =====");
        foreach (Vector2 p in points)
            print($"{p.x:F4}, {p.y:F4}");
    }

    // ---------------- Line Clipping (Cohen-Sutherland) ----------------

    private void TestClip(Vector2 start, Vector2 end)
    {
        string before = "Line " + start + " to " + end;

        if (LineClip(ref start, ref end))
            print(before + " accepted, clipped to " + start + " to " + end);
        else
            print(before + " rejected");
    }

    public bool LineClip(ref Vector2 start, ref Vector2 end)
    {
        OutCode startO = new OutCode(start);
        OutCode endO = new OutCode(end);
        OutCode inViewport = new OutCode();

        // both points inside 
        if ((startO + endO) == inViewport)
            return true;

        // both points outside the same edge
        if ((startO * endO) != inViewport)
            return false;

        // start is the point outside the viewport
        if (startO == inViewport)
            return LineClip(ref end, ref start);

        // move start onto the edge
        if (startO.up)
        {
            float n = (end.x - start.x) / (end.y - start.y); // 1/m
            start = new Vector2(start.x + n * (1 - start.y), 1);
            return LineClip(ref start, ref end);
        }

        if (startO.down)
        {
            float n = (end.x - start.x) / (end.y - start.y); // 1/m
            start = new Vector2(start.x + n * (-1 - start.y), -1);
            return LineClip(ref start, ref end);
        }

        if (startO.left)
        {
            float m = (end.y - start.y) / (end.x - start.x);
            start = new Vector2(-1, start.y + m * (-1 - start.x));
            return LineClip(ref start, ref end);
        }

        if (startO.right)
        {
            float m = (end.y - start.y) / (end.x - start.x);
            start = new Vector2(1, start.y + m * (1 - start.x));
            return LineClip(ref start, ref end);
        }

        return false;
    }

    // ---------------- Rasterisation ----------------

    // viewportto pixel coordinates
    private Vector2Int ToPixel(Vector2 v, int width, int height)
    {
        int x = Mathf.RoundToInt((v.x + 1) / 2 * (width - 1));
        int y = Mathf.RoundToInt((1 - v.y) / 2 * (height - 1));
        return new Vector2Int(x, y);
    }

    // Bresenham for any direction
    public List<Vector2Int> Bresenham(Vector2Int start, Vector2Int end)
    {
        int dx = end.x - start.x;
        int dy = end.y - start.y;

        // going left 
        if (dx < 0)
        {
            List<Vector2Int> reversed = Bresenham(end, start);
            reversed.Reverse();
            return reversed;
        }

        // going down 
        if (dy < 0)
            return NegateY(Bresenham(NegateY(start), NegateY(end)));

        // steep (slope > 1) 
        if (dy > dx)
            return SwapXY(Bresenham(SwapXY(start), SwapXY(end)));

        // slope between 0 and 1
        List<Vector2Int> result = new List<Vector2Int>();
        int twoDy = 2 * dy;
        int twoDyMinusDx = 2 * (dy - dx);
        int p = 2 * dy - dx;
        int y = start.y;

        for (int x = start.x; x <= end.x; x++)
        {
            result.Add(new Vector2Int(x, y));

            if (p < 0)
                p += twoDy;
            else
            {
                y++;
                p += twoDyMinusDx;
            }
        }

        return result;
    }

    private Vector2Int NegateY(Vector2Int v) { return new Vector2Int(v.x, -v.y); }

    private List<Vector2Int> NegateY(List<Vector2Int> points)
    {
        List<Vector2Int> hold = new List<Vector2Int>();
        foreach (Vector2Int v in points) hold.Add(NegateY(v));
        return hold;
    }

    private Vector2Int SwapXY(Vector2Int v) { return new Vector2Int(v.y, v.x); }

    private List<Vector2Int> SwapXY(List<Vector2Int> points)
    {
        List<Vector2Int> hold = new List<Vector2Int>();
        foreach (Vector2Int v in points) hold.Add(SwapXY(v));
        return hold;
    }
}