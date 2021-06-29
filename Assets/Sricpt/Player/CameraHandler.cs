using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;

namespace SG
{
    public class CameraHandler : MonoBehaviour
    {
        InputHandler inputHandler;
        PlayerManager playerManager;
        public Transform targetTransform;
        public Transform cameraTransform;
        public Transform cameraPivotTransform;
        private Transform myTransform;
        private Vector3 cameraTransformPosition;
        public LayerMask ignoreLayers;
        public LayerMask enviromentLayer;
        private Vector3 cameraFollowVelocity = Vector3.zero;
        public static CameraHandler singleton;

        public float lookSpeed = 0.1f;
        public float followSpeed = 0.1f;
        public float pivotSpeed = 0.03f;

        private float targetPosition;
        private float defaultPosition;
        private float lookAngle;
        private float pivotAngle;
        public float minimunPivot = -35;
        public float maximunPivot = 35;
        public float cameraSphereRadius = 0.2f;
        public float cameraCollisionOffSet = 0.2f;
        public float minimunCollisionOffset = 0.2f;
        public float lockPivotPosition = 2.25f;
        public float unloackPivotPosition = 1.65f;

        public Transform currentLockOnTarget;
        List<CharacterManager> avilableTargets = new List<CharacterManager>();
        public Transform nearestLockOnTarget;
        public Transform leftLockTarget;
        public Transform rightLockTarget;
        public float maximunLockOnDistance = 30;
        void Awake()
        {
            singleton = this;
            myTransform = transform;
            defaultPosition = cameraTransform.localPosition.z;
            ignoreLayers = ~(1 << 9);
            targetTransform = FindObjectOfType<PlayerManager>().transform;
            inputHandler = FindObjectOfType<InputHandler>();
            playerManager = FindObjectOfType<PlayerManager>();
        }
        private void Start() {
            enviromentLayer = LayerMask.NameToLayer("Enviroment");
        }
        public void FollowTarget(float delta)
        {
            Vector3 targetPosition = Vector3.SmoothDamp
                (myTransform.position, targetTransform.position,ref cameraFollowVelocity, delta / followSpeed);
            myTransform.position = targetPosition;
            HandleCameraCollisions(delta);
        }
        public void HandleCameraRotation(float delta, float mouseInputX, float mouseInputY)
        {
            if(inputHandler.lockOnFlag == false && currentLockOnTarget == null)
            {
                lookAngle += (mouseInputX * lookSpeed) / delta;
                pivotAngle -= (mouseInputY * pivotSpeed) / delta;
                pivotAngle = Mathf.Clamp(pivotAngle, minimunPivot, maximunPivot);

                Vector3 rotation = Vector3.zero;
                rotation.y = lookAngle;
                Quaternion targetRotation = Quaternion.Euler(rotation);
                myTransform.rotation = targetRotation;
                rotation = Vector3.zero;
                rotation.x = pivotAngle;
                targetRotation = Quaternion.Euler(rotation);
                cameraPivotTransform.localRotation = targetRotation;
            }
            else 
            {
                Vector3 dir = currentLockOnTarget.position - transform.position;
                dir.Normalize();
                dir.y = 0;

                Quaternion targetRotation = Quaternion.LookRotation(dir);
                transform.rotation = targetRotation;

                dir = currentLockOnTarget.position - cameraPivotTransform.position;
                dir.Normalize();

                targetRotation = Quaternion.LookRotation(dir);
                Vector3 eulerAngle = targetRotation.eulerAngles;
                eulerAngle.y = 0;
                cameraPivotTransform.localEulerAngles = eulerAngle;
            }
        }
        private void HandleCameraCollisions(float delta)
        {
            targetPosition = defaultPosition;
            RaycastHit hit;
            Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
            direction.Normalize();
            if (Physics.SphereCast
                (cameraPivotTransform.position, cameraSphereRadius, direction, out hit, Mathf.Abs(targetPosition)
                ,ignoreLayers))
            {
                float dis = Vector3.Distance(cameraPivotTransform.position,hit.point);
                targetPosition = -(dis - cameraCollisionOffSet);
            }
            if (Mathf.Abs(targetPosition) < minimunCollisionOffset)
            {
                targetPosition = -minimunCollisionOffset;
            }
            cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, delta / 0.2f);
            cameraTransform.localPosition = cameraTransformPosition;
        }
        
