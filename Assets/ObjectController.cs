using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public Transform holdRock;
    public LayerMask pickUpMask;
    public Vector3 Direction {get; set;}
    public KeyCode pickupKey = KeyCode.E;
    public KeyCode throwKey = KeyCode.Space;
    public float throwForce = 10f;
    public float maxThrowDuration = 1f;
    private float throwDuration;
    private GameObject itemHolding;
    // Start is called before the first frame update
 void Update()
    {
        if (Input.GetKeyDown(pickupKey)) {
            if (itemHolding) {
                //Drop the item
                DropItem();
            } else {
                PickUpItem();
            }
        }

        if (Input.GetKey(throwKey) && itemHolding) {
            throwDuration += Time.deltaTime;
            throwDuration = Mathf.Clamp(throwDuration, 0f, maxThrowDuration);
        }

        if (Input.GetKeyUp(throwKey) && itemHolding) {
            StartCoroutine(ThrowItem(itemHolding));
            itemHolding = null; 
        }
    }

    void PickUpItem()
    {
        Collider2D pickUpItem = Physics2D.OverlapCircle(transform.position + Direction, .4f, pickUpMask);
        if (pickUpItem)
        {   
            itemHolding = pickUpItem.gameObject;
            itemHolding.transform.position = holdRock.position;
            itemHolding.transform.parent = transform;
            
            if (itemHolding.GetComponent<Rigidbody2D>())
            {
                itemHolding.GetComponent<Rigidbody2D>().simulated = false;
            }
            itemHolding.GetComponent<SpriteRenderer>().sortingOrder = 1; 
        }
    }

    void DropItem() {
        itemHolding.transform.position = transform.position + Direction * 0.5f;
        itemHolding.transform.parent = null;
        itemHolding.GetComponent<SpriteRenderer>().sortingOrder = -1; 
        
        if (itemHolding.GetComponent<Rigidbody2D>()) {
            itemHolding.GetComponent<Rigidbody2D>().simulated = true;
        }
        itemHolding = null; 
    }

    IEnumerator ThrowItem(GameObject item) {
        Vector3 startPoint = item.transform.position;
        Vector3 endPoint = transform.position + Direction * throwForce * throwDuration; 
        item.transform.parent = null; 

        for (int i = 0; i < 25; i++) {
            item.transform.position = Vector3.Lerp(startPoint, endPoint, i* .04f);
            yield return new WaitForSeconds(throwDuration * 0.005F); 
        }
        
        if (item.GetComponent<Rigidbody2D>()) {
            item.GetComponent<Rigidbody2D>().simulated = true; 
        }
        throwDuration = 0; 
    }
}
