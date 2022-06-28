using Microsoft.AspNetCore.Components;
using System;

namespace PWoLi.Web.Controls.MdalPopup
{
    public class ModalBase : ComponentBase, IDisposable
    {
        [Inject] private ModalService ModalService { get; set; }

        protected bool IsVisible { get; set; }
        protected string Title { get; set; }
        protected RenderFragment Content { get; set; }

        protected override void OnInit()
        {
            ModalService.OnShow += ShowModal;
            ModalService.OnClose += CloseModal;
        }

        public void ShowModal(string title, RenderFragment content)
        {
            Title = title;
            Content = content;
            IsVisible = true;

            base.Invoke(StateHasChanged);
        }

        public void CloseModal()
        {
            IsVisible = false;
            Title = "";
            Content = null;

            base.Invoke(StateHasChanged);
        }

        public void Dispose()
        {
            ModalService.OnShow -= ShowModal;
            ModalService.OnClose -= CloseModal;
        }
    }
}