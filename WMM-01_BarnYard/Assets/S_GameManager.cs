using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_GameManager : MonoBehaviour
{
    public static S_GameManager _instance;
    //[SerializeField]
    //private bool debug;
    [SerializeField]
    private List<Transform> sequenceSpawnLocations;
    [SerializeField]
    private List<Transform> preFabSpawns;
    [SerializeField]
    public List<GameObject> availableObjects;
    [HideInInspector]
    public bool gameHasStarted = false;
    [HideInInspector]
    private List<GameObject> shuffleRemoved = new List<GameObject>();
    [SerializeField]
    public List<GameObject> availableObjectsParents;
    [SerializeField]
    public List<GameObject> shufflePattern;
    [SerializeField]
    public List<GameObject> clickedObjects;
    [SerializeField]
    private GameObject winScreen;
    [SerializeField]
    private GameObject failScreen;
    [SerializeField]
    private GameObject hideSequence;
    [SerializeField]
    private GameObject showPreFabs;
    private bool StartScreen = true;
    [SerializeField]
    private GameObject startButton;
    [SerializeField]
    private GameObject resetButton;
    [SerializeField]
    private GameObject startingScreen;
    [SerializeField]
    private GameObject Storymode;
    [SerializeField]
    private GameObject Hardcore;
    [SerializeField]
    private GameObject Title;
    [SerializeField]
    private TextMeshProUGUI Level;
    public LayerMask Trigger;
    [SerializeField]
    public AudioSource Oink;
    



    [SerializeField]
    private int countdownTime;
    [SerializeField]
    private TextMeshProUGUI countdownText;
    [SerializeField]
    private TextMeshProUGUI Memorize;
    [SerializeField]
    //private GameObject countdownScreen;


    //create prefabs for the memory objects and for loop them ... for int = 0 < pos.count



    public void Shuffle()//(GameObject[] availableObjects)
    {
        //if (shuffleRemoved.Count > 0)
        //{
        //    for (int i = 0; i < shuffleRemoved.Count; i++)
        //    {
        //        availableObjectsParents.Add(shuffleRemoved[i]);
        //        shuffleRemoved.Remove(shuffleRemoved[i]);
        //    }

        //}

        for (int i = 0; i < sequenceSpawnLocations.Count; i++)
        {
            Transform pos = sequenceSpawnLocations[i];
            int randomObject = Random.Range(0, availableObjectsParents.Count);
            GameObject go = availableObjectsParents[randomObject];

            Instantiate(go, pos.localPosition, Quaternion.identity, pos);

            shuffleRemoved.Add(availableObjectsParents[randomObject]);
            shufflePattern.Add(availableObjectsParents[randomObject]);
            availableObjectsParents.Remove(availableObjectsParents[randomObject]);


     
        }


    }

    public void doExitGame()
    {
        Application.Quit();
    }


    private void DestroyObjects()
    {
        
        foreach (Transform _tr in sequenceSpawnLocations)
        {
            //only use when you know how many kids there are
           
            for (int i = 0; i < _tr.childCount; i++)
            {
                Destroy(_tr.GetChild(i).gameObject);
            }
        }

    }

    private void ShufflePatternReset()
    {
        shufflePattern.Clear();
    }

    public void MainMenu()
    {
        gameHasStarted = false;
        startingScreen.SetActive(true);
        resetButton.SetActive(false);
        winScreen.SetActive(false);
        failScreen.SetActive(false);
        Title.SetActive(true);
        startButton.SetActive(true);
        StartScreen = true;
    }
    public void ResetGame()
    {
        StartCoroutine(StartGameOP());
        winScreen.SetActive(false);
        failScreen.SetActive(false);
        resetButton.SetActive(false);
        Level.text = (null);
        Storymode.SetActive(false);
        Hardcore.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        //creating singleton
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
        Level.text = ("Storymode");
     
        startingScreen.GetComponent<AudioSource>().Play();
    }

    public void StartGame()
    {
        StartCoroutine(StartGameOP());
        startingScreen.SetActive(false);
        StartScreen = false;
        Storymode.SetActive(false);
        Hardcore.SetActive(false);
        Level.text = (null);
        Title.SetActive(false);
    }

   
    // Update is called once per frame
    void Update()
    {
        if (gameHasStarted)
        {


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          /*  if (Physics.Raycast(ray, out hit))
            {
                Oink = hit.transform.gameObject.GetComponent<AudioSource>();

            }
*/

            if (Input.GetMouseButtonDown(0))
            {


                

                if (Physics.Raycast(ray, out hit))
                {
                    //All affe
                    Debug.Log(hit.transform.gameObject.name);
                   
                   

                    if (hit.collider.isTrigger)
                    {
                        
                        GameObject hitObject = hit.transform.gameObject;
                        

                        clickedObjects.Add(hitObject);

                        

                        hitObject.SetActive(false);

                    }
                   

                    
                }
                
            }
        }
        if (shuffleRemoved.Count > 0)
        {
            for (int i = 0; i < shuffleRemoved.Count; i++)
            {
                availableObjectsParents.Add(shuffleRemoved[i]);
                shuffleRemoved.Remove(shuffleRemoved[i]);
             
            }

        }

        if (gameHasStarted )
        {
            if (clickedObjects.Count == shufflePattern.Count)
            {
                MatchCheck();
                gameHasStarted = false;
                resetButton.SetActive(true);
                Hardcore.SetActive(true);
                Storymode.SetActive(true);
            }
        }
        
      

        if (!StartScreen)
        {
            startButton.SetActive(false);
        }

   





    }
    public void Audio()
    {
        Oink.GetComponent<AudioSource>().Play();
    }
    public void EasyMode()
    {
        countdownTime = 10;
        Level.text = ("Storymode");
     
    }
    public void HardCore()
    {
        countdownTime = 1;
        Level.text = ("HardCore");
    }
    private void MatchCheck()
    {

        for(int i = 0; i < clickedObjects.Count; i++)
        {
            if (clickedObjects[i] != shufflePattern[i])
            {
                failScreen.SetActive(true);
            
                
                failScreen.GetComponent<AudioSource>().Play();
               
            
                return;
            }
   
        }

        winScreen.SetActive(true);
        winScreen.GetComponent<AudioSource>().Play();


    }




    IEnumerator StartGameOP()
    {
     
        resetButton.SetActive(false);

        clickedObjects.Clear();

        hideSequence.SetActive(true);

        showPreFabs.SetActive(false);

        for (int i = 0; i < availableObjectsParents.Count; i++)
        {
            GameObject go = availableObjectsParents[i];
            go.SetActive(true);
        }

        DestroyObjects();

        ShufflePatternReset();



        yield return new WaitForSeconds(1f);

        Shuffle();

        Memorize.text = "Memorize This Sequence!";

        countdownText.text = countdownTime.ToString();
        //countdownScreen.SetActive(true);
        
        for (int i = countdownTime; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "Start!";

        yield return new WaitForSeconds(1f);

        hideSequence.SetActive(false); 
        Memorize.text = (null);
        

        yield return new WaitForEndOfFrame();

        showPreFabs.SetActive(true);
        countdownText.text = (null);
        gameHasStarted = true;

        yield break;
    }
    public GameObject hitObject;
    public LayerMask IgnoreMe;
    private MemoryObject _memObject;
    private MemoryObject _oldMemObject;
    [SerializeField]
    private List<GameObject> catchObject;
    [SerializeField]
    public List<GameObject> objectList;
    private RaycastHit hit;





    //public float scale = 2;
    //bool isHoverOn = false;

    

    private void FixedUpdate()
    {
        if (gameHasStarted)
        {
            //This is your raycast

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            
         
            

            if (!Physics.Raycast(ray, out hit))
            {
                /*  if (hitObject == catchObject[0])
                  {
                      if (_memObject != null)
                      {
                          _memObject.SetHoverState(false);
                          _memObject = null;
                      }
                  }*/
                if (_memObject != null)
                {
                    _memObject.SetHoverState(false);
                    _memObject = null;
                }
                hitObject = null;

                Debug.Log("Not Hitting anything");
            }
            else //If we aren't hovering, reset the scale of our object and clear it from memory
            {
                if (hit.collider.tag.Equals("MemObject"))
                {
                    hitObject = hit.transform.gameObject;
                   
                }

                //This determines which GameObject is being hit and stores it in a list as well as the previous hitobject


                catchObject.Add(hitObject);

                if (catchObject.Count > 1)
                {
                    catchObject.Remove(catchObject[1]);

                }

                if (hitObject != catchObject[0])
                {
                    if (objectList.Count > 0)
                    {
                        objectList.Clear();
                    }

                    objectList.Add(catchObject[0]);
                    catchObject.Clear();
                }



                //This Sets the previous hitobjects hoverstate to false 
                if (objectList.Count > 0)
                {
                    _oldMemObject = objectList[0].GetComponent<MemoryObject>();
                    _oldMemObject.SetHoverState(false);
                }




                Debug.Log("Hitting something");
                //Try to get a component
                try
                {
                    _memObject = catchObject[0].GetComponent<MemoryObject>();
                }
                catch
                {
                    Debug.Log("There is no component");
                }

                //If we got it, set the hoverstate
                if (_memObject != null)
                {
                    _memObject.SetHoverState(true);
                }








            }
        }
        
        

      
    }

}





