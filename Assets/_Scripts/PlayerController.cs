using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Material invincible;
    private Material initMaterial;
    public float speed;
    public float maxVeclocity;
    public float constanceForce;
    
    public Material colorMaterial;
    public GameObject spherePieces;

    public Camera fixedCamera;

    private Rigidbody rb;

    private Vector3 lastPoint;
    private Plane plane;
    private bool isButtonDown;
    private bool started;

    private bool isdead;

    public static PlayerController instance;

    [SerializeField]
    private GameObject hitExplode;

    [SerializeField]

    bool isInvincible;
    private ParticleSystemRenderer particleRenderer;

    private string sceneName;

    private Vector3 revivePosition;
    private GameObject piecesObject;

    private void Awake()
    {
        Time.timeScale=1f;
        instance = this;
        sceneName=SceneManager.GetActiveScene().name;
    }

    public bool Isdead
    {
        get{return isdead;}
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        plane = new Plane(Vector3.up, Vector3.zero);

        #if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            //speed *= 1.3f;
        #endif
    }

    void FixedUpdate()
    {
        if (!started) return;

        if (isButtonDown)
        {
            var ray = fixedCamera.ScreenPointToRay(Input.mousePosition);
            float ent;

            if (plane.Raycast(ray, out ent))
            {
                var hitPoint = ray.GetPoint(ent);
                Vector3 movement = (hitPoint - lastPoint);

                rb.AddForce(movement * speed);

                lastPoint = hitPoint;
            }
        }

        rb.AddForce(Vector3.forward * constanceForce);
        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVeclocity);
    }

    public void Revive()
    {
        started=false;
        isdead=false;
        Destroy(piecesObject);
        gameObject.SetActive(true);
        gameObject.transform.position=new Vector3(0,0.3f,revivePosition.z);
    }

    public void Dead()
    {
        isdead=true;
        revivePosition=this.gameObject.transform.position;

        gameObject.SetActive(false);

        piecesObject = Instantiate(spherePieces, transform.position, Quaternion.identity);

        foreach (Transform item in piecesObject.transform)
        {
            item.gameObject.GetComponent<MeshRenderer>().sharedMaterial=this.GetComponent<MeshRenderer>().sharedMaterial;
        }

        var force = rb.velocity * 10f;

        foreach (Transform pieceTr in piecesObject.transform)
        {
            pieceTr.GetComponent<Rigidbody>().AddForce(force);
        }

        GameController.instance.GameOver();
    }

    public void StartPlaying()
    {
        started = true;
        GameController.instance.StartPlaying();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!started && !CUtils.IsPointerOverUIObject())
            {
                StartPlaying();
            }

            var ray = fixedCamera.ScreenPointToRay(Input.mousePosition);
            float ent;

            if (plane.Raycast(ray, out ent))
            {
                lastPoint = ray.GetPoint(ent);
            }

            isButtonDown = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isButtonDown = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Dead();
        }

        if (transform.position.y < -6) Dead();
    }

    public void StupInvincible()
    {
        if(isdead) return;
        //print("God mode");
        initMaterial=this.GetComponent<MeshRenderer>().sharedMaterial;

        this.GetComponent<MeshRenderer>().sharedMaterial=invincible;

        isInvincible=true;

        constanceForce=constanceForce+20;

        StartCoroutine(a());
    }
    IEnumerator a()
    {
        yield return new WaitForSeconds(10.0f);

        isInvincible=false;

        this.GetComponent<MeshRenderer>().sharedMaterial=initMaterial;

        constanceForce=constanceForce-20;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            var meshRenderer = collision.gameObject.GetComponent<MeshRenderer>();
            if (meshRenderer == null) meshRenderer = collision.gameObject.GetComponentInChildren<MeshRenderer>();
            
            if (GameController.instance.isGameComplete)
                return;
            //if (meshRenderer != null && meshRenderer.sharedMaterial == colorMaterial)
            if(!isInvincible&&((meshRenderer.sharedMaterial!=colorMaterial&&this.GetComponent<MeshRenderer>().sharedMaterial==colorMaterial)||(meshRenderer.sharedMaterial==colorMaterial&&this.GetComponent<MeshRenderer>().sharedMaterial!=colorMaterial)))
            {
                if (!Prefs.PlayerNeverDie)
                {
                    //Time.timeScale=0.3f;
                    DieEffect.instance.OnDieEffect();

                    Dead();
                    Handheld.Vibrate();
                    Sound.instance.Play(Sound.Others.HitObject);
                }
            }
            else
            {
                if(sceneName=="Endless_Mode")
                {
                    if(!isInvincible)
                    InvincibleUI.instance.SetProgress(3.0f);
                }
                
                //GameObject hitExplodeObject= Instantiate(hitExplode,collision.transform.position,hitExplode.transform.rotation);
                //hitExplodeObject.GetComponent<ParticleSystem>().startColor=colorMaterial.color;
                //hitExplodeObject.GetComponent<ParticleSystemRenderer>().sharedMaterial=this.GetComponent<MeshRenderer>().sharedMaterial;
            }
        }
    }
}