📺 Cable Subscriber Management Application

A Windows desktop application built using C# (WPF) to manage cable TV subscribers.
The app allows you to add, edit, delete, search, and filter subscribers, with data stored locally in a JSON file.

This project is inspired by MS Access–style subscriber management, but implemented using modern C# practices.

✨ Features
✅ Subscriber Management

Add new subscribers

Edit existing subscribers

Delete subscribers

View all subscribers in a grid

🔍 Search & Filters

Search across:

Subscriber Name

Phone Numbers

Nick Name

Rent Amount

Filters:

Status

Area Name

Company Name

All filters work together

📞 Phone Number Handling

Supports two phone numbers per subscriber

Phone numbers are loaded directly from JSON

Editing phone numbers updates UI instantly

💾 Data Storage

Uses JSON file (subscribers.json)

No database required

Data persists between application runs

🖥 UI

Clean, Access-like desktop UI

Labeled input fields

Styled buttons

Watermark search box

Instant UI updates using ObservableCollection

🛠 Tech Stack

Language: C#

UI Framework: WPF (.NET Framework)

Data Storage: JSON

JSON Library: Newtonsoft.Json

Excel Import (optional): ExcelDataReader

Pattern Used: Code-behind (no MVVM for simplicity)

CableSubscriberApp
│
├── MainWindow.xaml              # Main UI (Subscriber list + filters)
├── MainWindow.xaml.cs           # Main logic (search, filters, CRUD)
│
├── SubscriberDialog.xaml        # Add/Edit subscriber dialog UI
├── SubscriberDialog.xaml.cs     # Dialog logic
│
├── Subscriber.cs                # Subscriber model (with change notification)
├── JsonDataService.cs           # JSON load/save logic
│
├── subscribers.json             # Data file (auto-created if missing)
│
└── README.md                    # Project documentation



▶️ How to Run the Application
1️⃣ Prerequisites

Visual Studio (2019 or newer recommended)

.NET Framework installed

Windows OS

2️⃣ Open the Project

Open Visual Studio

Click Open a project or solution

Select the project folder / .sln file

3️⃣ Restore NuGet Packages

If prompted, restore NuGet packages:

Newtonsoft.Json
