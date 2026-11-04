using System.Globalization;
using System.Runtime.InteropServices;
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

    public string Read_Input_File(string file_path)
    {
        string error_message = "";
        using var reader = CsvReader.FromFile(file_path);

        reader.Read();

        try
        {
            aircraft_type = reader.GetString("aircraft_type");
            planned_fuel = Convert.ToDouble(reader.GetString("planned_fuel"));
            flight_distance = Convert.ToDouble(reader.GetString("flight_distance"));
            load_weight = Convert.ToInt32(reader.GetString("load_weight"));
        } catch
        {
            error_message = "ERROR: Invalid Input";
            return error_message;
        }

        if (aircraft_type == "Boeing 737-800") //check for invalid inputs based on aircraft data
        {
            if (planned_fuel > 26000.0)
            {
                error_message += "ERROR: Planned fuel quantity exceeds aircraft maximum\n";
            } 
            else if (load_weight > 19410)
            {
                error_message += "ERROR: Planned load weight exceeds aircraft maximum\n";
            }
        }
        else
        {
            error_message = "ERROR: Aircraft type not recognised";
        }

        return error_message;
    }
}

class Program
{
    // path to flight data file
    static void Main()
    {
        Fuel_Range_Management new_flight = new Fuel_Range_Management();
        string error = new_flight.Read_Input_File("C:/repos/afrm-prototype/afrm/input_data/flight_data_1.csv"); // path to flight data file

        if (error == "")
        {
            
        }
        else
        {
            Console.WriteLine(error);
        }
    }
} 