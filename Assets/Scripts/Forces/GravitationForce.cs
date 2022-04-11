using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationForce : Force
{
    [SerializeField] FloatData gravitation;

    public override void ApplyForce(List<Body> bodies)
    {
        for(int i = 0; i < bodies.Count; i++)
        {
            for (int j = 0; j < bodies.Count; j++)
            {
                if (i == j) continue;

                Body bodyA = bodies[i];
                Body bodyB = bodies[j];

                Vector2 direction = bodyA.position - bodyB.position;
                float distance = Mathf.Max(1, direction.magnitude);
                float force = gravitation.value * (bodyA.mass * bodyB.mass) / distance;

                bodyA.ApplyForce(-direction.normalized * force, Body.eForceMode.FORCE);
                bodyB.ApplyForce(direction.normalized * force, Body.eForceMode.FORCE);
            }
        }
    }
}
