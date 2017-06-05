using UnityEngine;

public class PlayerController : MonoBehaviour, ICanPickup {
    Transform lastRayHit;
    Camera cam;
    public RaycastHit hit;
    public GameObject sphere;
    GameObject pickedUpObj;
    public GameObject objToShowTransparent;
    public bool carrying = false;
    private LayerMask currObjLayermask;
    private Shader transparent = Shader.Find("Transparent/Diffuse");

    public float smooth;

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

    void Update()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        //Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Vector3 rayOrigin = cam.transform.position;

        //Ray ray = cam.ScreenPointToRay(new Vector3(x, y));
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
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
                CheckCancel();
                if (hit.transform.gameObject.tag == "PlacableSurface")
                {
                    CheckRelease();
                }
                Carry(hit.point);
            }

            if (hit.transform.GetComponent<IInteractable>() != null)
            {
                Debug.Log("Pressable");
                hit.transform.GetComponent<IInteractable>().CheckInput();
            }
            if (hit.transform.GetComponent<IHighlightable>() == null)
            {
                if (lastRayHit != null)
                {
                    lastRayHit.GetComponent<IHighlightable>().DeHighlight();
                }
            }
            else if (hit.transform.GetComponent<IHighlightable>() != null)
            {
                lastRayHit = hit.transform;
                hit.transform.GetComponent<IHighlightable>().Highlight();
            }
            if (hit.transform.GetComponent<IPickupable>() != null)
            {
                if (carrying == false)
                {
                    CheckPickup(hit.transform.GetComponent<IPickupable>().ReturnGO());
                }
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
        pickedUpObj = GO;
        pickedUpObj.GetComponent<IPickupable>().Pickup(pickedUpObj.GetComponent<IHighlightable>().ReturnMatGO());
        pickedUpObj.GetComponent<Rigidbody>().isKinematic = true;
        objToShowTransparent = Instantiate(GO, pickedUpObj.transform.position, pickedUpObj.transform.rotation);
        currObjLayermask = pickedUpObj.layer;
        pickedUpObj.layer = LayerMask.NameToLayer("Ignore Raycast");
        objToShowTransparent.layer = LayerMask.NameToLayer("Ignore Raycast");
        
        if (sphere != null)
        {
            Destroy(sphere);
            sphere = null;
        }
        carrying = true;
    }

    public void CheckPickup(GameObject GO)
    {
        //Debug.Log("Checking Pickup");
        if (Input.GetButtonDown("Fire1"))
        {
            Pickup(GO);
        }
    }

    public void Carry(Vector3 hit)
    {
        Vector3 yOffset = new Vector3(0, objToShowTransparent.GetComponent<Collider>().bounds.max.y / 2 - objToShowTransparent.GetComponent<Collider>().bounds.min.y / 2, 0);
        objToShowTransparent.transform.position = Vector3.Lerp(objToShowTransparent.transform.position, hit + yOffset, Time.deltaTime * smooth);
        //objToShowTransparent.transform.position += new Vector3(0, objToShowTransparent.GetComponent<Collider>().bounds.max.y / 2 - objToShowTransparent.GetComponent<Collider>().bounds.min.y / 2, 0);
        objToShowTransparent.GetComponent<IPickupable>().AlignWithGroundBelow();
    }

    public void Release()
    {
        carrying = false;
        pickedUpObj.GetComponent<IPickupable>().Release(pickedUpObj.GetComponent<IHighlightable>().ReturnMatGO());
        pickedUpObj.layer = currObjLayermask;
        //pickedUpObj.GetComponent<IPickupable>().AlignWithGroundBelow();
        pickedUpObj.GetComponent<IPickupable>().MoveGO(objToShowTransparent.transform.position);
        pickedUpObj.GetComponent<IPickupable>().DropObj();
        Destroy(objToShowTransparent);
    }

    public void CheckRelease()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Release();
        }
    }

    public void CancelPickup()
    {
        carrying = false;
        Destroy(objToShowTransparent);
    }

    public void CheckCancel()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            CancelPickup();
        }
    }

    public void MakeSphere()
    {
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.GetComponent<MeshRenderer>().material.shader = Shader.Find("Transparent/Diffuse");
        sphere.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0, 0.5f);
        sphere.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
        sphere.GetComponent<Collider>().enabled = false;
    }
}
