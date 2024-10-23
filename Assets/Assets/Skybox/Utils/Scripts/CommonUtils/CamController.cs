using UnityEngine;

namespace Boxophobic.Utils
{
    public class CamController : MonoBehaviour
    {
        public float movementSpeed = 5f;
        public float accelerationMultiplier = 2f;
        public float sensitivity = 2f;

        private float yaw = 0f;
        private float pitch = 0f;

        void Start()
        {
            // Store the initial rotation of the camera
            yaw = transform.eulerAngles.y;
            pitch = transform.eulerAngles.x;
        }

        void Update()
        {
            float currentSpeed = movementSpeed;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                currentSpeed *= accelerationMultiplier;
            }

            float horizontalMovement = Input.GetAxis("Horizontal") * currentSpeed * Time.deltaTime;
            float verticalMovement = Input.GetAxis("Vertical") * currentSpeed * Time.deltaTime;

            transform.Translate(horizontalMovement, 0, verticalMovement);

            yaw += sensitivity * Input.GetAxis("Mouse X");
            pitch -= sensitivity * Input.GetAxis("Mouse Y");
            pitch = Mathf.Clamp(pitch, -90f, 90f);

            transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
        }
    }
}