using System;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using petConnection.FrontEnd.Pages.Pets;
using petConnection.FrontEnd.Repositories;
using petConnection.Share.Entitties;

namespace petConnection.FrontEnd.Pages.SuccessCases
{
	public partial class SuccessCaseCreate
	{
        private SuccessCaseForm? successCaseForm;

        private SuccessCase successCase = new();

        [Inject] private IRepository repository { get; set; }

        [Inject] private SweetAlertService sweetAlertService { get; set; } = null!;

        [Inject] private NavigationManager navigationManager { get; set; } = null!;

        protected override void OnInitialized()
        {
            successCase = new SuccessCase();
            successCase.User_id = 1;

            var pet = new Pet
            {
                Name = "Max",
                Specie = "Perro",
                Race = "PitBull",
                Age = 5,
                Gender = "Macho",
                Size = "120cm",
                Weight = "20 kls",
                Color = "Cafe",
                HealthCondition = "Excelente",
                Behavior = "Calmado",
                Photo = "https://upload.wikimedia.org/wikipedia/commons/6/6b/Icecat1-300x300.svg"
            };
            successCase.Pet = pet;

        }

        private async Task CreateAsync()
        {
            var responseHttp = await repository.PostAsync("/api/successCases", successCase);

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
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Caso de éxito creado con éxito");
        }

        private void Return()
        {
            successCaseForm!.FormPostedSuccessfully = true;
            navigationManager.NavigateTo("/successCases");
        }
    }
}

