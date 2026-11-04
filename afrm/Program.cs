using System.Globalization;
using CsvHelper;

class Fuel_Range_Management
{
    // initialise input values
    string aircraft_type = "";
    double planned_fuel = 0.0;
    double flight_distance = 0.0;
    int load_weight = 0;

    // initialise calculated fuel 
    double calculated_fuel = 0.0;

    public void Read_Input_File(string file_path)
    {
        using var reader = CsvReader.FromFile(file_path);
        
        int type_index = reader.GetFieldIndex("aircraft_type");
        int fuel_index = reader.GetFieldIndex("planned_fuel");
        int distance_index = reader.GetFieldIndex("flight_distance");
        int load_index = reader.GetFieldIndex("load_weight");

        reader.Read();

        aircraft_type = reader.GetString("aircraft_type");
        Console.WriteLine(aircraft_type);
    }

}

class Program
{
    // path to flight data file
    static void Main()
    {
        Fuel_Range_Management new_flight = new Fuel_Range_Management();
        new_flight.Read_Input_File("C:/repos/afrm-prototype/afrm/input_data/flight_data_1.csv"); // path to flight data file
    }
} 