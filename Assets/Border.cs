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

    public Vector3 GetRandomSpawnPoint()
    {
        float xSize = _collider.bounds.size.x / 2.0f;
        float ySize = _collider.bounds.size.y / 2.0f;

        //We do this check so that asteroids are more likely to spawn on the longer borders
        bool shouldSpawnOnXAxis = (Random.Range(0.0f, xSize + ySize) <= xSize);

        //Once our randomized axis is selected, on which side of the screen should it spawn
        bool shouldSpawnBottomLeft = Random.value > 0.5f;

        if(shouldSpawnOnXAxis)
        {
            float xPos = Random.Range(0.0f, xSize);
            float yPos = ySize;
            if (shouldSpawnBottomLeft)
                yPos *= -1;
            return new Vector3(xPos, yPos, 0);
        }
        else
        {
            float xPos = xSize;
            float yPos = Random.Range(0.0f, ySize);
            if (shouldSpawnBottomLeft)
                xPos *= -1;
            return new Vector3(xPos, yPos, 0);
        }
    }
}
