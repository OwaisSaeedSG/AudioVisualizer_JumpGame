using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioToShader : MonoBehaviour
{

    private Material changeMaterial;

    void Start()
    {
        changeMaterial = GetComponent<MeshRenderer>().sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ClickRay();
    }

    private void ClickRay()
    {
        var camera = Camera.main;
        var mousePosition = Input.mousePosition;

        var ray = camera.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y, camera.nearClipPlane));
        if(Physics.Raycast(ray, out var hit) && hit.collider.gameObject == gameObject)
        {
            Rippletime(hit.point);
        }
    }


    // for testing purposes
    private void Rippletime(Vector3 center)
    {
        changeMaterial.SetVector("_RippleCenter", center);
        changeMaterial.SetFloat("_RippleStartTime", Time.time);

        //changeMaterial.SetFloat("")
    }
}
