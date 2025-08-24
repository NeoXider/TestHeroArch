using System;
using System.Threading;
using App.Messages.Commands.HeroUpgrade;
using App.Messages.Dto.HeroUpgrade;
using App.Messages.Events.HeroUpgrade;
using App.Messages.Queries.HeroUpgrade;
using Cysharp.Threading.Tasks;
using MessagePipe;
using Presentation.Views.HeroUpgrade;
using Presentation.ViewModels.HeroUpgrade;
using R3;
using UnityEngine;
using VContainer.Unity;

namespace Presentation.Presenters.HeroUpgrade
{
    public sealed class HeroPanel : IStartable, IDisposable
    {
        private readonly HeroViewModel _vm;
        private readonly IHeroUpgradeView _view;

        private readonly IAsyncRequestHandler<GetHeroDtoQuery, HeroDto> _getHero;
        private readonly IRequestHandler<UpgradeHeroCommand, UpgradeResultDto> _upgradeCommand;

        private readonly ISubscriber<HeroChangedDto> _subscriber;

        private readonly CompositeDisposable _disposables = new();
        private readonly CancellationTokenSource _cts = new();

        private readonly ReactiveProperty<bool> _isBusy = new(false);
        private readonly float _throttleFirst = 0.3f;

        public HeroPanel(
            HeroViewModel vm,
            IHeroUpgradeView view,
            IAsyncRequestHandler<GetHeroDtoQuery, HeroDto> getHero,
            IRequestHandler<UpgradeHeroCommand, UpgradeResultDto> upgradeCommand,
            ISubscriber<HeroChangedDto> subscriber)
        {
            _vm = vm;
            _view = view;
            _getHero = getHero;
            _upgradeCommand = upgradeCommand;
            _subscriber = subscriber;
        }

        public void Start()
        {
            _vm.Name.Subscribe(_view.ShowName).AddTo(_disposables);
            _vm.Level.Subscribe(_view.ShowLevel).AddTo(_disposables);
            _vm.Strength.Subscribe(_view.ShowStrength).AddTo(_disposables);

            _subscriber.Subscribe(OnHeroChanged).AddTo(_disposables);

            _view.UpgradeClicks
                .Select(_ => _isBusy.Value)
                .Where(b => !b)
                .ThrottleFirst(TimeSpan.FromSeconds(_throttleFirst))
                .Subscribe(_ => UpgradeFlow().Forget())
                .AddTo(_disposables);

            _isBusy.Select(b => !b).Subscribe(_view.SetUpgradeInteractable).AddTo(_disposables);

            Initialize().Forget();
        }

        private void OnHeroChanged(HeroChangedDto message)
        {
            _vm.Apply(message.Dto);
        }

        private async UniTaskVoid Initialize()
        {
            _isBusy.Value = true;
            try
            {
                var s = await _getHero
                    .InvokeAsync(new GetHeroDtoQuery(), _cts.Token)
                    .AttachExternalCancellation(_cts.Token);

                _vm.Apply(s);
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                HandleError("Initialize failed", ex);
            }
            finally
            {
                _isBusy.Value = false;
            }
        }

        private async UniTaskVoid UpgradeFlow()
        {
            _isBusy.Value = true;
            try
            {
                var result = _upgradeCommand
                    .Invoke(new UpgradeHeroCommand());
                
                if (result.Success)
                {
                    Debug.Log("Hero upgraded successfully");
                }
                else
                {
                    Debug.Log("Failed to upgrade hero");
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                HandleError("Upgrade failed", ex);
            }
            finally
            {
                _isBusy.Value = false;
            }
        }

        private static void HandleError(string msg, Exception ex)
        {
            Debug.LogError($"[HeroUpgrade] {msg}: {ex}");
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
            _disposables.Dispose();
        }
    }
}