using Microsoft.AspNetCore.Components;
using PWoLi.Model;
using System;

namespace PWoLi.Web.Controls.MdalPopup
{
    public class ModalService
    {
        public event Action<string, RenderFragment> OnShow;

        public event Action OnClose;

        public SystemModel SelectedSystem { get; set; }

        public ConfigurationObjectModel SelectedConfigurationObjectModel { get; set; }

        public void Show(string title, Type contentType)
        {
            if (contentType.BaseType != typeof(ComponentBase))
            {
                throw new ArgumentException($"{contentType.FullName} must be a Blazor Component");
            }

            var content = new RenderFragment(x => { x.OpenComponent(1, contentType); x.CloseComponent(); });
            OnShow?.Invoke(title, content);
        }

        public void Close()
        {
            OnClose?.Invoke();
        }
    }
}