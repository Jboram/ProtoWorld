using UnityEngine;

namespace ProtoWorld
{
    public class Billboard : MonoBehaviour
    {
        private Transform camera;
        void Start()
        {
            camera = Camera.main.transform;
        }
        // Update is called once per frame
        void LateUpdate()
        {
            transform.LookAt(transform.position + camera.forward);
        }
    }
}
