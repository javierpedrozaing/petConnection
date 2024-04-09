using System;
using System.Diagnostics.Metrics;
using System.Net;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using petConnection.FrontEnd.Repositories;
using petConnection.Share.Entitties;

namespace petConnection.FrontEnd.Pages.Pets
{
	public partial class PetsIndex
	{
        [Inject] private IRepository repository { get; set; } = null!;

        private List<Pet> Pets { get; set; }

        [Inject] private SweetAlertService sweetAlertService { get; set; } = null!;

        [Inject] private NavigationManager navigationManager { get; set; } = null!; // framework component


        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            var responseHttp = await repository.GetAsync<List<Pet>>("api/pets");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            Pets = responseHttp.Response;
        }

        private async Task DeleteAsync(Pet pet)
        {
            var result = await sweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = $"¿Estas seguro de querer borrar esta mascota {pet.Name} ?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true
            });

            var confirm = string.IsNullOrEmpty(result.Value);
            if (confirm)
            {
                return;
            }

            var responseHttp = await repository.DeleteAsync<Pet>($"api/pets/{pet.Id}");

            if (responseHttp.Error)
            {
                if (responseHttp.Httpresponsemessage.StatusCode == HttpStatusCode.NotFound)
                {
                    navigationManager.NavigateTo("/pets");
                }
                else
                {
                    var message = await responseHttp.GetErrorMessageAsync();
                    await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                }
                return;
            }

            await LoadAsync();

            var toast = sweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Mascota borrada con éxito");
        }
    }

    
}

