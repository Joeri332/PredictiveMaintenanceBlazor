using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PredictiveMaintenance.Enums;
using PredictiveMaintenance.Models;
using PredictiveMaintenance.Services;
using System;
using System.Diagnostics;
using System.Reflection.Metadata;

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
                GenerateRandomCsvDataToDatabase();
            }
            catch (Exception ex)
            {
                errorMessage = "Error loading data: " + ex.Message;
            }
        }

        private async Task DeleteRecord(int itemUdi)
        {
            try
            {
                await MaintenanceService.DeleteMaintenanceDataAsync(itemUdi);
                maintenanceModelsList.RemoveAll(x => x.UDI == itemUdi);
                await UpdatePaginatedList();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                // Handle any errors, perhaps logging them and setting an error message
                errorMessage = "Error deleting record: " + ex.Message;
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

        private static Random random = new Random();
        public static string getRandomEngineEnum()
        {
            Array values = Enum.GetValues(typeof(EngineEnums));
            EngineEnums randomEngine = (EngineEnums)values.GetValue(random.Next(values.Length));
            return randomEngine.ToString();
        }

        private List<PredictiveMaintenanceModel> records = new List<PredictiveMaintenanceModel>();
        private int randomRecordSeconds = 5;
        private Timer recordGenerationTimer;
        private void OnTimerElapsed(Object stateInfo)
        {
            GetRandomRecord();
            // Depending on your needs, you might want to restart or stop the timer here
        }
        private void GenerateRecordsOnTimer(int sec)
        {
            // Convert seconds to milliseconds
            int interval = sec * 1000;

            // Create a timer that waits the specified interval, then calls OnTimerElapsed
            // AutoReset is false so that the timer runs only once
            recordGenerationTimer = new Timer(OnTimerElapsed, null, interval, Timeout.Infinite);

        }
        private async void GetRandomRecord()
        {
            if (records.Count > 0)
            {
                Random rnd = new Random();
                int index = rnd.Next(records.Count);
                await HandleRecords(records[index]);
            }

        }

        private void GenerateRandomCsvDataToDatabase()
        {
            string filePath = "Database/PredictiveMaintenance.csv";

            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    var record = new PredictiveMaintenanceModel
                    {
                        ProductID = getRandomEngineEnum(),
                        Type = GetType(values[2]),
                        AirTemperature = double.TryParse(values[3], out double airTemp)
                            ? Math.Round(airTemp / 10 - 273.15, 1) : 0,
                        ProcessTemperature = double.TryParse(values[4], out double procTemp)
                            ? Math.Round(procTemp / 10 - 273.15, 1) : 0,
                        RotationalSpeed = int.TryParse(values[5], out int rotSpeed) ? rotSpeed : 0,
                        Torque = float.TryParse(values[6], out float torque) ? torque : 0,
                        ToolWear = int.TryParse(values[7], out int toolWear) ? toolWear : 0
                    };

                    records.Add(record);
                }
            }
        }
        public string GetType(string type)
        {
            return type switch
            {
                "L" => "1",
                "M" => "2",
                "H" => "3",
                _ => "0"
            };
        }

        private async Task HandleValidSubmit()
        {
            await HandleRecords(newMaintenanceModel);
            StateHasChanged();
        }

        private async Task HandleRecords(PredictiveMaintenanceModel model)
        {
            try
            {
                int prediction = await PredictionService.GetPredictionAsync(model.ToDto());

                if (!ModelSelectionService.IsMultiClass)
                {
                    model.FailuresEnums = EnumExtensions.GetEnumById(prediction);
                }
                else
                {
                    model.FailuresEnums = EnumExtensions.GetEnumById(6);
                }

                model.PredictionFromModel = prediction;
                var a = Enum.TryParse<EngineEnums>(model.ProductID, out var engineEnum);
                SetEngineColor(engineEnum, prediction > 0);
                await MaintenanceService.CreateMaintenanceDataAsync(model);
                maintenanceModelsList.Insert(0, model);
                newMaintenanceModel = new PredictiveMaintenanceModel();
                await UpdatePaginatedList();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                errorMessage = "Error processing submission: " + ex.Message;
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
    }
}
