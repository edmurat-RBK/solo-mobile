using UnityEngine;

public class SceneManager : MonoBehaviour
{
    #region Singleton
    public static SceneManager Instance
    {
        get
        {
            if (_instance != null)
                return _instance;

            SceneManager[] managers = FindObjectsOfType(typeof(SceneManager)) as SceneManager[];
            if (managers.Length == 0)
            {
                Debug.LogWarning("GameManager not present on the scene. Creating a new one.");
                SceneManager manager = new GameObject("Scene Manager").AddComponent<SceneManager>();
                _instance = manager;
                return _instance;
            }
            else
            {
                return managers[0];
            }
        }
        set
        {
            if (_instance == null)
                _instance = value;
            else
            {
                Debug.LogError("You can only use one SceneManager. Destroying the new one attached to the GameObject " + value.gameObject.name);
                Destroy(value);
            }
        }
    }
    private static SceneManager _instance = null;
    #endregion

    private GameObject _parentManager;
    public GameObject ParentManager { get { return _parentManager;}}

    private GameObject _parentCamera;
    public GameObject ParentCamera { get { return _parentCamera;}}

    private GameObject _parentLight;
    public GameObject ParentLight { get { return _parentLight;}}

    private GameObject _parentInterface;
    public GameObject ParentInterface { get { return _parentInterface;}}

    private GameObject _parentWorld;
    public GameObject ParentWorld { get { return _parentWorld;}}

    private GameObject _parentDynamic;
    public GameObject ParentDynamic { get { return _parentDynamic;}}

    private void Start() {
        _parentManager = GameObject.Find("----- MANAGERS -----");
        _parentCamera = GameObject.Find("----- CAMERAS -----");
        _parentLight = GameObject.Find("----- LIGHTS -----");
        _parentInterface = GameObject.Find("----- INTERFACE -----");
        _parentWorld = GameObject.Find("----- WORLD -----");
        _parentDynamic = GameObject.Find("----- DYNAMICS -----");
    }
}
