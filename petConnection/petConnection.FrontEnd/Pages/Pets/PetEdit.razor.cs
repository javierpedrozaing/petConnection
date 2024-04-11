using System;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using petConnection.FrontEnd.Repositories;
using petConnection.FrontEnd.Shared;
using petConnection.Share.Entitties;
using System.Net;

namespace petConnection.FrontEnd.Pages.Pets
{
	public partial class PetEdit
	{
        private PetForm? petForm;

        private Pet? pet;

        [Inject] private IRepository repository { get; set; } = null;

        [Inject] private SweetAlertService sweetAlertService { get; set; } = null;

        [Inject] private NavigationManager navigationManager { get; set; } = null; // framework component

        [EditorRequired, Parameter] public int Id { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var responseHttp = await repository.GetAsync<Pet>($"/api/pets/{Id}");

            if (responseHttp.Error)
            {
                if (responseHttp.Httpresponsemessage.StatusCode == HttpStatusCode.NotFound)
                {
                    navigationManager.NavigateTo("/pets/");
                }
                else
                {
                    var message = await responseHttp.GetErrorMessageAsync();
                    await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                }
            }
            else
            {
                pet = responseHttp.Response;
            }
        }

        private async Task EditPetAsync()
        {
            var responseHttp = await repository.PutAsync("/api/pets", pet);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }


            Return();

            var toast = sweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 300
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Cambios guardados con éxito");
        }

        private void Return()
        {
            petForm!.FormPostedSuccessfully = true;
            navigationManager.NavigateTo("/pets");
        }
    }
}

