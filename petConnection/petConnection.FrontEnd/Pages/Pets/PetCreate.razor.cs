using System;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using petConnection.FrontEnd.Repositories;
using petConnection.Share.Entitties;

namespace petConnection.FrontEnd.Pages.Pets
{
	public partial class PetCreate
	{
		private PetForm? petForm;

        private Pet pet = new();

        [Inject] private IRepository repository { get; set; }

        [Inject] private SweetAlertService sweetAlertService { get; set; } = null!;

        [Inject] private NavigationManager navigationManager { get; set; } = null!;

        protected override void OnInitialized()
        {
            pet.UserId = 1;
        }

        private async Task CreateAsync()
        {
            var responseHttp = await repository.PostAsync("/api/pets", pet);

            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message);
                return;
            }

            Return();

            var toast = sweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Mascota creado con éxito");
        }

        private void Return()
        {
            petForm!.FormPostedSuccessfully = true;
            navigationManager.NavigateTo("/pets");
        }

    }
}

