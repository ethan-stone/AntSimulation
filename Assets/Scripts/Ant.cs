using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    public float maxSpeed = 20f;
    public float steerStrength = 4f;
    public float wanderStrength = 1f;

    Vector2 position;
    Vector2 velocity;
    Vector2 desiredDirection;

    void Update() {
        desiredDirection = (desiredDirection + Random.insideUnitCircle * wanderStrength).normalized;

        Vector2 desiredVelocity = desiredDirection * maxSpeed;
        Vector2 desiredSteeringForce = (desiredVelocity - velocity) * steerStrength;
        Vector2 acceleration = Vector2.ClampMagnitude(desiredDirection, steerStrength) / 1;

        velocity = Vector2.ClampMagnitude(velocity + acceleration * Time.deltaTime, maxSpeed);

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0.0 || 1.0 < pos.x) {
            velocity.x = -velocity.x;
        }
        if (pos.y < 0.0 || 1.0 < pos.y) {
            velocity.y = -velocity.y;
        }

        position += velocity * Time.deltaTime;

        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.SetPositionAndRotation(position, Quaternion.Euler(0, 0, angle));
    }
}
