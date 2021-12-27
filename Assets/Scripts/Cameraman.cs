using UnityEngine;

public class Cameraman : MonoBehaviour
{
    [SerializeField] 
    private GameObject filmObject;

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPosition = filmObject.transform.position;
        cameraPosition.z = -1;
        
        transform.position = cameraPosition;
        transform.rotation = filmObject.transform.rotation;
    }
}
