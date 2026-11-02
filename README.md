# RosaryForToday

RosaryForToday is an application containing the Catholic mysteries of the Rosary for each day of the week. It has two tabs:

1. **Today** — the Rosary mysteries for the current day.
2. **Other days** — the Rosary mysteries for the remaining days of the week.

## Classes

### RosaryForToday.Models

| Type | Description |
| --- | --- |
| [RosaryDto](src/RosaryForToday.Models/Dtos/RosaryDto.cs) | Carries a Rosary type identifier, its name, the associated day or days, and its reflections between the data and presentation layers. |
| [RosaryReflectionDto](src/RosaryForToday.Models/Dtos/RosaryReflectionDto.cs) | Carries a single reflection's identifier, Rosary type identifier, title, and content. |
| [RosaryTitleDto](src/RosaryForToday.Models/Dtos/RosaryTitleDto.cs) | Carries the current day's Rosary title and day name for the Android widget. |
| [GetRosaryForTodayQuery](src/RosaryForToday.Models/Queries/GetRosaryForTodayQuery.cs) | Requests today's Rosary and its reflections in the selected language. |
| [GetAllRosariesQuery](src/RosaryForToday.Models/Queries/GetAllRosariesQuery.cs) | Requests the Rosary sets for the other days in the selected language, excluding today's set. |
| [GetRosaryTitleForTodayQuery](src/RosaryForToday.Models/Queries/GetRosaryTitleForTodayQuery.cs) | Requests today's Rosary title and day name in the selected language. |
| [LanguageTypeEnum](src/RosaryForToday.Models/Enums/LanguageTypeEnum.cs) (enum) | Defines the English and Polish language identifiers used by queries and stored data. |

### RosaryForToday.Domain

| Type | Description |
| --- | --- |
| [Language](src/RosaryForToday.Domain/Entities/Language.cs) | Represents a language with its code, name, Rosary types, and reflections. |
| [RosaryType](src/RosaryForToday.Domain/Entities/RosaryType.cs) | Represents a set of Rosary mysteries in a particular language, together with its reflections and weekly schedule. |
| [RosaryReflection](src/RosaryForToday.Domain/Entities/RosaryReflection.cs) | Represents an individual mystery's title and reflection content, linked to a Rosary type and language. |
| [RosaryDaySchedule](src/RosaryForToday.Domain/Entities/RosaryDaySchedule.cs) | Associates a Rosary type and language with a day of the week. |
| [IRosaryDbQuery](src/RosaryForToday.Domain/DbQueries/IRosaryDbQuery.cs) (interface) | Defines data access operations for today's Rosary, the Rosary sets for other days, and today's title. |

### RosaryForToday.ApplicationLayer

| Class | Description |
| --- | --- |
| [GetRosaryForTodayQueryHandler](src/RosaryForToday.Application/QueryHandlers/GetRosaryForTodayQueryHandler.cs) | Handles the asynchronous query for today's Rosary by delegating to `IRosaryDbQuery`. |
| [GetAllRosariesQueryHandler](src/RosaryForToday.Application/QueryHandlers/GetAllRosariesQueryHandler.cs) | Handles the asynchronous query for other days by retrieving the Rosary sets that exclude today's set. |
| [GetRosaryTitleForTodayQueryHandler](src/RosaryForToday.Application/QueryHandlers/GetRosaryTitleForTodayQueryHandler.cs) | Handles the synchronous query for today's Rosary title and day name. |

### RosaryForToday.Infrastructure

