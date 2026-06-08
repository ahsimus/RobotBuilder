using UnityEngine;

public class MouseRotate : MonoBehaviour
{


    [SerializeField] private Transform target;
    [SerializeField] private float sensitivity = 200f;

    private bool isDragging;

    private void Update()
    {
        HandleInput();

        if (!isDragging || target == null)
            return;

        float deltaX = Input.GetAxis("Mouse X");

        float rotationY = deltaX * sensitivity * Time.deltaTime;

        target.Rotate(0f, -rotationY, 0f, Space.World);
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
            isDragging = true;

        if (Input.GetMouseButtonUp(0))
            isDragging = false;
    }

}