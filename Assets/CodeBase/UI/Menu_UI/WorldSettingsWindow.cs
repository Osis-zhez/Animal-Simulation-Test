using System;
using CodeBase.Architecture.Services.PersistentProgress;
using CodeBase.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Menu_UI
{
   public class WorldSettingsWindow : MonoBehaviour, ISavedProgress
   {
      [SerializeField] private TextMeshProUGUI _mapSizeText;
      [SerializeField] private TextMeshProUGUI _animalsText;
      [SerializeField] private TextMeshProUGUI _velocityText;
      
      [Header("Map Settings")]
      [SerializeField] private Slider _mapSizeSlider;
      [SerializeField] private int _minMapSize;
      [SerializeField] private int _maxMapSize;

      [Header("Animal Settings")] 
      [SerializeField] private Slider _animalsSlider;
      [SerializeField] private Slider _velocitySlider;
      
      private int _animals;
      private int _velocity;
      private int _mapSize;

      private void Start()
      {
         _mapSizeSlider.minValue = _minMapSize;
         _mapSizeSlider.maxValue = _maxMapSize;
      }

      private void Update()
      {
         _animalsSlider.maxValue = (float)_mapSize * _mapSize / 2;
         
         _mapSize = (int) _mapSizeSlider.value;
         _animals = (int) _animalsSlider.value;
         _velocity = (int) _velocitySlider.value;

         _mapSizeText.text = _mapSize.ToString();
         _animalsText.text = _animals.ToString();
         _velocityText.text = _velocity.ToString();
      }

      public void LoadProgress(PlayerProgress progress)
      {
         
      }

      public void UpdateProgress(PlayerProgress progress)
      {
         progress.WorldData.MapSize = _mapSize;
         progress.WorldData.Animals = _animals;
         progress.WorldData.Velocity = _velocity;
      }
   }
}