using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class AnimationEvents : MonoBehaviour
{
    // consider creating an interface and inheriting for specific use cases
    [Header("Visual Effects")]
    public GameObject Dash;

    void AE_Dodge()
    {
        var character = transform.parent.GetComponent<Character>();
        if (!character) return;

        float xOffset = -1.5f;
        float yOffset = -0.1f;
        Quaternion rotation = Quaternion.identity;
        int direction = 1;

        if (!character.IsFacingRight)
        {
            rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            direction = -1;
        }

        var newPos = transform.position + new Vector3(xOffset * direction, yOffset, 0f);

        // object destruction contained within animation event at the end of its playthrough
        Instantiate(Dash, newPos, rotation, transform);
    }
}
