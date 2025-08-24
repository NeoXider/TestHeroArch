using System;
using R3;                              
using UnityEngine;
using UnityEngine.UIElements;

namespace Presentation.Views.HeroUpgrade
{
    [RequireComponent(typeof(UIDocument))]
    public sealed class HeroView : MonoBehaviour, IHeroUpgradeView
    {
        [SerializeField] private UIDocument _doc;

        [Header("UXML element names")]
        [SerializeField] private string _nameLabel = "HeroNameLabel";
        [SerializeField] private string _levelLabel = "HeroLevelLabel";
        [SerializeField] private string _strengthLabel = "HeroStrengthLabel";
        [SerializeField] private string _upgradeButton = "HeroUpgradeButton";

        private Label _name;
        private Label _level;
        private Label _strength;
        private Button _button;

        private readonly Subject<Unit> _upgradeClicks = new();
        public Observable<Unit> UpgradeClicks => _upgradeClicks.AsObservable();
        
        public void ShowName(string current)
        {
            if (_name != null) _name.text = $"Name: {current}";
        }

        public void ShowLevel(int current)
        {
            if (_level != null) _level.text = $"Level: {current}";
        }

        public void ShowStrength(int current)
        {
            if (_strength != null) _strength.text = $"Strength: {current}";
        }

        private void Awake()
        {
            _doc ??= GetComponent<UIDocument>();
            var root = _doc.rootVisualElement;

            _name = root.Q<Label>(_nameLabel);
            _level = root.Q<Label>(_levelLabel);
            _strength = root.Q<Label>(_strengthLabel);
            _button = root.Q<Button>(_upgradeButton);
        }

        private void OnEnable()
        {
            if (_button != null) _button.clicked += HandleUpgradeClicked;
        }

        private void OnDisable()
        {
            if (_button != null) _button.clicked -= HandleUpgradeClicked;
        }

        public void SetUpgradeInteractable(bool value)
        {
            if (_button != null) _button.SetEnabled(value);
        }

        private void HandleUpgradeClicked()
        {
            _upgradeClicks.OnNext(Unit.Default);
        }
    }
}
