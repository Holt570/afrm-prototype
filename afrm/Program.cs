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

        try //check for invalid input types
        {
            aircraft_type = reader.GetString("aircraft_type");
            planned_fuel = Convert.ToDouble(reader.GetString("planned_fuel"));
            flight_distance = Convert.ToDouble(reader.GetString("flight_distance"));
            load_weight = Convert.ToInt32(reader.GetString("load_weight"));
        } 
        catch
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

    public void Calulate_Fuel()
    {
        //initialise fuel values to be calculated
        int cruise_altitude;
        double takeoff_climb_fuel;
        double cruise_fuel;
        double descent_landing_fuel;

        double load_weight_mod = 1.0 + (load_weight/35000.0); //placeholder value

        //determine cruise altitude based on flight_distance
        if (flight_distance < 1000)
        {
            cruise_altitude = 36000;
        }
        else if (flight_distance < 2000)
        {
            cruise_altitude = 38000;
        }
        else
        {
            cruise_altitude = 40000;
        }

        //cruise altitude modifies fuel numbers in different ways
        if (cruise_altitude == 36000)
        {
            takeoff_climb_fuel = 3000.0; //placeholder
            cruise_fuel = 6.35 * flight_distance; //placeholder
            descent_landing_fuel = 1000.0; //placeholder
        }
        else if (cruise_altitude == 38000)
        {
            takeoff_climb_fuel = 3500.0;
            cruise_fuel = 6.35 * flight_distance * 0.95;
            descent_landing_fuel = 1500.0;
        }
        else
        {
            takeoff_climb_fuel = 4000.0;
            cruise_fuel = 6.35 * flight_distance * 0.9;
            descent_landing_fuel = 2000.0;
        }

        calculated_fuel = (takeoff_climb_fuel + cruise_fuel + descent_landing_fuel) * load_weight_mod;
    }

    public void Output_Result()
    {
        double fuel_diff;

        if (planned_fuel > calculated_fuel)
        {
            fuel_diff = Math.Round(planned_fuel - calculated_fuel, 2);
            Console.WriteLine("Planned fuel exceeds calculated fuel by " + fuel_diff + " litres.");
        }
        else if (calculated_fuel > planned_fuel)
        {
            fuel_diff = Math.Round(calculated_fuel - planned_fuel, 2);
            Console.WriteLine("Calculated fuel exceeds planned fuel by " + fuel_diff + " litres.");
        }
        else
        {
            Console.WriteLine("Planned fuel and calculated fuel are equal.");
        }
    }
}

class Program
{
    static void Main()
    {
        Fuel_Range_Management new_flight = new Fuel_Range_Management();
        string error = new_flight.Read_Input_File("C:/repos/afrm-prototype/afrm/input_data/flight_data_1.csv"); // path to flight data file

        if (error == "")
        {
            new_flight.Calulate_Fuel();
            new_flight.Output_Result();
        }
        else
        {
            Console.WriteLine(error);
        }
    }
} 