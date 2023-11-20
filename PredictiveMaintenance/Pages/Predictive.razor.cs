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
            await MaintenanceService.CreateMaintenanceDataAsync(newMaintenanceModel);
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

        private bool HasPreviousPage => currentPageIndex > 0;
        private bool HasNextPage => (currentPageIndex + 1) * PageSize < maintenanceModelsList.Count;
    }
}