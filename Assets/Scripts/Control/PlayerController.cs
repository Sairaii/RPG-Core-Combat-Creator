using RPG.Attributes;
using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;
        void Awake() 
        {
            health = GetComponent<Health>();
        }
        void Start() 
        {
            
        }
        void Update()
        {
            if (health.IsDead())
            {
                return;
            }
            //if we attack then just attack not move or vice versa
            if (InteractWithCombat()) return; //I continue, skips over the rest of this body and exit the update
            if (InteractWithMovement()) return;
        }

        bool InteractWithCombat()
        {
            RaycastHit[] Hits = Physics.RaycastAll(GetMouseRay()); //raycastall is to find an object that might be obscured by another
            foreach (RaycastHit hit in Hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null)
                {
                    continue;
                }
                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue; //continue is used when we know the current element in the foreach loop isn't what we're looking for, so we want to move on to the next element.  It just says "Next, please".
                }

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit); //Raycast is a bool type. out is used to store hit variable information into Raycast (allows us to return information about the location that a raycast has hit)
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);//from main camera Returns a ray going from camera through a screen point
        }
    }
}