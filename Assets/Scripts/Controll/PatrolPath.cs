using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        private void Start()
        {
            Gizmos.color = Color.white;
        }

        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var nextIndex = GetNextIndex(i);
                Gizmos.DrawLine(GetWaypointPosition(i), GetWaypointPosition(nextIndex));
            }
        }
        public int GetNextIndex(int i)
        {
            return i + 1 == transform.childCount ? 0 : i + 1;
        }

        public Vector3 GetWaypointPosition(int i) => transform.GetChild(i).position;



    }
}