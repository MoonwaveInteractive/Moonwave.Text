# Moonwave.Text
An advanced text formatting library.

Moonwave.Text is a library in the making that allows you to quickly format text. Several features have been planned and are in the process of being added.

# Features
* Creating table formatted text.

## Creating a table
![Table examples.](https://www.dl.dropboxusercontent.com/s/v76vnkgrkbq0q3f/Photo%20Mar%2019%2C%2010%2026%2007%20PM.jpg?dl=0)

Creating customizable tables is fast and easy.

Some of the things you can do with a table inlude:
* Adding columns.
* Adding rows.
* Choosing a header style.
* Choosing a footer style.
* Choosing a guide character.

### Example #1
```c#
Table table = new Table("[Namespace:]", "[Class:]", "[Method:]");

table.Add("Root\\CIMV2", "Win32_Processor", "Name");
table.Add("Root\\CIMV2", "Win32_TemperatureProbe", "CurrentReading");
table.Add("Root\\CIMV2", "Win32_BIOS", "Version");
table.Add("Root\\CIMV2", "Win32_PhysicalMemory", "PositionInRow");
table.Add("Root\\CIMV2", "Win32_PhysicalMemory", "Model");
table.Add("Root\\CIMV2", "Win32_PhysicalMemory", "MaxVoltage");
table.Add("Root\\CIMV2", "Win32_SerialPort", "DeviceID");

// Spacing between columns.
table.ColumnMargin = 2;

// Character to display between cells in a row. Used for added readability.
table.GroupingCharacter = '·';

// Table styling.
table.Header.BorderStyle = HeaderBorderStyle.Column;
table.Footer.BorderStyle = FooterBorderStyle.Dual;
table.Footer.StatsType = FooterStatsType.Advanced;

table.Print();
```

### Example #2
```c#
List<string> failedComponents = new List<string>();

// Imagine having a method that returns WMI information but some
// of the paths you try to get a value from don't exist or return null.
// You would add that bad path to this list during runtime. But for right
// now, we will just do it manually for the sake of the example.
failedComponents.Add("Root\\CIMV2 Win32_Processor Name");
failedComponents.Add("Root\\CIMV2 Win32_TemperatureProbe CurrentReading");
failedComponents.Add("Root\\CIMV2 Win32_BIOS Version");
failedComponents.Add("Root\\CIMV2 Win32_PhysicalMemory PositionInRow");
failedComponents.Add("Root\\CIMV2 Win32_PhysicalMemory Model");
failedComponents.Add("Root\\CIMV2 Win32_PhysicalMemory MaxVoltage");
failedComponents.Add("Root\\CIMV2 Win32_SerialPort DeviceID");

Table table = new Table("[Namespace:]", "[Class:]", "[Method:]");

for (int i = 0; i < failedComponents.Count(); i++)
{
    table.Add(failedComponents[i].Split(' ').ToArray());
}

// Spacing between columns.
table.ColumnMargin = 2;

// Character to display between cells in a row. Used for added readability.
table.GroupingCharacter = '·';

// Table styling.
table.Header.BorderStyle = HeaderBorderStyle.Column;
table.Footer.BorderStyle = FooterBorderStyle.Dual;
table.Footer.StatsType = FooterStatsType.Advanced;

Console.WriteLine(table.ToString());
```
### Output for examples #1 & #2
```
[Namespace:]  [Class:]                [Method:]
────────────  ──────────────────────  ──────────────
Root\CIMV2····Win32_Processor·········Name
Root\CIMV2····Win32_TemperatureProbe··CurrentReading
Root\CIMV2····Win32_BIOS··············Version
Root\CIMV2····Win32_PhysicalMemory····PositionInRow
Root\CIMV2····Win32_PhysicalMemory····Model
Root\CIMV2····Win32_PhysicalMemory····MaxVoltage
Root\CIMV2····Win32_SerialPort········DeviceID
────────────────────────────────────────────────────
Showing [7] entries in [0 ms].
────────────────────────────────────────────────────
```
