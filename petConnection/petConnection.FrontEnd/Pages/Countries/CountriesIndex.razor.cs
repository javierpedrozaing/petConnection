using System;
using Microsoft.AspNetCore.Components;
using petConnection.FrontEnd.Repositories;
using System.Net;
using CurrieTechnologies.Razor.SweetAlert2;
using petConnection.Share.Entitties;

namespace petConnection.FrontEnd.Pages.Countries
{
    public partial class CountriesIndex // partial significa que hay dos clases que se llaman lo mismo, pero copiladas generan una sola
    {
        [Inject] private IRepository Repository { get; set; } = null!;

        public List<Country>? Countries { get; set; }

        [Inject] private SweetAlertService sweetAlertService { get; set; } = null;

        [Inject] private NavigationManager navigationManager { get; set; } = null!; // framework component

        protected async override Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            var responseHttp = await Repository.GetAsync<List<Country>>("api/countries");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            Countries = responseHttp.Response;
        }

        private async Task DeleteAsync(Country country)
        {

            var result = await sweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = $"¿Estas seguro de querer borrar el país {country.Name} ?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true
            });

            var confirm = string.IsNullOrEmpty(result.Value);
            if (confirm)
            {
                return;
            }

            var responseHttp = await Repository.DeleteAsync<Country>($"api/countries/{country.Id}");

            if (responseHttp.Error)
            {
                if (responseHttp.Httpresponsemessage.StatusCode == HttpStatusCode.NotFound)
                {
                    navigationManager.NavigateTo("/countries");
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
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "País borrado con éxito");
        }
    }
}

