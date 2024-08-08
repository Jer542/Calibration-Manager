using System;
using System.Collections.Generic;


class Program
{
    static List<Instrument> instruments = new List<Instrument>();

    static void Main(string[] args)
    {
        bool exit = false;

        while(!exit)
        {
            Console.WriteLine("Calibration Management System by Jeremy");
            Console.WriteLine("1. Add Instrument");
            Console.WriteLine("2. Record Calibration");
            Console.WriteLine("3. View Calibration History");
            Console.WriteLine("4. List Upcoming Calibrations");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddInstrument();
                    break;
                case "2":
                    RecordCalibration();
                    break;
                case "3":
                    ViewCalibrationHistory();
                    break;
                case "4":
                    ListUpcomingCalibrations();
                    break;
                case "5":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void RecordCalibration()
    {
        Console.WriteLine("Enter Instrument ID to record calibration:");
        string id = Console.ReadLine();

        Instrument instrument = instruments.Find(inst => inst.InstrumentID == id);

        if (instrument != null)
        {
            Console.WriteLine("Enter Calibration Date (yyyy-mm-dd):");
            DateTime calDate = DateTime.Parse(Console.ReadLine());  // Declare and initialize calDate

            Console.WriteLine("Enter Technician Name:");
            string technician = Console.ReadLine();  // Declare and initialize technician

            Console.WriteLine("Did the instrument pass the calibration? (yes/no):");
            bool passed = Console.ReadLine().ToLower() == "yes";

            Console.WriteLine("Enter Calibration Data:");
            string data = Console.ReadLine();

            instrument.CalibrationRecords.Add(new CalibrationRecord(calDate, technician, passed, data));
            instrument.LastCalibrationDate = calDate;

            Console.WriteLine("Calibration recorded successfully.");
        }
        else
        {
            Console.WriteLine("Instrument not found.");
        }
    }



    static void ViewCalibrationHistory()
    {
        Console.WriteLine("Enter Instrument ID to view calibration history:");
        string id = Console.ReadLine();
        
        Instrument instrument = instruments.Find(inst => inst.InstrumentID == id);
        
        if (instrument != null)
        {
            Console.WriteLine($"Calibration history for {instrument.Name} (ID: {instrument.InstrumentID}):");
            foreach (var record in instrument.CalibrationRecords)
            {
                Console.WriteLine($"Date: {record.CalibrationDate}, Technician: {record.TechnicianName}, Passed: {record.Passed}, Data: {record.CalibrationData}");
            }
        }
        else
        {
            Console.WriteLine("Instrument not found.");
        }
    }

    static void ListUpcomingCalibrations()
    {
        Console.WriteLine("Instruments due for calibration within the next 30 days:");
        DateTime now = DateTime.Now;
        
        foreach (var instrument in instruments)
        {
            DateTime nextCalibrationDue = instrument.LastCalibrationDate.AddMonths(instrument.CalibrationFrequencyInMonths);
            if (nextCalibrationDue <= now.AddDays(30))
            {
                Console.WriteLine($"Instrument: {instrument.Name} (ID: {instrument.InstrumentID}), Next Calibration Due: {nextCalibrationDue.ToShortDateString()}");
            }
        }
    }

    static void AddInstrument()
    {
        Console.WriteLine("Enter the instrument ID: ");
        string id = Console.ReadLine();

        Console.WriteLine("Enter the instrument name: ");
        string name = Console.ReadLine();

        Console.WriteLine("Enter the instrument manufacturer: ");
        string manufacturer = Console.ReadLine();

        Console.WriteLine("Enter the instrument model number: ");
        string modelNumber = Console.ReadLine();

        Console.WriteLine("Enter the calibration frequency in months: ");
        int frequency = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter the last calibration date: ");
        DateTime lastCalDate = DateTime.Parse(Console.ReadLine());

        instruments.Add(new Instrument(id, name, manufacturer, modelNumber, frequency, lastCalDate));

        Console.WriteLine("Instrument added successfully!" + "\n" + name + " " + modelNumber + " " + "has been added to the system.");
    }
}


// Create a class for the intrument and calibration record

public class Instrument 
{
    public string InstrumentID { get; set; }
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public string ModelNumber { get; set; }
    public int CalibrationFrequencyInMonths { get; set; }
    public DateTime LastCalibrationDate { get; set; }

    // Constructor for the Instrument class
    // similar to a method but is called when an instance of the class is created
    // in python __init__ is the constructor (self.__init__), in C# it is the class name
    // the constructor is used to initialize the object
    // the constructor is called when the object is created
    // the constructor is used to set the initial values of the object
    public Instrument(string instrumentID, string name, string manufacturer, string modelNumber, int calibrationFrequencyInMonths, DateTime lastCalibrationDate)
    {
        InstrumentID = instrumentID;
        Name = name;
        Manufacturer = manufacturer;
        ModelNumber = modelNumber;
        CalibrationFrequencyInMonths = calibrationFrequencyInMonths;
        LastCalibrationDate = lastCalibrationDate;
    }

    public List<CalibrationRecord> CalibrationRecords { get; set; } = new List<CalibrationRecord>();


}

public class CalibrationRecord 
{
    public DateTime CalibrationDate { get; set; }
    public string TechnicianName { get; set; }
    public bool Passed { get; set; }
    public string CalibrationData { get; set; }

    public CalibrationRecord(DateTime calibrationDate, string technicianName, bool passed, string calibrationData)
    {
        CalibrationDate = calibrationDate;
        TechnicianName = technicianName;
        Passed = passed;
        CalibrationData = calibrationData;
    }
}



