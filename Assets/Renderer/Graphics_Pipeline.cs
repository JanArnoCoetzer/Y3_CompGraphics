using UnityEngine;

public class Graphics_Pipeline : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Model model = new Model();
        model.add_Vertices();
        model.add_Faces();
        model.CreateUnityGameObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
