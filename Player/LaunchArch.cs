using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaunchArch : MonoBehaviour
{

    [SerializeField]
    private GameObject explosionIndicator;
    [SerializeField]
    private BooleanVariable showArch;
    [SerializeField]
    private FloatVariable throwingForce;
    [SerializeField]
    private LayerMask layerMask = -1;

    private GameObject indicatorInstance = null;
    public int maxPositionCount = 1024;

    private LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        indicatorInstance = Instantiate(explosionIndicator, transform.position, Quaternion.identity);
        indicatorInstance.SetActive(false);
    }

    private void Update()
    {
        if (showArch.Value == false)
        {
            lr.positionCount = 0;
            if (indicatorInstance.activeInHierarchy) indicatorInstance.SetActive(false);
        }
        else
        {
            DrawArch();
        }
    }

    private void DrawArch()
    {
        // Initialize Values
        float velocity = throwingForce.Value;
        Vector3 currentVelocity = transform.forward * velocity;
        Vector3 currentPosition = transform.position;

        lr.positionCount = maxPositionCount;

        int index = 0;
        bool keepDrawingArch = true;

        while (index < maxPositionCount || keepDrawingArch == false)
        {
            // Draw the current line renderer position
            lr.SetPosition(index, currentPosition);

            // Calculate the next position
            float time = Time.deltaTime;
            Vector3 nextVelocity = currentVelocity + Physics.gravity * time;
            Vector3 nextPosition = currentPosition + nextVelocity * time;

            // Check if it hits something and start
            RaycastHit hit;
            Vector3 nextPositionDirection = nextPosition - currentPosition;
            if (Physics.Raycast(currentPosition, nextPositionDirection, out hit, nextPositionDirection.magnitude, layerMask))
            {
                lr.positionCount = index + 2;
                lr.SetPosition(index + 1, hit.point);
                if (indicatorInstance != null) 
                {
                    indicatorInstance.SetActive(true);
                    indicatorInstance.transform.forward = hit.normal;
                    indicatorInstance.transform.position = hit.point - transform.forward * 0.25f;
                };
                break;
            }
            else
            {
                indicatorInstance.SetActive(false);
            }
            currentPosition = nextPosition;
            currentVelocity = nextVelocity;
            index++;
        }
    }
}
