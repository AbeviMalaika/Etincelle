using UnityEngine;

public class CollisionChaise : MonoBehaviour
{
    static public bool contactChaise;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        contactChaise = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider infoCollider)
    {
        if (infoCollider.gameObject.name == "PlayerController")
        {
            contactChaise = true;
            print("Contact avec la chaise");
        }
    }
}