| Class | Description |
| --- | --- |
| [RosaryDbContext](src/RosaryForToday.Infrastructure/Data/RosaryDbContext.cs) | Provides Entity Framework Core access to languages, Rosary types, reflections, and schedules, and loads their entity configurations. |
| [DesignTimeDbContextFactory](src/RosaryForToday.Infrastructure/Data/DesignTimeDbContextFactory.cs) | Creates a SQLite-backed `RosaryDbContext` for Entity Framework design-time tools. |
| [RosaryDbQuery](src/RosaryForToday.Infrastructure/DbQueries/RosaryDbQuery.cs) | Implements Rosary data queries, selecting today's set from the UTC day of the week, loading reflections, grouping the remaining sets, and formatting Polish day names. |
| [LanguageConfiguration](src/RosaryForToday.Infrastructure/Data/Configuration/LanguageConfiguration.cs) | Configures language keys, required fields, length limits, and the unique language code, and seeds English and Polish records. |
| [RosaryTypeConfiguration](src/RosaryForToday.Infrastructure/Data/Configuration/RosaryTypeConfiguration.cs) | Configures Rosary type fields and their language relationship, and seeds the Joyful, Sorrowful, Luminous, and Glorious sets in English and Polish. |
| [RosaryReflectionConfiguration](src/RosaryForToday.Infrastructure/Data/Configuration/RosaryReflectionConfiguration.cs) | Configures reflection fields and relationships, and seeds the mystery titles and reflection content. |
| [RosaryDayScheduleConfiguration](src/RosaryForToday.Infrastructure/Data/Configuration/RosaryDayScheduleConfiguration.cs) | Configures schedule relationships and uniqueness, and seeds the weekly assignments for both languages. |
| [SeedDataIds](src/RosaryForToday.Infrastructure/Data/Configuration/SeedDataIds.cs) | Groups the fixed identifiers shared by the seeded language and Rosary records. |
| [SeedDataIds.Languages](src/RosaryForToday.Infrastructure/Data/Configuration/SeedDataIds.cs) | Defines the fixed identifiers for English and Polish seed records. |
| [SeedDataIds.RosaryTypes](src/RosaryForToday.Infrastructure/Data/Configuration/SeedDataIds.cs) | Defines the fixed identifiers for each Rosary set in English and Polish. |
| [Initial](src/RosaryForToday.Infrastructure/Migrations/20251121153002_Initial.cs) | Creates the initial database tables, relationships, indexes, and seed data; its designer partial class records the migration's target model. |
| [Rosary_Reflections_Descriptions](src/RosaryForToday.Infrastructure/Migrations/20251121163820_Rosary_Reflections_Descriptions.cs) | Updates the seeded reflection descriptions; its designer partial class records the migration's target model. |
| [RosaryDbContextModelSnapshot](src/RosaryForToday.Infrastructure/Migrations/RosaryDbContextModelSnapshot.cs) | Records the database model used by Entity Framework when generating subsequent migrations. |

### RosaryForToday.Configuration

| Class | Description |
| --- | --- |
| [DependencyExtension](src/RosaryForToday.Configuration/DependencyExtension.cs) | Registers the SQLite database context, applies pending migrations, and registers the Rosary query implementation and SimpleCqrs handlers. |

### RosaryForToday.Presentation

| Class | Description |
| --- | --- |
| [RosaryListView](src/RosaryForToday.Presentation/Views/RosaryListView.xaml.cs) | Displays today's Rosary and the Rosary sets for other days, binds to `RosaryListViewModel`, and reloads data when the page appears. |
| [CrashLogView](src/RosaryForToday.Presentation/Views/CrashLogView.xaml.cs) | Displays the saved crash log and asks its view model to load the log when the page appears. |
| [RosaryListViewModel](src/RosaryForToday.Presentation/ViewModels/RosaryListViewModel.cs) | Loads the two Rosary collections through SimpleCqrs, maps them to view models, switches tabs, refreshes data, reports errors, and opens the crash log page. |
| [RosaryViewModel](src/RosaryForToday.Presentation/ViewModels/RosaryViewModel.cs) | Holds a Rosary set's identifier, name, description, days, and reflection view models, with expandable details. |
| [RosaryReflectionViewModel](src/RosaryForToday.Presentation/ViewModels/RosaryReflectionViewModel.cs) | Holds a reflection's identifiers, title, and content, with expandable details. |
| [DetailsBaseViewModel](src/RosaryForToday.Presentation/ViewModels/DetailsBaseViewModel.cs) | Supplies the shared details visibility state, toggle command, button text, and property change notifications. |
| [CrashLogViewModel](src/RosaryForToday.Presentation/ViewModels/CrashLogViewModel.cs) | Loads crash log text and exposes it to the view, providing a message when the log is unavailable. |
| [RosaryMapper](src/RosaryForToday.Presentation/Mappers/RosaryMapper.cs) | Declares the Mapperly-generated conversion from `RosaryDto` to `RosaryViewModel`. |
| [RosaryReflectionMapper](src/RosaryForToday.Presentation/Mappers/RosaryReflectionMapper.cs) | Declares Mapperly-generated conversions for individual reflection DTOs and arrays of reflection DTOs. |
| [BoolToIsVisibleConverter](src/RosaryForToday.Presentation/Converters/BoolToIsVisibleConverter.cs) | Converts a Boolean value to a visibility value, optionally reversing it when the `Invert` parameter is supplied. |
| [CrashLogService](src/RosaryForToday.Presentation/Helpers/CrashLogService.cs) | Appends exception details and UTC timestamps to `crash.log` in the app data directory and reads the saved log asynchronously. |
| [PlatformClass1 (Android)](src/RosaryForToday.Presentation/Platforms/Android/PlatformClass1.cs) | Empty placeholder for Android-specific presentation code. |
| [PlatformClass1 (iOS)](src/RosaryForToday.Presentation/Platforms/iOS/PlatformClass1.cs) | Empty placeholder for iOS-specific presentation code. |
| [PlatformClass1 (Mac Catalyst)](src/RosaryForToday.Presentation/Platforms/MacCatalyst/PlatformClass1.cs) | Empty placeholder for Mac Catalyst-specific presentation code. |
| [PlatformClass1 (Windows)](src/RosaryForToday.Presentation/Platforms/Windows/PlatformClass1.cs) | Empty placeholder for Windows-specific presentation code. |
| [PlatformClass1 (Tizen)](src/RosaryForToday.Presentation/Platforms/Tizen/PlatformClass1.cs) | Empty placeholder for Tizen-specific presentation code. |

