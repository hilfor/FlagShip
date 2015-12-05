using UnityEngine;
using System.Collections;

public class PlayerControllerz : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Mouse 0 clicked");
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 10f))
            {
                if (hit.collider.tag.Equals("TacMap"))
                {
                    //Debug.Log("Ray hit tacmap");
                    hit.collider.GetComponent<TacMapController>().DoSelectProjection(hit.textureCoord);
                }
            }
        }
    }
}
