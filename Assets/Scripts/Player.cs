using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{

    public MeshGenerator mg;
    public float moveSpeed = 10;
    public float friction = 0.9f;
    public float sensitivity = 50;
    public int radius = 10;

    bool onPoint = false;
    Vector3Int placeAt = Vector3Int.zero;
    bool cursorLocked;

    Rigidbody rb;
    GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (mg.storedData.Count == 0) {
            mg.storedData = new List<Vector4>();
            for (int y = 0; y < mg.numPointsPerAxis; y++) {
                for (int x = 0; x < mg.numPointsPerAxis; x++) {
                    for (int z = 0; z < mg.numPointsPerAxis; z++) {
                        mg.storedData.Add(new Vector4(x, y, z, -15));
                    }
                }
            }
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cam = gameObject;//Camera.main.gameObject;
    }

    void Awake()
    {

    }

    int ConvertVec3ToInt(Vector3Int v)
    {
        return v.x + v.y * 30 + v.z * 30 * 30; // * 30 + v.y * 30 + v.z;
    }

    // Update is called once per frame
    void Update()
    {
        // ------------------- CURSOR -------------------

        if (Input.GetKeyDown(KeyCode.Escape)) {
            cursorLocked = !cursorLocked;
            if (cursorLocked) {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        // ------------------- LOOK AROUND -------------------

        if (Cursor.lockState == CursorLockMode.Locked) {

            float MouseyawX = Input.GetAxis("Mouse X");
            float MouseyawY = Input.GetAxis("Mouse Y");
            Vector3 PlayerRotation = new Vector3(-MouseyawY * sensitivity, MouseyawX * sensitivity, 0f) * (1 / Time.deltaTime) / 60;
            cam.transform.Rotate(PlayerRotation * Time.deltaTime);

            Vector3 rotation = cam.transform.rotation.eulerAngles;
            rotation.z = 0f;
            cam.transform.rotation = Quaternion.Euler(rotation);
            if (cam.transform.rotation.eulerAngles.x > 85 && cam.transform.rotation.eulerAngles.x < 90) {
                cam.transform.rotation = Quaternion.Euler(new Vector3(85, cam.transform.rotation.eulerAngles.y, cam.transform.rotation.eulerAngles.z));
            }
            if (cam.transform.rotation.eulerAngles.x < 275 && cam.transform.rotation.eulerAngles.x > 90) {
                cam.transform.rotation = Quaternion.Euler(new Vector3(275, cam.transform.rotation.eulerAngles.y, cam.transform.rotation.eulerAngles.z));
            }
        }



        string rotxS = transform.eulerAngles.x.ToString();
        float rotxCam = float.Parse(rotxS);

        transform.rotation = Quaternion.Euler(new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z));

        Vector3 relativeTranForward = transform.forward;
        Vector3 relativeTranRight = transform.right;
        transform.rotation = Quaternion.Euler(new Vector3(rotxCam, transform.eulerAngles.y, transform.eulerAngles.z));

        // ------------------- MOVE AROUND -------------------

        if (Input.GetKey(KeyCode.W)) {
            rb.AddForce(relativeTranForward * moveSpeed);
        }
        if (Input.GetKey(KeyCode.S)) {
            rb.AddForce(-relativeTranForward * moveSpeed);
        }
        if (Input.GetKey(KeyCode.D)) {
            rb.AddForce(relativeTranRight * moveSpeed);
        }
        if (Input.GetKey(KeyCode.A)) {
            rb.AddForce(-relativeTranRight * moveSpeed);
        }
        if (Input.GetKey(KeyCode.Space)) {
            rb.AddForce(new Vector3(0, 1, 0) * moveSpeed);
        }
        if (Input.GetKey(KeyCode.LeftShift)) {
            rb.AddForce(new Vector3(0, -1, 0) * moveSpeed);
        }
        rb.velocity *= friction;


        // ------------------- RAYCAST -------------------

        Vector3 forward = transform.forward;
        onPoint = false;
        RaycastHit hit;
        //int layerMask = 1 << 9;
        Debug.DrawRay(transform.position, forward * 1000, Color.blue);
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, forward, out hit, Mathf.Infinity)) {
            Vector3 placePos = hit.point;
            onPoint = true;
            placeAt = new Vector3Int(Mathf.RoundToInt(placePos.x), Mathf.RoundToInt(placePos.y), Mathf.RoundToInt(placePos.z));
        }

        // ------------------- BUTTONS -------------------

        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1)) {
            if (onPoint) {
                Vector3Int rPlaceAt = placeAt - new Vector3Int(15, -15, 15);
                //  print("DATA: " + mg.storedData[ConvertVec3ToInt(placeAt)] + "|" + rPlaceAt);
                float remove = Input.GetKey(KeyCode.Mouse1) ? -1 : 1;

                for (int y = -radius; y < radius; y++) {
                    for (int x = -radius; x < radius; x++) {
                        for (int z = -radius; z < radius; z++) {
                            Vector3 newPos = new Vector3(x, y, z);// - new Vector3(radius, radius, radius) * 0.5f;
                            // print(newPos.sqrMagnitude);
                            if (Mathf.Abs( newPos.sqrMagnitude) < radius) {
                                print(x);
                                Vector3Int newPlaceAt = rPlaceAt + new Vector3Int(x, y, z) + new Vector3Int(15, -30, 15);

                                try {
                                    mg.storedData[ConvertVec3ToInt(newPlaceAt)] = (mg.storedData[ConvertVec3ToInt(newPlaceAt)] + new Vector4(0, 0, 0, remove* 0.1f)); // new Vector4(newPlaceAt.x, newPlaceAt.y, newPlaceAt.z, Random.Range(-15,15));

                                }
                                catch (System.Exception) {
                                    print("error");
                                }
                            }

                        }
                    }
                }

                // mg.storedData[ConvertVec3ToInt(placeAt)] = new Vector4(rPlaceAt.x, rPlaceAt.y, rPlaceAt.z, 15f);

                mg.UpdateAllChunks();
            }


        }

        if (Input.GetKeyDown(KeyCode.G)) { // RANDOM
            mg.storedData = new List<Vector4>();
            for (int y = 0; y < mg.numPointsPerAxis; y++) {
                print(y - 15);
                for (int x = 0; x < mg.numPointsPerAxis; x++) {
                    for (int z = 0; z < mg.numPointsPerAxis; z++) {
                        mg.storedData.Add(new Vector4(x * MeshGenerator._pointSpacing - 15, y * MeshGenerator._pointSpacing - 15, z * MeshGenerator._pointSpacing - 15, Random.Range(-15, 15)));
                    }
                }
            }
            mg.UpdateAllChunks();

        }



    }
}
