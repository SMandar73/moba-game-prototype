using UnityEngine;
using MobaPrototype.Core;

namespace MobaPrototype.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public bool MatchEnded { get; private set; }
        public Team WinningTeam { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void EndMatch(Team winningTeam)
        {
            if (MatchEnded) return;
            MatchEnded = true;
            WinningTeam = winningTeam;
            Debug.Log($"Match ended. Winning team: {winningTeam}");
        }
    }
}
