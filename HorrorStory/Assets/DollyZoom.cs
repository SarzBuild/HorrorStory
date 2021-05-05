using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollyZoom : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float dollySpeed = 5.0f;
    private float initialFrustrumHeight; 

    private Camera camera;


    private void Awake()
    {
        Initialize();
    }

    private void  Initialize() {
        camera = GetComponent<Camera>();
        Debug.Assert(camera != null);
        float distanceFromTarget = Vector3.Distance(transform.position, target.position);
        initialFrustrumHeight = ComputeFrustumHeight(distanceFromTarget);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Vertical") * Vector3.forward * Time.deltaTime * dollySpeed);
        float currentDistance = Vector3.Distance(transform.position, target.position);
        camera.fieldOfView = ComputeFieldOfView(initialFrustrumHeight, currentDistance);
    }

    private float ComputeFrustumHeight(float distance)
    {
        return (2.0f * distance * Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad));
    }

    private float ComputeFieldOfView(float height, float distance)
    {
        return (2.0f * Mathf.Atan(height * 0.5f / distance) * Mathf.Rad2Deg);
    }
}
