namespace PredictiveMaintenance.Constants;

public static class PredictionServiceHelpers
{
    public const string ConnectionString = "http://localhost:5000/";

    //Prediction
    public const string Predict = "predict"; 

    //Model creation
    public const string GetListOfModels = "list_models";
    public const string RetrainModel = "retrain_model";
    public const string LoadModel = "load_model";
    public const string SetModel = "load_model";

    //Csv File creation
    public const string ListCsvFiles = "list_csv_files";
    public const string ExportCsv = "maintenance_data_to_csv";
}