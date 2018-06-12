using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandTrails : MonoBehaviour {
    //This should be an icosphere, no way to enforce it afaik though
    public Mesh mesh;
    public MeshCollider mcollider;
    // Use this for initialization
    void Start() {
        mesh = GetComponent<MeshFilter>().mesh;

        mcollider = GetComponent<MeshCollider>();
        //just for testing, picking a random point that's close
        //enough on the sphere
        Lower(new Vector3(0, -2, 0), 0.2f, -0.1f);
    }


    //Lower or raise a point and the area around it 
    public void Lower(Vector3 point, float radius, float amount) {

        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;

        float radiusSqr = radius * radius;

        for (int i = 0; i < vertices.Length; i++) {

            //vertex positions are in local space
            Vector3 wpos = transform.TransformPoint(vertices[i]);

            float distanceSqr = (wpos - point).sqrMagnitude;
            //if it is in raiseterrain radius (is a sphere, this will give strange effect when radius is too large
            //for now a sphere is fine but should probably be changed to be in a circle
            if (distanceSqr < radiusSqr) {

                //log factor
                float g = 1.3f;
                //Logarithmic interpolation
                float raiseAmount = ((0 - amount) * Mathf.Exp(g * distanceSqr) + amount * Mathf.Exp(g * radiusSqr) - 0 * Mathf.Exp(g * 0))
                    / (Mathf.Exp(g * radiusSqr) - Mathf.Exp(g * 0));
                //move in direction of normal
                Vector3 dir = normals[i];
                vertices[i] += dir * raiseAmount;

            }
        }


        //update mesh     
        mesh.vertices = vertices;
        mesh.RecalculateNormals();

        //update collider;
        DestroyImmediate(mcollider);
        mcollider = gameObject.AddComponent<MeshCollider>();

    }
}
