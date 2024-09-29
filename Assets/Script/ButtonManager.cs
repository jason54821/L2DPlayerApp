using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private H3L2D h3l2d;

    [SerializeField] private Button idle;
    [SerializeField] private Button loop1;
    [SerializeField] private Button loop2;
    [SerializeField] private Button finish;
    [SerializeField] private Button back;


    // Start is called before the first frame update
    void Awake()
    {
        GameObject obj = GameObject.Find("H3L2D");
        h3l2d = obj.GetComponent<H3L2D>();

        idle.onClick.AddListener(OnClick_idle);
        loop1.onClick.AddListener(OnClick_loop1);
        loop2.onClick.AddListener(OnClick_loop2);
        finish.onClick.AddListener(OnClick_finish);
        back.onClick.AddListener(OnClick_back);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClick_idle()
    {
        h3l2d.PlayIdle();
    }

    void OnClick_loop1()
    {
        if (!h3l2d.shootLock) h3l2d.PlayLoop1();
    }

    void OnClick_loop2()
    {
        if (!h3l2d.shootLock) h3l2d.PlayLoop2();
    }

    void OnClick_finish()
    {
        if (!h3l2d.shootLock) h3l2d.PlayShoot();
    }

    void OnClick_back()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Demo_A");
    }
}
