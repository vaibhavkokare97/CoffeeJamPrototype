using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrayBlock : MonoBehaviour
{
    public Dictionary<Vector2Int, GameObject> trayBlocks = new Dictionary<Vector2Int, GameObject>();
    public new Rigidbody rigidbody;
    public new HingeJoint hingeJoint;
    public Color color;

    private void Start()
    {
        rigidbody = gameObject.AddComponent<Rigidbody>();
        hingeJoint = gameObject.AddComponent<HingeJoint>();
        rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        rigidbody.linearDamping = 5f;
        rigidbody.isKinematic = true;
    }

    public void Initiate()
    {
        foreach (var item in trayBlocks)
        {
            item.Value.GetComponent<Renderer>().materials[0].color = color;
            item.Value.GetComponent<Renderer>().materials[1].color = color;
        }
    }

    public void SnapToClosest()
    {
        float posX = trayBlocks.First().Value.transform.position.x;
        float posZ = trayBlocks.First().Value.transform.position.z;
        float deltaX = posX - Mathf.RoundToInt(posX);
        float deltaZ = posZ - Mathf.RoundToInt(posZ);
        transform.position -= new Vector3(deltaX, 0, deltaZ);
    }
}
