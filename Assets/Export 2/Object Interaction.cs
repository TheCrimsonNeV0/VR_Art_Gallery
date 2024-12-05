using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public Transform controller; // Drag your VR controller here in Unity

    private GameObject selectedObject = null; // Object currently selected

    private GameObject heldObject = null; // Duplicate object held in the user's hand

    void Update()
    {
        if (Physics.Raycast(controller.position, controller.forward, out RaycastHit hit, 50f))
        {
            if (hit.collider.CompareTag("Interactable") && heldObject == null)
            {
                Debug.Log("Targeting: " + hit.collider.gameObject.name);

                if (Input.GetButtonDown("Fire1"))
                {
                    CreateDuplicate(hit.collider.gameObject);
                }
            }
        }

        if (Input.GetButtonUp("Fire1") && heldObject != null)
        {
            DestroyDuplicate();
        }
    }

    void CreateDuplicate(GameObject original)
    {
        // Create a duplicate of the original object
        heldObject = Instantiate(original, controller.position, Quaternion.identity);

        heldObject.transform.rotation = Quaternion.Euler(270, 0, 0); // Set it upright (adjust values if needed)

        heldObject.transform.SetParent(controller); // Attach to the controller

        Vector3 originalSize = original.GetComponent<Renderer>().bounds.size;
        float maxDimension = Mathf.Max(originalSize.x, originalSize.y, originalSize.z);
        if (maxDimension > 0.5f) // Adjust this threshold as needed
        {
            float scaleFactor = 0.25f / maxDimension; // Normalize the largest dimension to 1 unit
            heldObject.transform.localScale = original.transform.localScale * scaleFactor;
        }

        heldObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics on the duplicate

        // Optional: Hide the original while interacting
        //original.SetActive(false);
    }

    void DestroyDuplicate()
    {
        if (heldObject != null)
        {
            Destroy(heldObject); // Destroy the duplicate
            heldObject = null;

            // Reactivate the original object
            if (selectedObject != null)
            {
                selectedObject.SetActive(true);
            }
        }
    }
}