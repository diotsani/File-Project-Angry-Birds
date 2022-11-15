using System;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Birds
{
    public class SelectBird : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;
        [SerializeField] private Bird _bird;
        [Header("Bird Card")]
        [SerializeField] private string _birdName;
        [SerializeField] private GameObject _birdCardParent;
        private BirdCard _birdCard;
        private Sprite _birdSprite;
        
        private Button _button;

        private void Start()
        {
            _bird = Resources.Load<Bird>("Birds/" + _birdName+"Bird");
            _birdCard = Resources.Load<BirdCard>("Birds/BirdCard");
            _birdSprite = Resources.Load<Sprite>("Sprites/" + _birdName);
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Select);
            
            _gameController.OnMaxBirds += MaxBirds;
        }

        void Select()
        {
            Bird bird = Instantiate(_bird);
            bird.gameObject.SetActive(false);
            _gameController.SelectBird(bird);

            BirdCard birdCard = Instantiate(_birdCard,_birdCardParent.transform);
            birdCard.gameController = this._gameController;
            birdCard.birdId = _gameController.Birds.Count - 1;
            birdCard.name = _birdName + " Bird";
            birdCard.isSelected = true;
            birdCard.birdImage.sprite = _birdSprite;
        }
        void MaxBirds()
        {
            _button.interactable = false;
        }
    }
}