using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePlot : Interactable
{
    [SerializeField] private SpriteRenderer _defaultRenderer;
    [SerializeField] private GameObject _plantSelectionPopup;

    private Plant _plant;
    private bool _isLocked = false;
    private int _interactingId = -1;

    // TODO: Quando no teclado não rega(?)
    protected override void Interact(int id)
    {
        if (_plant == null)
        {
            if (!_isLocked)
            {
                //Seleção de plantas
                _isLocked = true;
                _interactingId = id;
                PlantGridLayoutManager popupManager = Instantiate(_plantSelectionPopup, Player.s_ActivePlayers[id - 1].PlayerCanvas.transform).GetComponent<PlantGridLayoutManager>();
                popupManager.OnChoosePlant += SetPlant;
                popupManager.Initialize(id);
            }
        }
        else
        {
            //Caso a planta não esteja madura
            if (_plant.Growth < _plant.GrowthTime)
            {
                _plant.waterPlant();
            }

            else
            {
                _plant.Use(CharacterList[id - 1]);
                _defaultRenderer.enabled = true;
                _plant = null;
            }
        }
    }

    public void SetPlant(GameObject plantPrefab)
    {
        if (plantPrefab != null)
        {
            GameObject plantObject = Instantiate(plantPrefab, transform);
            _plant = plantObject.GetComponent<Plant>();

            _defaultRenderer.enabled = false;
        }

        _isLocked = false;
        _interactingId = -1;

    }
}