using System;
using System.Data;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using petConnection.FrontEnd.Repositories;
using petConnection.FrontEnd.Shared;
using petConnection.Share.Entitties;

namespace petConnection.FrontEnd.Pages.Countries
{
    [Authorize(Roles = "Admin")]
    public partial class CountryCreate
    {
        private FormWithName<Country>? countryForm;

        private Country country = new();

        [Inject] private IRepository repository { get; set; } = null!;

        [Inject] private SweetAlertService sweetAlertService { get; set; } = null!;

        [Inject] private NavigationManager navigationManager { get; set; } = null!; // framework component


        private async Task CreateAsync()
        {
            var responseHttp = await repository.PostAsync("/api/countries", country);

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
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "País creado con éxito");
        }

        private void Return()
        {
            countryForm!.FormPostedSuccessfully = true;
            navigationManager.NavigateTo("/countries");
        }
    }
}

