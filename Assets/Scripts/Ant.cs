using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    public float maxSpeed = 2;
    public float steerStrength = 2;
    public float wanderStrength = 1;

    public LayerMask foodLayer;

    public float viewRadius;
    public float viewAngle;

    Vector2 position;
    Vector2 velocity;
    Vector2 desiredDirection;

    Transform targetFood;

    void Update() {
        desiredDirection = (desiredDirection + Random.insideUnitCircle * wanderStrength).normalized;

        Vector2 desiredVelocity = desiredDirection * maxSpeed;
        Vector2 desiredSteeringForce = (desiredVelocity - velocity) * steerStrength;
        Vector2 acceleration = Vector2.ClampMagnitude(desiredSteeringForce, steerStrength) / 1;

        velocity = Vector2.ClampMagnitude(velocity + acceleration * Time.deltaTime, maxSpeed);

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0.0 || 1.0 < pos.x) {
            velocity.x = -velocity.x;
        }
        if (pos.y < 0.0 || 1.0 < pos.y) {
            velocity.y = -velocity.y;
        }

        position += velocity * Time.deltaTime;
        HandleFood();

        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.SetPositionAndRotation(position, Quaternion.Euler(0, 0, angle));
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
        if (!angleIsGlobal) {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    void HandleFood() {
        Collider[] allFood = Physics.OverlapSphere(transform.position, viewRadius, foodLayer);
        
        print(allFood.Length);
        if (targetFood == null) {
            if (allFood.Length > 0) {
                Transform food = allFood[Random.Range(0, allFood.Length)].transform;
                Vector2 dirToFood = (food.position - transform.position).normalized;

                if (Vector2.Angle(transform.forward, dirToFood) < viewAngle / 2) {
                    food.gameObject.layer = LayerMask.NameToLayer("TakenFood");
                    targetFood = food;
                }
            }
        } else {
            desiredDirection = (targetFood.position - transform.position).normalized;
            
            const float foodPickupRadius = 0.5f;
            if (Vector3.Distance(targetFood.position, transform.position) < foodPickupRadius) {
                targetFood.position = transform.position;
                targetFood.parent = transform;
                targetFood = null;
            }
        }
    }
}
