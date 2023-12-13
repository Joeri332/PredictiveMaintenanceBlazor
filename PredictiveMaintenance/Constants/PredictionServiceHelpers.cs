public static class PredictionServiceHelpers
{
    public const string ConnectionString = "http://localhost:5000/";

    //Prediction
    public const string Predict = "predict"; 

    //Model creation
    public const string GetListOfModels = "list_Models";
    public const string RetrainModel = "retrain_model";
    public const string GetNewModel = "load_model";

    //Csv File creation
    public const string ListCsvFiles = "list_csv_files";
    public const string ExportCsv = "export_to_csv";

}