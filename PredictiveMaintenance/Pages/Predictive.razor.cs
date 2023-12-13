using Microsoft.JSInterop;
using PredictiveMaintenance.Enums;
using PredictiveMaintenance.Models;

namespace PredictiveMaintenance.Pages
{
    public partial class Predictive
    {
        private PredictiveMaintenanceModel newMaintenanceModel = new PredictiveMaintenanceModel();
        private List<PredictiveMaintenanceModel> maintenanceModelsList;
        private List<PredictiveMaintenanceModel> paginatedMaintenanceModelsList;
        private int currentPageIndex = 0;
        private const int PageSize = 5;

        protected override async Task OnInitializedAsync()
        {
            maintenanceModelsList = await MaintenanceService.GetMaintenanceDataAsync();
            UpdatePaginatedList();
        }

        private async Task HandleValidSubmit()
        {
            // This method gets the prediction from the api of python script.          
            int prediction = await PredictionService.GetPredictionAsync(newMaintenanceModel.ToDto());

            newMaintenanceModel.FailuresEnums = EnumExtensions.GetEnumById(prediction);
            await MakeRed(prediction);
            await MaintenanceService.CreateMaintenanceDataAsync(newMaintenanceModel);
            maintenanceModelsList.Insert(0, newMaintenanceModel);
            newMaintenanceModel = new PredictiveMaintenanceModel();
            UpdatePaginatedList();
        }

        private async Task MakeRed(int prediction)
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

        private void UpdatePaginatedList()
        {
            paginatedMaintenanceModelsList = maintenanceModelsList
                .Skip(currentPageIndex * PageSize)
                .Take(PageSize)
                .ToList();
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
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("ThreeJSFunctions.load");
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

        private bool HasPreviousPage => currentPageIndex > 0;
        private bool HasNextPage => (currentPageIndex + 1) * PageSize < maintenanceModelsList.Count;
        /*
        private async void GenerateData(int a)
        {
            PredictiveMaintenanceModel newModel = new PredictiveMaintenanceModel();
            int prediction;
            switch (a)
            {
                case 1:      
                    newModel = GenerateFakeModels.GeneratePWF();
                    prediction = await PredictionService.GetPredictionAsync(newModel.ToDto());
                    newModel.FailuresEnums = EnumExtensions.GetEnumById(prediction);
                    await MakeRed(prediction);
                    await MaintenanceService.CreateMaintenanceDataAsync(newModel);
                    maintenanceModelsList.Insert(0, newModel);
                    UpdatePaginatedList();
                    break;
                case 2:
                    newModel = GenerateFakeModels.GenerateTWF();
                    prediction = await PredictionService.GetPredictionAsync(newModel.ToDto());
                    newModel.FailuresEnums = EnumExtensions.GetEnumById(prediction);
                    await MakeRed(prediction);
                    await MaintenanceService.CreateMaintenanceDataAsync(newModel);
                    maintenanceModelsList.Insert(0, newModel);
                    UpdatePaginatedList();
                    break;
                case 3:
                    newModel = GenerateFakeModels.GenerateOSF();
                    prediction = await PredictionService.GetPredictionAsync(newModel.ToDto());
                    newModel.FailuresEnums = EnumExtensions.GetEnumById(prediction);
                    await MakeRed(prediction);
                    await MaintenanceService.CreateMaintenanceDataAsync(newModel);
                    maintenanceModelsList.Insert(0, newModel);
                    UpdatePaginatedList();
                    break;
                case 4:
                    newModel = GenerateFakeModels.GenerateHDF();
                    prediction = await PredictionService.GetPredictionAsync(newModel.ToDto());
                    newModel.FailuresEnums = EnumExtensions.GetEnumById(prediction);
                    await MakeRed(prediction);
                    await MaintenanceService.CreateMaintenanceDataAsync(newModel);
                    maintenanceModelsList.Insert(0, newModel);
                    UpdatePaginatedList();
                    break;
                case 5:
                    newModel = GenerateFakeModels.GenerateRNF();
                    prediction = await PredictionService.GetPredictionAsync(newModel.ToDto());
                    newModel.FailuresEnums = EnumExtensions.GetEnumById(prediction);
                    await MakeRed(prediction);
                    await MaintenanceService.CreateMaintenanceDataAsync(newModel);
                    maintenanceModelsList.Insert(0, newModel);
                    UpdatePaginatedList();
                    break;
                default:
                    // Handle the case when 'a' is not in the range [1, 5]
                    break;
            }*/
    }
}
