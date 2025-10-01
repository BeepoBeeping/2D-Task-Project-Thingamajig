using Unity.VisualScripting;
using UnityEngine;

public class HelperScript : MonoBehaviour
{
    public bool HW;
    public void DoFlipObject( bool flip )
    {
        // get the SpriteRenderer component
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();

        if (flip == true)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }

        
    }

}

