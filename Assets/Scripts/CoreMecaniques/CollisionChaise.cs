using UnityEngine;

public class CollisionChaise : MonoBehaviour
{
    public bool contactChaise;

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

    private void OnTriggerExit(Collider infoCollider)
    {
        if (infoCollider.gameObject.name == "PlayerController")
        {
            contactChaise = false;
        }
    }
}