        public void HandleLockOn()
        {
            float shortesDistance = Mathf.Infinity;
            float shortestDistanceOfLeftTarget = Mathf.Infinity;
            float shortestDistanceOfRightTarget = Mathf.Infinity;
            Collider[] colliders = Physics.OverlapSphere(targetTransform.position,26);
            for(int i = 0;i < colliders.Length;i++)
            {
                CharacterManager character = colliders[i].GetComponent<CharacterManager>();
                if(character != null)
                {
                    Vector3 lockTargetDirection = character.transform.position - targetTransform.position;
                    float distanceFromTarget = Vector3.Distance(targetTransform.position,character.transform.position);
                    float viewableAngle = Vector3.Angle(lockTargetDirection,cameraTransform.forward);
                    RaycastHit hit;
                    if(character.transform.root!=targetTransform.root
                        && viewableAngle > -50 && viewableAngle < 50
                        && distanceFromTarget <= maximunLockOnDistance)
                    {
                        if(Physics.Linecast(playerManager.lockOnTransform.position,character.lockOnTransform.position, out hit))
                        {
                            Debug.DrawLine(playerManager.lockOnTransform.position,character.lockOnTransform.position);
                            if(hit.transform.gameObject.layer == enviromentLayer)
                            {
                                // which cannot to be locked
                            }
                            else
                            {
                                avilableTargets.Add(character);
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < avilableTargets.Count; i++)
            {
                float distanceFromTarget = Vector3.Distance(targetTransform.position,avilableTargets[i].transform.position);
                if(distanceFromTarget < shortesDistance)
                {
                    shortesDistance = distanceFromTarget;
                    nearestLockOnTarget = avilableTargets[i].lockOnTransform;
                }
                if(inputHandler.lockOnFlag)
                {
                    Vector3 relativeEnemyPosition = currentLockOnTarget.InverseTransformPoint(avilableTargets[i].transform.position);//以currentLockOnTarget为中心
                    var distanceFromRightTarget = currentLockOnTarget.transform.position.x - avilableTargets[i].transform.position.x;
                    var distanceFromLeftTarget = avilableTargets[i].transform.position.x + currentLockOnTarget.transform.position.x;  

                    if(relativeEnemyPosition.x > 0.00 && distanceFromLeftTarget < shortestDistanceOfLeftTarget)
                    {
                        shortestDistanceOfLeftTarget = distanceFromLeftTarget;
                        leftLockTarget = avilableTargets[i].lockOnTransform;
                    }
                    if(relativeEnemyPosition.x < 0.00 && distanceFromRightTarget < shortestDistanceOfRightTarget)
                    {
                        shortestDistanceOfRightTarget = distanceFromRightTarget;
                        rightLockTarget = avilableTargets[i].lockOnTransform;
                    }
                }
            }
        }

        public void ClearLockOnTargets()
        {
            avilableTargets.Clear();
            currentLockOnTarget = null;
            nearestLockOnTarget = null;
        }

        public void SetCameraHeight()
        {
            Vector3 velocity = Vector3.zero;
            Vector3 newLockedPosition = new Vector3(0,lockPivotPosition);
            Vector3 newUnlockedPosition = new Vector3(0,unloackPivotPosition);
            if(currentLockOnTarget!=null)
            {
                cameraPivotTransform.transform.localPosition  = Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition,newLockedPosition,ref velocity , Time.deltaTime);
            }
            else
            {
                cameraPivotTransform.transform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.transform.localPosition,newUnlockedPosition,ref velocity,Time.deltaTime);
            }
        }
    }
}
 