using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICanPickup {
    Transform lastRayHit;
    Camera cam;
    public RaycastHit hit;
    public GameObject sphere;
    GameObject pickedUpObj;
    public GameObject objToShowTransparent;
    public bool carrying = false;
    bool canPickup = true;
    public float smooth;

	// Use this for initialization
	void Start () {
        if(GetComponentInChildren<Camera>() != null)
        {
            cam = GetComponentInChildren<Camera>();
        }
        else if(GetComponent<Camera>() != null)
        {
            cam = GetComponent<Camera>();
        }
        else
        {
            Debug.Log("NO CAM ATTACHED");
        }


    }

    // Update is called once per frame
    void Update()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        
        Ray ray = cam.ScreenPointToRay(new Vector3(x, y));
        Debug.DrawRay(ray.origin, ray.direction * 1000, new Color(1f, 0.922f, 0.016f, 1f));

        if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, 50))
        {

            if (carrying == false)
            {
                if (sphere == null)
                {
                    MakeSphere();
                }
                sphere.transform.position = hit.point;
            }
            else if (carrying == true)
            {
                if(hit.transform.gameObject.tag == "PlacableSurface")
                {
                    CheckRelease();
                }
                Carry(hit.point);
            }

            if (hit.transform.GetComponent<IHighlightable>() == null)
            {
                if (lastRayHit != null)
                {
                    lastRayHit.GetComponent<IHighlightable>().DeHighlight();
                }
                return;
            }
            else if (hit.transform.GetComponent<IHighlightable>() != null)
            {
                lastRayHit = hit.transform;
                hit.transform.GetComponent<IHighlightable>().Highlight();
            }

            if (hit.transform.GetComponent<IPickupable>() == null)
            {
                return;
            }
            else if (hit.transform.GetComponent<IPickupable>() != null)
            {
                Debug.Log("Pickupable");
                CheckPickup(hit.transform.GetComponent<IPickupable>().ReturnGO());
            }

            

            if (hit.transform.GetComponent<IInteractable>() == null)
            {
                return;
            }
            else if (hit.transform.GetComponent<IInteractable>() != null)
            {

                hit.transform.GetComponent<IInteractable>().CheckInput();
            }

        }
        else
        {
            if (lastRayHit != null)
            {
                lastRayHit.GetComponent<IHighlightable>().DeHighlight();
            }
        }

        

    }

    public void Pickup(GameObject GO)
    {
        Debug.Log("Picked up");
        pickedUpObj = GO;
        //pickedUpObj.GetComponent<Rigidbody>().isKinematic = true;
        objToShowTransparent = Instantiate(GO);
        objToShowTransparent.layer = LayerMask.NameToLayer("Ignore Raycast");
        Color tempColor = objToShowTransparent.GetComponent<Renderer>().material.color;
        tempColor.a = 0.5f;
        objToShowTransparent.GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");
        objToShowTransparent.GetComponent<Renderer>().material.color = tempColor;
        //objToShowTransparent.GetComponent<Collider>().enabled = false;
        if (sphere != null)
        {
            Destroy(sphere);
            sphere = null;
        }
        carrying = true;
    }

    public void CheckPickup(GameObject GO)
    {
        Debug.Log("Checking Pickup");
        if (Input.GetButtonDown("Fire1"))
        {
            Pickup(GO);
        }
    }

    public void Carry(Vector3 hit)
    {
        //objToShowTransparent.GetComponent<Rigidbody>().AddForce(hit * Time.deltaTime * smooth);
        objToShowTransparent.transform.position = Vector3.Lerp(objToShowTransparent.transform.position, hit, Time.deltaTime * smooth);
        objToShowTransparent.GetComponent<IPickupable>().AlignWithGroundBelow();
        //objToShowTransparent.GetComponent<Rigidbody>().MovePosition(hit);
    }

    public void Release()
    {
        carrying = false;
        //pickedUpObj.GetComponent<Rigidbody>().isKinematic = false;
        pickedUpObj.GetComponent<IPickupable>().MoveGO(objToShowTransparent.transform.position);
        pickedUpObj.GetComponent<IPickupable>().AlignWithGroundBelow();
        Destroy(objToShowTransparent);
        objToShowTransparent = null;
    }

    public void CheckRelease()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Release();
        }
    }

    public void MakeSphere()
    {
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //Instantiate(sphere);
        sphere.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 1.0f);
        sphere.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
        sphere.GetComponent<Collider>().enabled = false;
    }
}
