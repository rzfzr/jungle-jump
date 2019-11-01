using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InfiniteHopper
{
    public class IPHHighscores : MonoBehaviour
    {
        /// <summary>
        /// Reference to score prefab that instantiate in list of highscores
        /// </summary>
        [SerializeField]
        private IPHScore scorePrefab;
        /// <summary>
        /// Reference to list of highscores
        /// </summary>
        [SerializeField]
        private Transform list;

        // Start is called before the first frame update
        void OnEnable()
        {
            ClearList();

            //TODO: Replace this mock data with real data.
            int[] MockData = new int[10] { 1000, 700, 500, 400, 300, 200, 150, 120, 100, 100 };
            for (int i = 0; i < MockData.Length; i++)
            {
                GameObject go = Instantiate(scorePrefab.gameObject) as GameObject;
                go.transform.SetParent(list, false);

                IPHScore score = go.GetComponent<IPHScore>();
                score.Score = MockData[i].ToString();
            }
        }

        private void ClearList()
        {
            foreach (Transform t in list)
                Destroy(t.gameObject);
        }
    }
}
