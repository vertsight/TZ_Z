using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ItemInteraction : MonoBehaviour
{
    public Transform handPosition;
    private GameObject heldItem;
    [Inject] private UIManager uiManager;
    [Inject] private ResourcesManager resourcesManager;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 2f))
            {
                if (hit.collider.CompareTag("Item"))
                {
                    PickUpItem(hit.collider.gameObject);
                }
            }
        }
    }

    private void PickUpItem(GameObject item)
    {
        if(heldItem != null) return;

        heldItem = item;
        heldItem.GetComponent<Rigidbody>().isKinematic = true;
        heldItem.GetComponent<Collider>().isTrigger = true;
        heldItem.GetComponent<MeshRenderer>().material = resourcesManager.PickedItemMaterial;
        item.transform.SetParent(handPosition);
        item.transform.localPosition = Vector3.zero;
        
        uiManager.ShowDropButton();
    }

    public void DropItem()
    {
        if (heldItem == null) return;
        
        heldItem.GetComponent<Rigidbody>().isKinematic = false;
        heldItem.transform.SetParent(null);
        heldItem.GetComponent<Collider>().isTrigger = false;
        heldItem.GetComponent<MeshRenderer>().material = resourcesManager.DefaultItemMaterial;
        heldItem.GetComponent<Rigidbody>().AddForce((transform.forward + Vector3.up) * 4f, ForceMode.Impulse);
        heldItem = null;

        uiManager.HideDropButton();
    }
}
