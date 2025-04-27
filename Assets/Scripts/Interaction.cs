using UnityEngine;

public class Interaction : MonoBehaviour
{
    public static TrayBlock activeTrayBlock = null;
    private static GameObject interactPointObj;

    private void Awake()
    {
        interactPointObj = new GameObject();
        Rigidbody rb = interactPointObj.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        rb.mass = 100000f;
        rb.linearDamping = 10000f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && activeTrayBlock == null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("tray")))
            {
                activeTrayBlock = hit.transform.GetComponentInParent<TrayBlock>();
                activeTrayBlock.rigidbody.isKinematic = false;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (activeTrayBlock != null)
            {
                activeTrayBlock.rigidbody.isKinematic = true;
                activeTrayBlock.hingeJoint.connectedBody = null;
                activeTrayBlock.SnapToClosest();
                activeTrayBlock = null;
            }
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && activeTrayBlock != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("floor", "tray")))
            {

                Vector3 targetPosition = hit.point;
                Vector3 currentPosition = activeTrayBlock.rigidbody.worldCenterOfMass;

                interactPointObj.transform.position = hit.point;
                activeTrayBlock.hingeJoint.connectedBody = interactPointObj.GetComponent<Rigidbody>();
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (activeTrayBlock != null)
        {
            Gizmos.DrawWireSphere(activeTrayBlock.rigidbody.worldCenterOfMass, 0.2f);
        }
    }
}
