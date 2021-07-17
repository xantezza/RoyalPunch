using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 _prefferedCameraPosition;

    public Vector3 _prefferedCameraRotation;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public IEnumerator SetupCamera()
    {
        var player = FindObjectOfType<PlayerCharacter>();
        player.isStunned = true;
        while (!(_camera.transform.position == _prefferedCameraPosition && _camera.transform.eulerAngles == _prefferedCameraRotation))
        {
            _camera.transform.position += Vector3.ClampMagnitude(_prefferedCameraPosition - _camera.transform.position, 0.05f);
            _camera.transform.eulerAngles += Vector3.ClampMagnitude(_prefferedCameraRotation - _camera.transform.eulerAngles, 5f);
            yield return new WaitForEndOfFrame();
        }
        player.isStunned = false;
    }
}