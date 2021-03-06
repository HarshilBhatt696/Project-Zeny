using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceObjects : MonoBehaviour
{



    public Collider2D anotherCollider;

    // Distance can be 2m;
    // Distance should be 0.6f;

    float DivideDistance = 0.6f; // was 0.7
    private float GlobalScale = 0.1f;
    public List<GameObject> ChairList = new List<GameObject>();

    // public ARRaycastManager arRaycastManager;
    //public GameObject cubePrefab;
    public ARPlane arPlane;    // Start is called before the first frame update
    public Text DistancingText;
    public Text ScaleText;
    public GameObject ScrollView;
    public GameObject Animation;
    // Corona
    public GameObject Corona;

    private GameObject SpawnedObject;
    private GameObject SpawnedObject1;
    private GameObject SpawnedObject2;
    int Sizey = 0;

    // GAME OBJECTS 
    public GameObject GameObjectCube; // UNIVERSITY
    public GameObject ChairTable; // SCHOOL
    public GameObject DinnerTable;
    public GameObject BarTable;
    public GameObject Audi;
    public GameObject SchoolDeskObject;


    private GameObject SpawnItem;

    private static List<Vector3> ObjectsLocation = new List<Vector3>();

    // private static List<GameObject> ObjectsLocation = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        print("TAGME " + other.gameObject.tag);
    }
    private void OnTriggerStay(Collider other)
    {
        print("TAGME 123 " + other.gameObject.tag);
    }
    private ARRaycastManager rayMangaer;
    private ARPlaneManager PlaneManagers;
    private Vector2 TouchPosition;

    // Buttons 
    public Button MainButton;
    //School Chairs
    public Button ChairsButton;
    //Dinners Chairs
    public Button DinnerTableButton;
    //BAR TABLE
    public Button BarChar;
    // NORMAL TEST
    public Button Test;
    // Normal Test
    public Button AudiBtn;
    // Normal Test
    public Button SchoolDesk;


    private bool showPage = false;


    private bool added = false;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    // Update is called once per frame
    private ARPlaneManager ARPlaneMan;
    private void Awake()
    {
        ScrollView.gameObject.SetActive(false);
        rayMangaer = GetComponent<ARRaycastManager>();
        ARPlaneMan = GetComponent<ARPlaneManager>();
    }

    private void Start()
    {
        Button btn = MainButton.GetComponent<Button>();
        btn.onClick.AddListener(MainButtonOrder);
        ChairsButton.onClick.AddListener(ChairSpawner);
        DinnerTableButton.onClick.AddListener(DinnerSpawner);
        Test.onClick.AddListener(TestSpawner);
        BarChar.onClick.AddListener(BarTableSpawner);
        AudiBtn.onClick.AddListener(AudiClick);
        SchoolDesk.onClick.AddListener(SchoolDeskSpawner);

    }

    void SchoolDeskSpawner()
    {
        SpawnItem = SchoolDeskObject;


        TaskOnClick();
    }



    void BarTableSpawner()
    {
        SpawnItem = BarTable;


        TaskOnClick();
    }

    void TestSpawner()
    {

        SpawnItem = GameObjectCube;


        TaskOnClick();
    }
    void ChairSpawner()
    {

        SpawnItem = ChairTable;





        TaskOnClick();

    }

    void DinnerSpawner()
    {
        SpawnItem = DinnerTable;



        TaskOnClick();
    }
    void AudiClick()
    {
        SpawnItem = Audi;


        TaskOnClick();
    }


    void MainButtonOrder()
    {

        foreach (var light in ChairList)
        {
            Destroy(light);
        }
        showPage = true;
        ScrollView.gameObject.SetActive(true);

    }



    public float dimX;
    public float dimY;


    public void OnValueChanged(float Value)
    {


        if (DivideDistance - Value >= 0.1f || DivideDistance - Value <= -0.1f)
        {
            DivideDistance = (Mathf.Round((Value) * 100f) / 100f);
            DistancingText.text = DivideDistance.ToString() + "m";


            foreach (var light in ChairList)
            {
                Destroy(light);
            }
            if (SpawnItem != null)
            {
                TaskOnClick();
            }
        }
    }

    public void onScaleChange(float Scale)
    {

        GlobalScale = (Mathf.Round((Scale) * 100f) / 100f);
        ScaleText.text = GlobalScale.ToString();
        SpawnItem.transform.localScale = new Vector3(GlobalScale, GlobalScale, GlobalScale);
        foreach (var light in ChairList)
        {
            Destroy(light);
        }
        if (SpawnItem != null)
        {
            TaskOnClick();
        }


    }
    void TaskOnClick()
    {





        showPage = false;

        //Plane planeRight = new Plane(_selectionPoints.SelectoionPlaneFarBR,selectionPoints.SelectoionPlaneFarTR,_selectionPoints.SelectoionPlaneNearTR);  

        ScrollView.gameObject.SetActive(false);

        bool DoneOnce = false;
        foreach (var plane in ARPlaneMan.trackables)
        {





            Mesh planeMesh = plane.GetComponent<MeshFilter>().mesh;


            plane.GetComponent<Collider>().isTrigger = true;
            plane.tag = "-Planee";

            print("So wait this is the size I got okay Now figure it out " + plane.size);
            print(" So wait this is the size I got okay Now figure it out " + planeMesh.bounds.size);
            //Algo(plane.center, planeMesh.bounds.size , 0.8f);

            // SpawnItem = GameObjectCube;

            //if ((plane.size.y / 2) / CurrentDistancing % 2 != 0)
            //{
            PlaceChairs(plane.center, plane, 0.6f); // WAS 0.6f

            // foreach (var light in ChairList)
            // {


            //Vector3 AC = light.GetComponent<GameObject>().transform.position;
            //AC.z -= 1f;
            //AC.x += 1f;
            // light.GetComponent<GameObject>().transform.position += AC;

            //}

            //}
            //else {

            //    EVENFUNCTION(plane.center, plane, 0.6f);
            //}

            ARPlaneMan.enabled = false;
            break;


        }
    }

    void Update()
    {

        bool Done = false;
        foreach (var plane in ARPlaneMan.trackables)
        {
            Animation.SetActive(false);
            if (Done == false)
            {

                Done = true;
            }
            else
            {
                Destroy(plane);
            }


        }



    }




    private void ArPlane_BoundaryChanged(ARPlaneBoundaryChangedEventArgs obj)
    {
        //areaText.text = CalculatePlaneArea(arPlane).ToString();
    }
    private float CalculatePlaneArea(ARPlane plane)
    {
        return plane.size.x * plane.size.y;
    }

    private object Vector3(float x, float y, float z)
    {
        throw new System.NotImplementedException();
    }

    // Divisble by 2

    // 6 or 7
    private void PlaceChairs(Vector3 Plane, ARPlane Item, float Distancing)
    {
        int i = 0;






        // LEFT 
        while (i <= (Item.size.y / 2) / DivideDistance)
        {
            SpawnedObject = Instantiate(SpawnItem, Plane, arPlane.transform.rotation);
            Vector3 A = Plane;
            A.y = Item.transform.position.y;
            SpawnedObject.transform.parent = Item.transform;
            A.z -= (DivideDistance * i);
            SpawnedObject.gameObject.tag = "Chair";
            ChairList.Add(SpawnedObject);

            int j = 0;

            while (j <= (Item.size.x / 2) / DivideDistance)
            {
                SpawnedObject = Instantiate(SpawnItem, Plane, arPlane.transform.rotation);
                Vector3 AB = Plane;
                AB.y = Item.transform.position.y;


                SpawnedObject.transform.parent = Item.transform;
                AB.z -= (DivideDistance * i);
                AB.x -= (DivideDistance * j);

                SpawnedObject.transform.position = AB;
                SpawnedObject.gameObject.tag = "Chair";
                ChairList.Add(SpawnedObject);

                j++;
            }

            j = 0;
            while (j <= (Item.size.x / 2) / DivideDistance)
            {
                SpawnedObject = Instantiate(SpawnItem, Plane, arPlane.transform.rotation);
                Vector3 AB = Plane;

                AB.y = Item.transform.position.y;

                AB.z -= (DivideDistance * i);

                SpawnedObject.transform.parent = Item.transform;
                AB.x += (DivideDistance * j);
                SpawnedObject.transform.position = AB;
                SpawnedObject.gameObject.tag = "Chair";
                ChairList.Add(SpawnedObject);

                j++;
            }


            SpawnedObject.transform.position = A;


            i++;
        }

        /// right 
        i = 0;
        while (i <= (Item.size.y / 2) / DivideDistance)
        {
            SpawnedObject = Instantiate(SpawnItem, Plane, arPlane.transform.rotation);
            Vector3 A = Plane;

            A.y = Item.transform.position.y;
            SpawnedObject.gameObject.tag = "Chair";
            A.z += (DivideDistance * i);
            ChairList.Add(SpawnedObject);

            SpawnedObject.transform.parent = Item.transform;
            int j = 0;

            while (j <= (Item.size.x / 2) / DivideDistance)
            {
                SpawnedObject = Instantiate(SpawnItem, Plane, arPlane.transform.rotation);
                Vector3 AB = Plane;
                AB.y = Item.transform.position.y;

                AB.z += (DivideDistance * i);

                SpawnedObject.transform.parent = Item.transform;

                AB.x -= (DivideDistance * j);

                SpawnedObject.transform.position = AB;
                SpawnedObject.gameObject.tag = "Chair";
                ChairList.Add(SpawnedObject);
                j++;
            }

            j = 0;
            while (j <= (Item.size.x / 2) / DivideDistance)
            {
                SpawnedObject = Instantiate(SpawnItem, Plane, arPlane.transform.rotation);
                Vector3 AB = Plane;

                AB.y = Item.transform.position.y;


                AB.z += (DivideDistance * i);
                SpawnedObject.transform.parent = Item.transform;
                AB.x += (DivideDistance * j);

                SpawnedObject.transform.position = AB;
                ChairList.Add(SpawnedObject);
                SpawnedObject.gameObject.tag = "Chair";

                j++;
            }








            SpawnedObject.transform.position = A;


            i++;
        }
        // Y AXIS


        foreach (var light in ChairList)
        {

            Vector3 A = light.transform.position;
            A.y -= A.y * 3;

            int World = 0;
            while (World < 5)
            {

                A.x += A.x + World;

                ChairList.Add(Instantiate(Corona, A, arPlane.transform.rotation));
                A.z += A.z + World;
                print("addie");
                ChairList.Add(Instantiate(Corona, A, arPlane.transform.rotation));

                World++;
            }
            Vector3 B = light.transform.position;
            B.y -= B.y * 3;

            light.transform.position = B;
        }






    }
}







// IF EVEN ---------------------------------------------------------------------------------------------------------------------------------------------<<<------------------------------------------------------------------------------------------>>------------------------------ EVEN












