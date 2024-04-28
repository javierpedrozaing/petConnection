using System;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using petConnection.Share.Entitties;

namespace petConnection.FrontEnd.Pages.SuccessCases
{
	public partial class SuccessCaseForm
	{
        private EditContext editContext = null!;

        [EditorRequired, Parameter] public SuccessCase SuccessCase { get; set; } = null!;

        [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }

        [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }

        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!;

        public bool FormPostedSuccessfully { get; set; }

        protected override void OnParametersSet()
        {
            editContext = new EditContext(SuccessCase);
        }

        protected override void OnInitialized()
        {
            editContext = new EditContext(SuccessCase);
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
    }
}

