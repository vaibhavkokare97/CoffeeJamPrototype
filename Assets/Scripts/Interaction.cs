using UnityEngine;

public class Interaction : MonoBehaviour
{
    public static TrayBlock activeTrayBlock = null;

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
                Vector3 directionToMouse = hit.point - activeTrayBlock.rigidbody.worldCenterOfMass;
                float distance = directionToMouse.magnitude;
                float strength = Mathf.Lerp(0, 2000f, distance / 20f);
                Vector3 force = directionToMouse.normalized * strength;
                activeTrayBlock.rigidbody.AddForce(force, ForceMode.Force);
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