### RosaryForToday.UI

| Class | Description |
| --- | --- |
| [App](src/RosaryForToday.UI/App.xaml.cs) | Initializes application resources, connects unhandled exception logging, and creates the main window containing `AppShell`. |
| [AppShell](src/RosaryForToday.UI/AppShell.xaml.cs) | Initializes the Shell navigation structure containing the Rosary list and crash log routes. |
| [MauiProgram](src/RosaryForToday.UI/MauiProgram.cs) | Builds the MAUI application, configures fonts and debug logging, sets up the database and services, and exposes the resulting service provider. |
| [ServiceLocator](src/RosaryForToday.UI/ServiceLocator.cs) | Stores the application's service provider so Android widget components can resolve their dependencies. |
| [DependencyExtension](src/RosaryForToday.UI/Configuration/DependencyExtension.cs) | Registers the Rosary list and crash log views and their view models as transient services. |
| [MainActivity](src/RosaryForToday.UI/Platforms/Android/MainActivity.cs) | Hosts the Android MAUI application, declares launcher and configuration behavior, and sets the display cutout mode during creation. |
| [MainApplication](src/RosaryForToday.UI/Platforms/Android/MainApplication.cs) | Initializes the Android application and delegates MAUI app creation to `MauiProgram`. |
| [RosaryTodayAppWidgetProvider](src/RosaryForToday.UI/Platforms/Android/RosaryTodayAppWidgetProvider.cs) | Receives Android widget updates and refresh actions, displays today's Rosary title, connects the reflection list service, and configures refresh and open-app buttons. |
| [RosaryWidgetRemoteViewsService](src/RosaryForToday.UI/Platforms/Android/RosaryWidgetRemoteViewsService.cs) | Supplies the `RosaryListRemoteViewsFactory` used to populate the Android widget's list. |
| [RosaryListRemoteViewsFactory](src/RosaryForToday.UI/Platforms/Android/RosaryListRemoteViewsFactory.cs) | Loads today's Polish mystery titles through SimpleCqrs and creates a `RemoteViews` row for each title, or prompts the user to open the app when services are unavailable. |
| [App (Windows)](src/RosaryForToday.UI/Platforms/Windows/App.xaml.cs) | Initializes the WinUI application and delegates MAUI app creation to `MauiProgram`. |
| [Program (iOS)](src/RosaryForToday.UI/Platforms/iOS/Program.cs) | Provides the iOS entry point and starts UIKit with `AppDelegate`. |
| [AppDelegate (iOS)](src/RosaryForToday.UI/Platforms/iOS/AppDelegate.cs) | Connects the iOS application lifecycle to `MauiProgram`. |
| [Program (Mac Catalyst)](src/RosaryForToday.UI/Platforms/MacCatalyst/Program.cs) | Provides the Mac Catalyst entry point and starts UIKit with `AppDelegate`. |
| [AppDelegate (Mac Catalyst)](src/RosaryForToday.UI/Platforms/MacCatalyst/AppDelegate.cs) | Connects the Mac Catalyst application lifecycle to `MauiProgram`. |
| [Program (Tizen)](src/RosaryForToday.UI/Platforms/Tizen/Main.cs) | Provides the Tizen entry point, runs the native application, and delegates MAUI app creation to `MauiProgram`. |

### RosaryForToday.Application.UnitTests

| Class | Description |
| --- | --- |
| [BaseFixture](src/Tests/RosaryForToday.Application.UnitTests/Tests/BaseFixture.cs) | Provides the shared AutoFixture setup with AutoNSubstitute and a frozen `IMapper` substitute for test classes. |
| [CreateRosaryTypeCommandHandlerTests](src/Tests/RosaryForToday.Application.UnitTests/Tests/CommandHandlers/CreateRosaryTypeCommandHandlerTests.cs) | Defines the Rosary type creation test using `BaseFixture` and an in-memory database, checking the saved name and language identifier. |

### Additional source declaration

| Class | Description |
| --- | --- |
| [RosaryMapper (additional copy)](src/src/RosaryForToday.Presentation/Mappers/RosaryMapper.cs) | An additional mapper declaration outside the solution's project directories. It declares Rosary and reflection DTO-to-view-model conversions and is not compiled by the current projects. |

## Android

The application is built for Android with .NET MAUI, targeting `net10.0-android`. It requires **Android 12 (API level 31) or later**, as specified by `SupportedOSPlatformVersion` in [RosaryForToday.UI.csproj](src/RosaryForToday.UI/RosaryForToday.UI.csproj).
