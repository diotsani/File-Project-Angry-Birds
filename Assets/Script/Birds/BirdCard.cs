using System;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Birds
{
    public class BirdCard : MonoBehaviour
    {
        public GameController gameController;
        public Image birdImage;
        public int birdId;
        public bool isSelected;

        private void Start()
        {
            this.GetComponent<Button>().onClick.AddListener(SelectedCard);
            gameController.OnRemoveBird += RemovedBird;
        }
        
        void SelectedCard()
        {
            if(isSelected)
            {
                gameController.RemoveBird(birdId);
                Destroy(this.gameObject);
            }
        }

        void RemovedBird()
        {
            if(birdId <= 0)return;
            birdId--;
        }
    }
}