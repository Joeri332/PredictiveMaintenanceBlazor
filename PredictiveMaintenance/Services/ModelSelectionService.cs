namespace PredictiveMaintenance.Services
{
    public class ModelSelectionService
    {
        public string SelectedModel { get; private set; }

        public event Action OnModelSelected;

        public bool IsMultiClass { get; set; }

        public void SetModel(string model)
        {
            IsMultiClass = model.Contains("singleclass");
            SelectedModel = model;
            OnModelSelected?.Invoke();
        }
    }

}
