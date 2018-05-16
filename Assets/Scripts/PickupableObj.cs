using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PickupableObj : MonoBehaviour, IPickupable {

    Rigidbody rb;
    Color currentColor;
    Shader currentShader;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        
    }

    public void Pickup(Material mat)
    {
        currentColor = mat.color;
        currentShader = mat.shader;
        Color tempColor = mat.color;
        tempColor.a = 0.5f;
        mat.shader = Shader.Find("Transparent/Diffuse");
        mat.color = tempColor;
    }

    public void Release(Material mat)
    {
        mat.shader = currentShader;
        mat.color = currentColor;
    }

    public GameObject ReturnGO()
    {
        return transform.gameObject;
    }

    public void MoveGO(Vector3 pos)
    {
        transform.position = pos;
        AlignWithGroundBelow();
        DropObj();
    }

    public void AlignWithGroundBelow()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit))
        {
            transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
    }

    public void DropObj()
    {
        StartCoroutine(DropObject());
    }

    public IEnumerator DropObject()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        yield return new WaitForSeconds(1f);
        rb.isKinematic = true;
        rb.useGravity = false;
    }
}
