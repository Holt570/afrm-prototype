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

    void Read_Input_File(string file_path)
    {
        
    }

}

class Program
{
    // path to flight data file
    string input_file = "../input_data/flight_data_1.csv";
    static void Main()
    {
        Fuel_Range_Management new_flight = new Fuel_Range_Management();
    }
} 