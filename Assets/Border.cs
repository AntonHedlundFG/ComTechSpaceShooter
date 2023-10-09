using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Border : MonoBehaviour
{
    BoxCollider _collider;

    void OnEnable()
    {
        _collider = GetComponent<BoxCollider>();
    }
    private void OnTriggerExit(Collider other)
    {
        //We never want to teleport a child object, only the parent. 
        Transform otherObject = other.transform;
        while(otherObject.parent != null)
        {
            otherObject = otherObject.parent;
        }

        //This part teleports a game object with a collider if it tries to leave the play area.
        //Manual checking of each bounds; for example, if we're past the upper x bound, we teleport bounds.size.x in the negative x direction.
        Vector3 bounds = _collider.bounds.size / 2;
        Vector3 otherPosition = otherObject.position;

        if (other.transform.position.x > transform.position.x + bounds.x)
        {
            otherPosition += new Vector3(-bounds.x * 2, 0, 0);
        } else if (other.transform.position.x < transform.position.x - bounds.x)
        {
            otherPosition += new Vector3(bounds.x * 2, 0, 0);
        }

        if (other.transform.position.y > transform.position.y + bounds.y)
        {
            otherPosition += new Vector3(0, -bounds.y * 2, 0);
        }
        else if (other.transform.position.y < transform.position.y - bounds.y)
        {
            otherPosition += new Vector3(0, bounds.y * 2, 0);
        }

        otherObject.position = otherPosition;

    }

}
