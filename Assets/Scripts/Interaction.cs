using UnityEngine;

public class Interaction : MonoBehaviour
{
    public static Rigidbody activeTrayBlock = null;
    [SerializeField] private float forceFactor = 10f;


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && activeTrayBlock == null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("tray")))
            {
                Debug.Log("x");
                activeTrayBlock = hit.transform.GetComponentInParent<Rigidbody>();
                activeTrayBlock.isKinematic = false;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (activeTrayBlock != null)
            {
                activeTrayBlock.isKinematic = true;
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

                /*
                Vector3 direction = (hit.point - activeTrayBlock.worldCenterOfMass);
                float distance = direction.magnitude;
                activeTrayBlock.AddForce(direction.normalized * distance * force, ForceMode.Force);*/

                // Calculate the direction towards the mouse position
                Vector3 directionToMouse = hit.point - activeTrayBlock.worldCenterOfMass;

                // Get the distance to the mouse position
                float distance = directionToMouse.magnitude;

                // Scale the magnet strength based on the distance
                float strength = Mathf.Lerp(0, 2000f, distance / 20f);

                // Apply the force to move the object towards the mouse
                Vector3 force = directionToMouse.normalized * strength;

                // Apply the force using AddForce
                activeTrayBlock.AddForce(force, ForceMode.Force);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (activeTrayBlock != null)
        {
            Gizmos.DrawWireSphere(activeTrayBlock.worldCenterOfMass, 0.2f);
        }
    }
}
