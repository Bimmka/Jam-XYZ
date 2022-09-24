using System;
using Features.Constants;
using Features.GameStates;
using Features.Services.Assets;
using Features.Services.StaticData;
using Features.Services.UI.Windows;
using Features.StaticData.Windows;
using Features.UI.Windows.Base;
using Features.UI.Windows.GameMenu;
using Features.UI.Windows.MainMenu;
using UnityEngine;
using Zenject;

namespace Features.Services.UI.Factory.BaseUI
{
  public class UIFactory : IUIFactory
  {
    private readonly IGameStateMachine gameStateMachine;
    private readonly IAssetProvider assets;
    private readonly IStaticDataService staticData;

    private Transform uiRoot;

    private Camera mainCamera;

    public event Action<WindowId,BaseWindow> Spawned;
    public bool IsCleanedUp { get; private set; }
    
    [Inject]
    public UIFactory(IGameStateMachine gameStateMachine, IAssetProvider assets, IStaticDataService staticData)
    {
      this.gameStateMachine = gameStateMachine;
      this.assets = assets;
      this.staticData = staticData;
    }
    
    public void Cleanup()
    {
      IsCleanedUp = true;
    }


    public void CreateWindow(WindowId id, IWindowsService windowsService)
    {
      WindowInstantiateData config = LoadWindowInstantiateData(id);
      
      if (uiRoot == null)
        CreateUIRoot();
      
      switch (id)
      {
        case WindowId.MainMenu:
          CreateMainMenu(config, gameStateMachine);
          break;
        case WindowId.LevelMenu:
          CreateLevelMenu(config);
          break;
        default:
          CreateWindow(config, id);
          break;
      }
    }

    private void CreateMainMenu(WindowInstantiateData config, IGameStateMachine gameStateMachine)
    {
      BaseWindow window = InstantiateWindow(config, uiRoot);
      ((UIMainMenu)window).Construct(gameStateMachine);
      NotifyAboutCreateWindow(config.ID, window);
    }

    private void CreateLevelMenu(WindowInstantiateData config)
    {
      BaseWindow window = InstantiateWindow(config, uiRoot);
      NotifyAboutCreateWindow(config.ID, window);
    }

    private void CreateUIRoot()
    {
        if (uiRoot != null)
            return;

        UIRoot prefab = assets.Instantiate(GameConstants.UIRootPath).GetComponent<UIRoot>();

        prefab.SetCamera(GetCamera());
        uiRoot = prefab.transform;
    }

    private void CreateWindow(WindowInstantiateData config, WindowId id)
    {
      BaseWindow window = InstantiateWindow(config);
      NotifyAboutCreateWindow(id, window);
    }

    private BaseWindow InstantiateWindow(WindowInstantiateData config)
    {
      BaseWindow window = assets.Instantiate(config.Window, uiRoot);
      window.SetID(config.ID);
      return window;
    }

    private BaseWindow InstantiateWindow(WindowInstantiateData config, Transform parent)
    {
      BaseWindow window = assets.Instantiate(config.Window, parent);
      window.SetID(config.ID);
      return window;
    }

    private void NotifyAboutCreateWindow(WindowId id, BaseWindow window) => 
      Spawned?.Invoke(id, window);

    private WindowInstantiateData LoadWindowInstantiateData(WindowId id) => 
      staticData.ForWindow(id);

    private Camera GetCamera()
    {
      if (mainCamera == null)
        mainCamera = Camera.main;
      return mainCamera;
    }
  }
}