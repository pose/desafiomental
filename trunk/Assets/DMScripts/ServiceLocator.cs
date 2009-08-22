using UnityEngine;
using System.Collections;

public class ServiceLocator : MonoBehaviour
{
    private static ServiceLocator instance = null;
    private Hashtable h = new Hashtable();
    void Start()
    {   
        DontDestroyOnLoad(this);
        ServiceLocator.instance = this;
    }

    void Update()
    {

    }

    public object Get(string s)
    {
        return h[s];
    }

    public void Set(string s, object o)
    {
        h[s] = o;
    }

    public static ServiceLocator GetInstance()
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load("ServiceLocator")) as ServiceLocator;
        }
        return instance;
    }

}