using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private CameraController _cameraController;

    public void OnPlayButton(Button button)
    {
        StartCoroutine(_cameraController.SetupCamera());
        Destroy(button.gameObject);
    }
    
}