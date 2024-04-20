using System;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using petConnection.FrontEnd.Repositories;
using petConnection.Share.Entitties;

namespace petConnection.FrontEnd.Pages.Articles
{
	public partial class ArticleCreate
	{
		private ArticleForm? articleForm;
		private Article article = new();

		[Inject] private IRepository repository { get; set; }
        [Inject] private SweetAlertService sweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager navigationManager { get; set; } = null!;

        protected override void OnInitialized()
        {
            article = new Article();
            article.CreatedAt = DateTime.Now;
            article.UpdatedAt = DateTime.Now;
        }

        private async Task CreateAsync()
        {
            var responseHttp = await repository.PostAsync("api/articles", article);

            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message);
            }

            Return();

            var toast = sweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Artículo creado con éxito");
        }

        private void Return()
        {
            articleForm!.FormPostedSuccessfully = true;
            navigationManager.NavigateTo("/articles");
        }
    }
}

