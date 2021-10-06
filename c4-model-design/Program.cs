using Structurizr;
using Structurizr.Api;

namespace c4_model_design
{
    class Program
    {
        static void Main(string[] args)
        {
            Banking();
        }

        static void Banking()
        {
            const long workspaceId = 70064;
            const string apiKey = "902aed58-332a-4428-a088-ddb6f9494272";
            const string apiSecret = "58a0ec4a-3817-45d5-a94c-a4cedf93c47c";

            StructurizrClient structurizrClient = new StructurizrClient(apiKey, apiSecret);
            Workspace workspace = new Workspace("Sistema soporte de energia ecologica", "Sistema soporte de energia ecologica para Conductores de vehículos eléctricos, Operadores de punto de carga, Propietarios de flota y Proveedores de energía");
            ViewSet viewSet = workspace.Views;
            Model model = workspace.Model;

            // 1. Diagrama de Contexto
            SoftwareSystem energySystem = model.AddSoftwareSystem("Lorem ipsum", "Lorem ipsum");
            SoftwareSystem googleMaps = model.AddSoftwareSystem("Google Maps", "Plataforma que ofrece una REST API para geolocalizacion referencial.");
            SoftwareSystem vehicleSystem = model.AddSoftwareSystem("Vehicle System", "Permite conocer la informacion de la carga del vehiculo");
            SoftwareSystem weatherSystem = model.AddSoftwareSystem("Weather System", "Permite conocer el clima para validar los costos o tarifas de carga");

            Person conductor =  model.AddPerson("conductor", "clientte2");
            Person operadorPC = model.AddPerson("operador", "cliendte2");
            Person propietarioF = model.AddPerson("propietario flota", "cliente2");
            Person proveedorE = model.AddPerson("proveedor energia", "cliesnte2");
            
            conductor.Uses(energySystem, "Realiza consultas para mantenerse ");
            operadorPC.Uses(energySystem, "Realiza consultas para mantenerse ");
            propietarioF.Uses(energySystem, "Realiza consultas para mantenerse ");
            proveedorE.Uses(energySystem, "Realiza consultas para mantenerse ");
            energySystem.Uses(weatherSystem, "Lorem ipsum");
            energySystem.Uses(vehicleSystem, "Lorem ipsum");
            energySystem.Uses(googleMaps, "Usa la API de google maps");
            
            SystemContextView contextView = viewSet.CreateSystemContextView(energySystem, "Contexto", "Diagrama de contexto");
            contextView.PaperSize = PaperSize.A3_Landscape;
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();

            // Tags
            conductor.AddTags("cliente");
            proveedorE.AddTags("cliente");
            propietarioF.AddTags("cliente");
            operadorPC.AddTags("cliente");
            energySystem.AddTags("EnergySystem");
            googleMaps.AddTags("GoogleMaps");
            vehicleSystem.AddTags("VehicleSystem");
            weatherSystem.AddTags("WeatherSystem");

            Styles styles = viewSet.Configuration.Styles;
            styles.Add(new ElementStyle("cliente") { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle("EnergySystem") { Background = "#008f39", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("GoogleMaps") { Background = "#90714c", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("VehicleSystem") { Background = "#2f95c7", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("WeatherSystem") { Background = "#FF3062", Color = "#ffffff", Shape = Shape.RoundedBox });

            // // 2. Diagrama de Contenedores
            // Container mobileApplication = energySystem.AddContainer("Mobile App", "Permite a los usuarios visualizar un dashboard con el resumen de toda la información del traslado de los lotes de vacunas.", "Flutter");
            // Container webApplication = energySystem.AddContainer("Web App", "Permite a los usuarios visualizar un dashboard con el resumen de toda la información del traslado de los lotes de vacunas.", "Flutter Web");
            // Container landingPage = energySystem.AddContainer("Landing Page", "", "Flutter Web");
            // Container apiRest = energySystem.AddContainer("API Rest", "API Rest", "NodeJS (NestJS) port 8080");
            // Container flightPlanningContext = energySystem.AddContainer("Flight Planning Context", "Bounded Context del Microservicio de Planificación de Vuelos", "NodeJS (NestJS)");
            // Container airportContext = energySystem.AddContainer("Airport Context", "Bounded Context del Microservicio de información de Aeropuertos", "NodeJS (NestJS)");
            // Container aircraftInventoryContext = energySystem.AddContainer("Aircraft Inventory Context", "Bounded Context del Microservicio de Inventario de Aviones", "NodeJS (NestJS)");
            // Container vaccinesInventoryContext = energySystem.AddContainer("Vaccines Inventory Context", "Bounded Context del Microservicio de Inventario de Vacunas", "NodeJS (NestJS)");
            // Container monitoringContext = energySystem.AddContainer("Monitoring Context", "Bounded Context del Microservicio de Monitoreo en tiempo real del status y ubicación del vuelo que transporta las vacunas", "NodeJS (NestJS)");
            // Container database = energySystem.AddContainer("Database", "", "Oracle");
            
            // ciudadano.Uses(mobileApplication, "Consulta");
            // ciudadano.Uses(webApplication, "Consulta");
            // ciudadano.Uses(landingPage, "Consulta");

            // mobileApplication.Uses(apiRest, "API Request", "JSON/HTTPS");
            // webApplication.Uses(apiRest, "API Request", "JSON/HTTPS");

            // apiRest.Uses(flightPlanningContext, "", "");
            // apiRest.Uses(airportContext, "", "");
            // apiRest.Uses(aircraftInventoryContext, "", "");
            // apiRest.Uses(vaccinesInventoryContext, "", "");
            // apiRest.Uses(monitoringContext, "", "");
            
            // flightPlanningContext.Uses(database, "", "JDBC");
            // airportContext.Uses(database, "", "JDBC");
            // aircraftInventoryContext.Uses(database, "", "JDBC");
            // vaccinesInventoryContext.Uses(database, "", "JDBC");
            // monitoringContext.Uses(database, "", "JDBC");
            
            // monitoringContext.Uses(googleMaps, "API Request", "JSON/HTTPS");
            // monitoringContext.Uses(vehicleSystem, "API Request", "JSON/HTTPS");

            // // Tags
            // mobileApplication.AddTags("MobileApp");
            // webApplication.AddTags("WebApp");
            // landingPage.AddTags("LandingPage");
            // apiRest.AddTags("APIRest");
            // database.AddTags("Database");
            // flightPlanningContext.AddTags("FlightPlanningContext");
            // airportContext.AddTags("AirportContext");
            // aircraftInventoryContext.AddTags("AircraftInventoryContext");
            // vaccinesInventoryContext.AddTags("VaccinesInventoryContext");
            // monitoringContext.AddTags("MonitoringContext");

            // styles.Add(new ElementStyle("MobileApp") { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.MobileDevicePortrait, Icon = "" });
            // styles.Add(new ElementStyle("WebApp") { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
            // styles.Add(new ElementStyle("LandingPage") { Background = "#929000", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
            // styles.Add(new ElementStyle("APIRest") { Shape = Shape.RoundedBox, Background = "#0000ff", Color = "#ffffff", Icon = "" });
            // styles.Add(new ElementStyle("Database") { Shape = Shape.Cylinder, Background = "#ff0000", Color = "#ffffff", Icon = "" });
            // styles.Add(new ElementStyle("FlightPlanningContext") { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
            // styles.Add(new ElementStyle("AirportContext") { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
            // styles.Add(new ElementStyle("AircraftInventoryContext") { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
            // styles.Add(new ElementStyle("VaccinesInventoryContext") { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
            // styles.Add(new ElementStyle("MonitoringContext") { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });

            // ContainerView containerView = viewSet.CreateContainerView(energySystem, "Contenedor", "Diagrama de contenedores");
            // contextView.PaperSize = PaperSize.A4_Landscape;
            // containerView.AddAllElements();            

            structurizrClient.UnlockWorkspace(workspaceId);
            structurizrClient.PutWorkspace(workspaceId, workspace);
        }
    }
}