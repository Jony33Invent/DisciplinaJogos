using System;
using MWP.Misc;
using MWP.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace MWP.Interactables
{
    [DisallowMultipleComponent]
    public class InteractablePlot : Interactable
    {
        [SerializeField] private SpriteRenderer defaultRenderer;
        [FormerlySerializedAs("_plantSelectionPopup")] [SerializeField] private GameObject plantSelectionPopup;
        private int _interactingId = -1;
        private bool _isLocked;

        private Plant _plant;
        [SerializeField] private Sprite[] groundSprites;
        private bool _isWatered = false;
        [SerializeField] private GameObject plantFx;
        [SerializeField] private GameObject waterFx;

        // TODO: Quando no teclado não rega(?)
        public override void Interact(Character character)
        {
            if (_plant == null)
            {
                if (_isLocked) return;
                //Seleção de plantas
                _isLocked = true;
                _interactingId = character.OwnerId;

                var popupManager = Instantiate(plantSelectionPopup, PlayerController.SActivePlayers[_interactingId - 1].playerCanvas.transform).
                    GetComponent<PlantGridLayoutManager>();
                
                popupManager.OnChoosePlant += SetPlant;
                popupManager.Initialize(_interactingId);
            }
            else
            {
                //Caso a planta não esteja madura
                if (_plant.Growth < _plant.GrowthTime)
                {
                    if (!_isWatered)
                    {
                        Instantiate(waterFx, transform);
                        _plant.WaterPlant();
                        defaultRenderer.sprite = groundSprites[1];
                        _isWatered = true;
                        defaultRenderer.material.SetInt(UseOutline, 0);
                    }
                }

                else 
                {
                    _plant.Use(character);
                    _plant = null;
                    defaultRenderer.material.SetInt(UseOutline, 1);
                }
            }
        }
        

        private void SetPlant(GameObject plantPrefab)
        {
            if (plantPrefab != null && _plant == null)
            {
                Instantiate(plantFx,transform);
                var plantObject = Instantiate(plantPrefab, transform);
                _plant = plantObject.GetComponent<Plant>();

            }

            _isLocked = false;
            _interactingId = -1;
        }

        private void DeWater()
        {
            defaultRenderer.sprite = groundSprites[0];
            _isWatered = false;
        }

        public override void Enter()
        {
            base.Enter();
            if (_plant != null && _plant.IsFullyGrown)
            {
                _plant.Glow();
            }
            else if(!_isWatered)
            {
                defaultRenderer.material.SetInt(UseOutline, 1);
            }
            
        }

        public override void Exit()
        {
            base.Exit();
            if (_plant != null && _plant.IsFullyGrown)
            {
                _plant.UnGlow();
            }
            
            defaultRenderer.material.SetInt(UseOutline, 0);
        }

        protected override void Start()
        {
            base.Start();
            GameEvents.Instance.OnWaveEnd += DeWater;
        }

        private void OnDestroy()
        {
            GameEvents.Instance.OnWaveEnd -= DeWater;
        }
    }
}