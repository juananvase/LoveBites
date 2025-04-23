using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class FaceCameraUI : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(CheckUIFacingCamera());
    }

    private IEnumerator CheckUIFacingCamera()
    {
        while (true) 
        {
            FaceCamera();
            yield return new WaitForSeconds(1f);
        }
    }

    private void FaceCamera()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
