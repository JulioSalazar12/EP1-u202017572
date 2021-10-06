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
            SoftwareSystem energySystem = model.AddSoftwareSystem("Sistema soporte de energia ecologica", "Sistema soporte de energia ecologica para Conductores de vehículos eléctricos, Operadores de punto de carga, Propietarios de flota y Proveedores de energía");
            SoftwareSystem googleMaps = model.AddSoftwareSystem("Google Maps", "Plataforma que ofrece una REST API para geolocalizacion referencial.");
            SoftwareSystem vehicleSystem = model.AddSoftwareSystem("Vehicle System", "Permite conocer la informacion de la carga del vehiculo");
            SoftwareSystem weatherSystem = model.AddSoftwareSystem("Weather System", "Permite conocer el clima para validar los costos o tarifas de carga");

            Person conductor =  model.AddPerson("Conductor de cehiculo", "Conductor de cehiculo electrico");
            Person operadorPC = model.AddPerson("CPO", "Operador de punto de carga");
            Person propietarioF = model.AddPerson("Propietario de flota", "Empresario propietario de una flota de vehiculos electricos");
            Person proveedorE = model.AddPerson("Proveedor de energia", "Empresa proveedora de energia");
            
            conductor.Uses(energySystem, "Realiza consultas para conocer ubicacion y reserva en CPO");
            operadorPC.Uses(energySystem, "Realiza consultas para conocer la reserva de carga");
            propietarioF.Uses(energySystem, "Realiza consultas para reservar turno de flota de vehiculos");
            proveedorE.Uses(energySystem, "Realiza consultas para suminstrar energia correctamente");
            energySystem.Uses(weatherSystem, "Usa el servicio de clima para estimar una tarifa correcta");
            energySystem.Uses(vehicleSystem, "Usa el sistema del vehiculo para conocer la informacion de carga de cehiculo");
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

            // 2. Diagrama de Contenedores
            Container webApplication   = energySystem.AddContainer("Web App",                   "Permite a los usuarios visualizar la ubicacion, informacion y tarifa de la energia", "Flutter Web");
            Container recargaContext   = energySystem.AddContainer("Recarga Context",           "Bounded Context del Microservicio de Recarga de vehiculo", "NodeJS (NestJS)");
            Container ubicacionContext = energySystem.AddContainer("Geolocalizacion Context",   "Bounded Context del Microservicio de información de CPO", "NodeJS (NestJS)");
            Container reservaContext   = energySystem.AddContainer("Reserva Inventory Context", "Bounded Context del Microservicio de horario de recargas", "NodeJS (NestJS)");            
            Container database         = energySystem.AddContainer("Database", "", "Oracle");
            
            conductor.Uses(webApplication, "Consulta");
            proveedorE.Uses(webApplication, "Consulta");
            propietarioF.Uses(webApplication, "Consulta");
            operadorPC.Uses(webApplication, "Consulta");

            webApplication.Uses(recargaContext, " x ", "JSON/HTTPS");
            webApplication.Uses(ubicacionContext, " x ", "JSON/HTTPS");
            webApplication.Uses(reservaContext, " x ", "JSON/HTTPS");
            
            recargaContext.Uses(database, "", "JDBC");
            ubicacionContext.Uses(database, "", "JDBC");
            reservaContext.Uses(database, "", "JDBC");
            
            ubicacionContext.Uses(googleMaps, "API Request", "JSON/HTTPS");
            recargaContext.Uses(vehicleSystem, "API Request", "JSON/HTTPS");
            recargaContext.Uses(weatherSystem, "API Request", "JSON/HTTPS");

            // Tags
            webApplication.AddTags("WebApp");
            database.AddTags("Database");
            recargaContext.AddTags("BoundedContext");
            ubicacionContext.AddTags("BoundedContext");
            reservaContext.AddTags("BoundedContext");

            styles.Add(new ElementStyle("WebApp") { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
            styles.Add(new ElementStyle("Database") { Shape = Shape.Cylinder, Background = "#ff0000", Color = "#ffffff", Icon = "" });
            styles.Add(new ElementStyle("BoundedContext") { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });

            ContainerView containerView = viewSet.CreateContainerView(energySystem, "Contenedor", "Diagrama de contenedores");
            contextView.PaperSize = PaperSize.A4_Landscape;
            containerView.AddAllElements();            

            structurizrClient.UnlockWorkspace(workspaceId);
            structurizrClient.PutWorkspace(workspaceId, workspace);
        }
    }
}