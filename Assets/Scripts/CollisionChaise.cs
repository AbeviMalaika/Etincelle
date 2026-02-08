using UnityEngine;

public class CollisionChaise : MonoBehaviour
{
    static public bool contactChaise;

    void Start()
    {
        contactChaise = false;
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
