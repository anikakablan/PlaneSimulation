using UnityEngine;

namespace FLFlight
{
    public class CameraRig : MonoBehaviour
    {
        [Tooltip("The ship to follow around.")]
        [SerializeField] private Transform ship = null;

        [Tooltip("The camera on this rig. Required for lookahead motions.")]
        [SerializeField] private Camera cam = null;

        [Tooltip("The lookahead portion of the rig. This is a separate transform under the camera rig which is used only for the lookahead motions.")]
        [SerializeField] private Transform lookAheadRig = null;

        [Tooltip("Enable if the target to follow is being updated during FixedUpdate (e.g. if it is a Rigidbody using physics).")]
        [SerializeField] private bool useFixed = true;

        [Tooltip("How quickly the camera rotates to new positions. Tweak this values to get something that feels good. High values will result in tighter camera motion.")]
        [SerializeField] private float smoothSpeed = 10f;

        [Header("Lookahead Values")]
        [SerializeField] private float horizontalTurnAngle = 15f;
        [SerializeField] private float verticalTurnUpAngle = 5.0f;
        [SerializeField] private float verticalTurnDownAngle = 15.0f;

        const float kSpeedSlope = 0.0002f;

        private void Update()
        {
            if (useFixed == false)
                MoveCamera();
        }

        private void FixedUpdate()
        {
            if (useFixed == true)
                MoveCamera();
        }

        private void MoveCamera()
        {
            if (ship == null)
                return;

            // Follow the ship around.
            transform.position = ship.position;


            var targetRigRotation = Quaternion.LookRotation(ship.forward, transform.up);
            transform.rotation = SmoothDamp.DampS(transform.rotation, targetRigRotation, smoothSpeed, Time.deltaTime);


            RotateRigAndCameraForLookahead();
        }

        private void RotateRigAndCameraForLookahead()
        {

            var mousePos = Input.mousePosition;

            var mouseScreenX = (mousePos.x - (Screen.width * 0.5f)) / (Screen.width * 0.5f);
            var mouseScreenY = -(mousePos.y - (Screen.height * 0.5f)) / (Screen.height * 0.5f);

            // Clamp these screen position to make sure the rig doesn't oversteer.
            mouseScreenX = Mathf.Clamp(mouseScreenX, -1f, 1f);
            mouseScreenY = Mathf.Clamp(mouseScreenY, -1f, 1f);

            // Use the horizontal and vertical turn angles to rotate the camera's local rig position.
            float horizontal = 0f;
            float vertical = 0f;
            horizontal = horizontalTurnAngle * mouseScreenX;
            vertical = (mouseScreenY < 0.0f) ? verticalTurnUpAngle * mouseScreenY : verticalTurnDownAngle * mouseScreenY;

            lookAheadRig.localRotation = SmoothDamp.DampS(lookAheadRig.localRotation, Quaternion.Euler(-vertical, -horizontal, 0f), smoothSpeed, Time.deltaTime);


            Vector3 lookaheadPosition = ship.transform.TransformPoint(Vector3.forward * 100f);
            cam.transform.rotation = Quaternion.LookRotation(lookaheadPosition - lookAheadRig.position, lookAheadRig.up);
        }
    }
}
