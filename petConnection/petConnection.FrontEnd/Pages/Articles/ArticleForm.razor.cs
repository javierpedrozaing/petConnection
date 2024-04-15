using System;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using petConnection.Share.Entitties;

namespace petConnection.FrontEnd.Pages.Articles
{
	public partial class ArticleForm
	{
		private EditContext editContext = null!;

        private string selectedImage = null!;

		[EditorRequired, Parameter] public Article Article { get; set; } = null;

        [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }

        [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }

        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!;

        public bool FormPostedSuccessfully { get; set; }        

        protected override void OnInitialized()
        {
            editContext = new EditContext(Article);
        }

        private async Task OnBeforeInternalNavigation(LocationChangingContext context)
        {
            var formWasEdited = editContext.IsModified();

            if (!formWasEdited || FormPostedSuccessfully)
            {
                return;
            }

            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = "¿Deseas abandonar la página sin guardar?",
                Icon = SweetAlertIcon.Question,
                ScrollbarPadding = true
            });

            var confirm = string.IsNullOrEmpty(result.Value);

            if (confirm)
            {
                return;
            }

            context.PreventNavigation();
        }

        private async Task HandleFileSelected(InputFileChangeEventArgs e)
        {
            var file = e.File;
            if (file != null)
            {
                var imageStream = file.OpenReadStream(maxAllowedSize: 2 * 1024 * 1024); 
                                                                                       
                var buffer = new byte[imageStream.Length];
                await imageStream.ReadAsync(buffer, 0, (int)imageStream.Length);
                selectedImage = $"data:image/png;base64,{Convert.ToBase64String(buffer)}";
            }
        }
    }
}

