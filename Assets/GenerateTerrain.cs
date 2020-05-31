using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateTerrain : MonoBehaviour
{

    // Start is called before the first frame update
    Mesh mesh;
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    MeshCollider meshCollider;
    Material material;
    Material sunmaterial;
    Material moonmaterial;
    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uvs;
    private int vx,vz,l,b;
    private float xc, zc, tc, ka, kd, ks;
    private int shine;
    //private float dx = 0.3f, dz = 0.3f;
    private Vector3 camPos;
    [SerializeField] private Transform sun;
    [SerializeField] private Transform moon;
    [SerializeField] private Color ambColor;
    [SerializeField] private Color diffColor;
    [SerializeField] private Color specColor;

    
    void Start()
    {
        mesh = new Mesh();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
        meshFilter = GetComponent<MeshFilter>();
        sunmaterial = new Material(Shader.Find("SunShader"));
        moonmaterial = new Material(Shader.Find("MoonShader"));
        material = new Material(Shader.Find("WaveShader"));
        meshRenderer.material = material;
        //material.SetColor("_Color", Color.blue);
        vx = (int)GameObject.Find("vx").GetComponent<Slider>().value;
        vz = (int)GameObject.Find("vz").GetComponent<Slider>().value;
        Debug.Log(vx + " " + vz);
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
        sun.GetComponent<MeshRenderer>().material = sunmaterial;
        moon.GetComponent<MeshRenderer>().material = moonmaterial;
        sun.position = new Vector3(0, 10, 0);
        moon.position = new Vector3(0, -10, 0);

    }

    // Update is called once per frame
    void Update()
    {
        camPos = Camera.current.transform.position;
        Debug.Log("Camera: "+camPos);
        vx = (int)GameObject.Find("vx").GetComponent<Slider>().value;
        vz = (int)GameObject.Find("vz").GetComponent<Slider>().value;
        l = (int)GameObject.Find("length").GetComponent<Slider>().value;
        b = (int)GameObject.Find("breath").GetComponent<Slider>().value;
        //ka = GameObject.Find("ka").GetComponent<Slider>().value;
        //kd = GameObject.Find("kd").GetComponent<Slider>().value;
        ks = GameObject.Find("ks").GetComponent<Slider>().value;
        shine = (int)GameObject.Find("shine").GetComponent<Slider>().value;
        //Debug.Log(vx + " " + vz);
        CreateShape();
        //Debug.Log(vertices.Length);
        UpdateMesh();
        if(sun.position.y > 0)
        {
            material.SetVector("_Light", sun.position);
            kd = 1f;
            ka = 0.5f;
        }
        else if (moon.position.y > 0)
        {
            material.SetVector("_Light", moon.position);
            ka = .3f;
            kd = .8f;
        }
        material.SetFloat("_Shine", shine);
        material.SetFloat("_Kd", kd);
        material.SetColor("_Amb", ka * ambColor);
        material.SetVector("_Diff", kd * diffColor);
        material.SetVector("_Spec", ks * specColor);
        material.SetVector("_Cam", camPos);
        //dx += 0.1f;
        //dz += 0.1f;
    }


    void CreateShape() {
        vertices = new Vector3[(vx + 1) * (vz + 1)];
        float dx = l / (float)vx;
        float dz = b / (float)vz;
        int i = 0;
        for (float z = -b / 2; z <= b / 2; z += dz)
            for (float x = -l / 2; x <= l / 2; x += dx)
            {
                //float y = Mathf.PerlinNoise(x * .3f + .4f*Time.time, z * .4f + Time.time) * 2f;
                //float y = Mathf.Sin(Mathf.PI * (x*.2f  + .6f*Time.time));
                xc = GameObject.Find("xc").GetComponent<Slider>().value;
                zc = GameObject.Find("zc").GetComponent<Slider>().value;
                tc = GameObject.Find("tc").GetComponent<Slider>().value;
                float y = GetNoise(x, z, xc, zc, tc);
                vertices[i] = new Vector3(x, y, z);
                i++;
            }


        int vert = 0;
        int tris = 0;
        triangles = new int[vz*vx * 6];
   
        for (int z = 0; z < vz; z++)
        {
            for (int x = 0; x < vx; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + vx + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + vx + 1;
                triangles[tris + 5] = vert + vx + 2;
                vert++;
                tris += 6;
            }
            vert++; 
        }
        i = 0;
        uvs = new Vector2[vertices.Length];
        for (float z = -b / 2; z <= b / 2; z += dz)
            for (float x = -l / 2; x <= l / 2; x += dx)
            {
                uvs[i] = new Vector2((float)x / vx, (float)z / vz);
                i++;
            }
    }

    void UpdateMesh() {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        //for (int i = 0; i < mesh.normals.Length; i++)
        //    Debug.Log(i + " "+mesh.normals[i]);
        mesh.RecalculateNormals();
        mesh.uv = uvs;
        //Debug.Log("Normals: "+mesh.normals[10]);
    }

    float GetNoise(float x, float z, float xc, float zc, float tc)
    {
        int n = GameObject.Find("Noise").GetComponent<Dropdown>().value;
        float y1 = 0f;
        if (n==0)
            y1 = Mathf.Sin(Mathf.PI * (x * xc + z * zc + tc * Time.time));     
        else if (n == 1)
            y1 = Mathf.PerlinNoise(x * xc + tc * Time.time, z * zc + tc * Time.time) * 2f;
        return y1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        //Debug.Log("Giz");
        if (vertices != null)
            for(int i = 0; i< vertices.Length; i++)
                Gizmos.DrawSphere(vertices[i], .1f);
    }
}
