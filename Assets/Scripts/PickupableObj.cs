using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickupableObj : MonoBehaviour, IPickupable {

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

	public GameObject ReturnGO()
    {
        return transform.gameObject;
    }

    public void MoveGO(Vector3 pos)
    {
        transform.position = pos;
        transform.position += new Vector3(0, GetComponent<Collider>().bounds.size.y / 2, 0);
    }

    public void AlignWithGroundBelow()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Aligning");
            transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
    }
}
