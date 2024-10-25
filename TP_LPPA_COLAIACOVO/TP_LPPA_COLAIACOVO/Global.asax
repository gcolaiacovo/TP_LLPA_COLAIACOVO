<%@ Application Language="C#" %>

<script runat="server">
    void Application_Start(object sender, EventArgs e) 
    {
        LPPA_Colaiacovo_Services.DatabaseBackupService databaseBackupService = new LPPA_Colaiacovo_Services.DatabaseBackupService();

        databaseBackupService.CrearBackupBaseDeDatos();
    }

    void Application_End(object sender, EventArgs e) 
    {
        // Código que se ejecuta cuando la aplicación se está cerrando
    }

    void Application_Error(object sender, EventArgs e) 
    {
        // Manejo de errores globales
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Código que se ejecuta cuando una nueva sesión de usuario comienza
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Código que se ejecuta cuando una sesión de usuario termina
    }
</script>