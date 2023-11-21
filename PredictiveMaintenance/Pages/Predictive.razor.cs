using Microsoft.JSInterop;
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

        protected override async Task OnInitializedAsync()
        {
            maintenanceModelsList = await MaintenanceService.GetMaintenanceDataAsync();
            UpdatePaginatedList();
        }

        private async Task HandleValidSubmit()
        {
            // This method gets the prediction from the api of python script.          
           // int prediction = await PredictionService.GetPredictionAsync(newMaintenanceModel.ToDto());
            //Setting the prediction to the value of the mode. because of the await method, this is only continues when having an answer.
            //newMaintenanceModel.PredictionFromModel = prediction;
            await MaintenanceService.CreateMaintenanceDataAsync(newMaintenanceModel);

            if (newMaintenanceModel.PredictionFromModel >= 1 ) {
                await MakeRedGlow();
            }
            else
            {
                await DisableRedGlow();
            }

            maintenanceModelsList.Insert(0, newMaintenanceModel); // Insert at the beginning to show the most recent entry
            newMaintenanceModel = new PredictiveMaintenanceModel(); // Reset the form
            UpdatePaginatedList();
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
    }
}