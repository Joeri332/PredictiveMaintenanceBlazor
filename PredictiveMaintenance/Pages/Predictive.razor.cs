using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PredictiveMaintenance.Enums;
using PredictiveMaintenance.Models;
using PredictiveMaintenance.Services;

namespace PredictiveMaintenance.Pages
{
    public partial class Predictive
    {
        private PredictiveMaintenanceModel newMaintenanceModel = new PredictiveMaintenanceModel();
        private List<PredictiveMaintenanceModel> maintenanceModelsList;
        private List<PredictiveMaintenanceModel> paginatedMaintenanceModelsList;
        private int currentPageIndex = 0;
        private const int PageSize = 5;
        private string errorMessage;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                maintenanceModelsList = await MaintenanceService.GetMaintenanceDataAsync();
                UpdatePaginatedList();
            }
            catch (Exception ex)
            {
                errorMessage = "Error loading data: " + ex.Message;
            }
        }

        private async Task HandleValidSubmit()
        {
            try
            {
                int prediction = await PredictionService.GetPredictionAsync(newMaintenanceModel.ToDto());

                if (!ModelSelectionService.IsMultiClass)
                {
                    newMaintenanceModel.FailuresEnums = EnumExtensions.GetEnumById(prediction);
                }
                else
                {
                    newMaintenanceModel.FailuresEnums = EnumExtensions.GetEnumById(6);
                }
     
                await MakeRed(prediction);
                await MaintenanceService.CreateMaintenanceDataAsync(newMaintenanceModel);
                maintenanceModelsList.Insert(0, newMaintenanceModel);
                newMaintenanceModel = new PredictiveMaintenanceModel();
                UpdatePaginatedList();
            }
            catch (Exception ex)
            {
                errorMessage = "Error processing submission: " + ex.Message;
            }
        }

        private async Task MakeRed(int prediction)
        {
            try
            {
                if (prediction > 0)
                {
                    await MakeRedGlow();
                    newMaintenanceModel.PredictionFromModel = 1;
                }
                else
                {
                    await DisableRedGlow();
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Error updating UI: " + ex.Message;
            }
        }

        private Task UpdatePaginatedList()
        {
            paginatedMaintenanceModelsList = maintenanceModelsList
                .Skip(currentPageIndex * PageSize)
                .Take(PageSize)
                .ToList();

            return Task.CompletedTask;
        }

        private void NextPage()
        {
            currentPageIndex++;
            UpdatePaginatedList();
        }

        private void PreviousPage()
        {
            if (currentPageIndex > 0)
            {
                currentPageIndex--;
                UpdatePaginatedList();
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {
                    await JSRuntime.InvokeVoidAsync("ThreeJSFunctions.load");
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Error in after render processing: " + ex.Message;
            }
        }

        public async Task MakeRedGlow()
        {
            await JSRuntime.InvokeVoidAsync("ThreeJSFunctions.applyRedGlow");
        }

        public async Task DisableRedGlow()
        {
            await JSRuntime.InvokeVoidAsync("ThreeJSFunctions.disabledRedGlow");
        }

        private RenderFragment DisplayErrorMessage() => builder =>
        {
            if (!string.IsNullOrEmpty(errorMessage))
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "class", "error-message");
                builder.AddContent(2, errorMessage);
                builder.CloseElement();
            }
        };

        private bool HasPreviousPage => currentPageIndex > 0;
        private bool HasNextPage => (currentPageIndex + 1) * PageSize < maintenanceModelsList.Count;

        private async void GenerateData(int a)
        {
            PredictiveMaintenanceModel newModel = new PredictiveMaintenanceModel();
            int prediction;
            switch (a)
            {
                case 1:
                    newModel = GenerateFakeModels.GeneratePWF();
                    await MaintenanceService.CreateMaintenanceDataAsync(newModel);
                    maintenanceModelsList.Insert(0, newModel);

                    // Run both methods concurrently
                    await Task.WhenAll(UpdatePaginatedList(), MakeRedGlow());
                    break;
                case 2:
                    newModel = GenerateFakeModels.GenerateTWF();
                    await MaintenanceService.CreateMaintenanceDataAsync(newModel);
                    maintenanceModelsList.Insert(0, newModel);

                    await Task.WhenAll(UpdatePaginatedList(), MakeRedGlow());
                    break;
                case 3:
                    newModel = GenerateFakeModels.GenerateOSF();
                    await MaintenanceService.CreateMaintenanceDataAsync(newModel);
                    maintenanceModelsList.Insert(0, newModel);

                    await Task.WhenAll(UpdatePaginatedList(), MakeRedGlow());
                    break;
                case 4:
                    newModel = GenerateFakeModels.GenerateHDF();
                    await MaintenanceService.CreateMaintenanceDataAsync(newModel);
                    maintenanceModelsList.Insert(0, newModel);

                    await Task.WhenAll(UpdatePaginatedList(), MakeRedGlow());
                    break;
                case 5:
                    newModel = GenerateFakeModels.GenerateRNF();
                    await MaintenanceService.CreateMaintenanceDataAsync(newModel);
                    maintenanceModelsList.Insert(0, newModel);

                    await Task.WhenAll(UpdatePaginatedList(), MakeRedGlow());
                    break;
                default:
                    // Handle the case when 'a' is not in the range [1, 5]
                    break;
            }


        }
    }
}
