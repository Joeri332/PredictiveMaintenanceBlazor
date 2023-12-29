using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PredictiveMaintenance.Enums;
using PredictiveMaintenance.Models;
using PredictiveMaintenance.Services;

namespace PredictiveMaintenance.Pages
{
    public partial class Predictive
    {
        private int svgWidth = 900;
        private int svgHeight = 600;
        private int circleRadius = 30;
        private int circleSpacingX = 175;
        private int circleSpacingY = 120;
        private int startX = 50;
        private int startY = 50;
        private int strokeWidth = 3;

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

        private string[] colors = Enumerable.Repeat("green", 20).ToArray();
        string[] engineTexts = new string[20];
        private string GetColor(EngineEnums engine)
        {
            return colors[(int)engine - 1];
        }
        public void SetEngineColor(EngineEnums engine, bool shouldBeRed)
        {
            int index = (int)engine;
            if (index >= 0 && index < colors.Length) // Checking the bounds to prevent IndexOutOfRangeException
            {
                colors[index] = shouldBeRed ? "red" : "green";
                StateHasChanged();
            }
            else
            {
                // Handle the case when index is out of range, such as logging or setting an error message
                errorMessage = "Invalid engine index: " + index;
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

                newMaintenanceModel.PredictionFromModel = prediction;
                var a = Enum.TryParse<EngineEnums>(newMaintenanceModel.ProductID, out var engineEnum);
                SetEngineColor(engineEnum, prediction > 0);
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
                    var enumParser = Enum.TryParse<EngineEnums>(newModel.ProductID, out var engineEnum);
                    SetEngineColor(engineEnum, newModel.PredictionFromModel > 0);
                    // Run both methods concurrently
                    await Task.WhenAll(UpdatePaginatedList());
                    break;
                case 2:
                    newModel = GenerateFakeModels.GenerateTWF();
                    await MaintenanceService.CreateMaintenanceDataAsync(newModel);
                    maintenanceModelsList.Insert(0, newModel);
                    var enumParser1 = Enum.TryParse<EngineEnums>(newModel.ProductID, out var engineEnum1);
                    SetEngineColor(engineEnum1, newModel.PredictionFromModel > 0);
                    await Task.WhenAll(UpdatePaginatedList());
                    break;
                case 3:
                    newModel = GenerateFakeModels.GenerateOSF();
                    await MaintenanceService.CreateMaintenanceDataAsync(newModel);
                    maintenanceModelsList.Insert(0, newModel);
                    var enumParser2 = Enum.TryParse<EngineEnums>(newModel.ProductID, out var engineEnum2);
                    SetEngineColor(engineEnum2, newModel.PredictionFromModel > 0);
                    await Task.WhenAll(UpdatePaginatedList());
                    break;
                case 4:
                    newModel = GenerateFakeModels.GenerateHDF();
                    await MaintenanceService.CreateMaintenanceDataAsync(newModel);
                    maintenanceModelsList.Insert(0, newModel);
                    var enumParse3r = Enum.TryParse<EngineEnums>(newModel.ProductID, out var engineEnum3);
                    SetEngineColor(engineEnum3, newModel.PredictionFromModel > 0);
                    await Task.WhenAll(UpdatePaginatedList());
                    break;
                case 5:
                    newModel = GenerateFakeModels.GenerateRNF();
                    await MaintenanceService.CreateMaintenanceDataAsync(newModel);
                    maintenanceModelsList.Insert(0, newModel);
                    var enumParse4r = Enum.TryParse<EngineEnums>(newModel.ProductID, out var engineEnum4);
                    SetEngineColor(engineEnum4, newModel.PredictionFromModel > 0);
                    await Task.WhenAll(UpdatePaginatedList());
                    break;
                default:
                    // Handle the case when 'a' is not in the range [1, 5]
                    break;
            }


        }
    }
}
