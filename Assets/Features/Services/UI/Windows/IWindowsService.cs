using Features.Services.UI.Factory;
using Features.UI.Windows.Base;

namespace Features.Services.UI.Windows
{
  public interface IWindowsService : ICleanupService
  {
    void Open(WindowId windowId);
    void Close(WindowId windowId);
    BaseWindow Window(WindowId windowId);
  }
}